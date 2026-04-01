using System;
using System.Data.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcMusicStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Provide connection string to EF6 from appsettings.json
var connStr = builder.Configuration.GetConnectionString("MusicStoreEntities");
if (!string.IsNullOrEmpty(connStr))
{
    MusicStoreEntities.ConnectionString = connStr;
}

// Configure EF6 initializer to seed sample data
Database.SetInitializer(new SampleData());

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/LogOn";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2880);
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Index");
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Error/NotFound");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
