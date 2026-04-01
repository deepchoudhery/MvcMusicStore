# Progress Details — 04-aspnet-core-migration

## What Was Done

Full in-place migration from ASP.NET MVC 5 (System.Web) to ASP.NET Core MVC on net10.0.

## Files Deleted
- `Global.asax` + `Global.asax.cs` → replaced by Program.cs
- `App_Start/BundleConfig.cs`, `FilterConfig.cs`, `RouteConfig.cs` → replaced by Program.cs configuration
- `App_Code/CustomHelpers.cshtml` → replaced with `App_Code/CustomHelpers.cs`

## Files Created
- **`Program.cs`** — ASP.NET Core startup: cookie auth, session, EF6 initializer, MVC routing, static files
- **`appsettings.json`** — connection strings and logging config (migrated from web.config)
- **`Views/_ViewImports.cshtml`** — tag helpers + namespace imports for views
- **`App_Code/CustomHelpers.cs`** — static C# helper replacing the Razor `@helper` directive
- **`wwwroot/`** — static assets copied from Content/, Scripts/, fonts/, favicon.ico

## Files Modified
- **All 7 Controllers** — removed System.Web usings, added Microsoft.AspNetCore.Mvc; replaced HttpStatusCodeResult→BadRequest(), HttpNotFound()→NotFound(), Server.HtmlEncode→WebUtility.HtmlEncode, FormCollection→IFormCollection, removed [ChildActionOnly]
- **AccountController** — replaced Membership/FormsAuthentication with cookie auth (SignInAsync/SignOutAsync)
- **Models/AccountModels.cs** — removed System.Web.Mvc + System.Web.Security usings
- **Models/ShoppingCart.cs** — replaced HttpContextBase with HttpContext, session calls updated to GetString/SetString
- **Models/Album.cs, Order.cs** — removed System.Web usings
- **Views/Shared/_Layout.cshtml** — replaced @Styles.Render/@Scripts.Render with direct link/script tags; replaced Html.RenderAction with inline HTML
- **Views/Shared/Error.cshtml** — removed @model System.Web.Mvc.HandleErrorInfo
- **Views/Account/Register.cshtml, ChangePassword.cshtml** — removed @Membership.MinRequiredPasswordLength
- **Views/StoreManager/Create.cshtml, Edit.cshtml** — replaced @Scripts.Render with direct script tags

## Build Result
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
Time Elapsed 00:00:02.26
```

## Done When Criteria
- [x] All System.Web references removed
- [x] Application compiles against net10.0 ASP.NET Core
- [x] All controllers/views/models build without errors
- [x] Program.cs replaces Global.asax with proper ASP.NET Core startup
- [x] Cookie auth replaces FormsAuthentication/Membership
- [x] appsettings.json replaces web.config
- [x] Static files in wwwroot
