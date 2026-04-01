using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class AccountController : Controller
    {
        private MusicStoreEntities dbContext = new MusicStoreEntities();

        private void MigrateShoppingCart(string userName)
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            cart.MigrateCart(userName);
            HttpContext.Session.SetString(ShoppingCart.CartSessionKey, userName);
        }

        // GET: /Account/LogOn
        public ActionResult LogOn()
        {
            return View();
        }

        // POST: /Account/LogOn
        [HttpPost]
        public async Task<ActionResult> LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Validate user against the database
                var user = dbContext.Users.SingleOrDefault(u => u.UserName == model.UserName);
                if (user != null && VerifyPassword(model.Password, user.Password))
                {
                    MigrateShoppingCart(model.UserName);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.UserName)
                    };

                    // Add role claims
                    var userRoles = dbContext.UserRoles
                        .Where(ur => ur.UserId == user.UserId)
                        .Select(ur => ur.Role.RoleName)
                        .ToList();
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { IsPersistent = model.RememberMe });

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(model);
        }

        // GET: /Account/LogOff
        public async Task<ActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password.Length < 6)
                {
                    ModelState.AddModelError("", "The password provided is invalid. Password must be at least 6 characters.");
                    return View(model);
                }

                if (dbContext.Users.Any(u => u.UserName == model.UserName))
                {
                    ModelState.AddModelError("", "User name already exists. Please enter a different user name.");
                    return View(model);
                }

                var newUser = new MusicStoreUser
                {
                    UserId = Guid.NewGuid(),
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = HashPassword(model.Password),
                    CreateDate = DateTime.Now,
                    IsApproved = true
                };
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                MigrateShoppingCart(model.UserName);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: /Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Account/ChangePassword
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    var user = dbContext.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                    if (user != null && VerifyPassword(model.OldPassword, user.Password))
                    {
                        user.Password = HashPassword(model.NewPassword);
                        dbContext.SaveChanges();
                        changePasswordSucceeded = true;
                    }
                    else
                    {
                        changePasswordSucceeded = false;
                    }
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            return View(model);
        }

        // GET: /Account/ChangePasswordSuccess
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private static string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string plaintext, string storedHash)
        {
            return HashPassword(plaintext) == storedHash;
        }
    }
}
