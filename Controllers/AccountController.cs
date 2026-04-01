using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using MVCUserRoles.Models;
using System.Security.Claims;

namespace MVCUserRoles.Controllers
{
    public class AccountController : Controller
    {
        private readonly MusicStoreEntities _dbContext;

        public AccountController(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        private void MigrateShoppingCart(string userName)
        {
            var cart = ShoppingCart.GetCart(this);
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
                var user = _dbContext.Users.SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
                if (user != null)
                {
                    MigrateShoppingCart(model.UserName);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
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
                if (_dbContext.Users.Any(u => u.UserName == model.UserName))
                {
                    ModelState.AddModelError("", "User name already exists. Please enter a different user name.");
                    return View(model);
                }

                var user = new AppUser { UserName = model.UserName, Password = model.Password, Email = model.Email, Role = "User" };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                MigrateShoppingCart(model.UserName);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET: /Account/ChangePassword
        [Microsoft.AspNetCore.Authorization.Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Account/ChangePassword
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.UserName == User.Identity!.Name && u.Password == model.OldPassword);
                if (user != null)
                {
                    user.Password = model.NewPassword;
                    _dbContext.SaveChanges();
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
    }
}
