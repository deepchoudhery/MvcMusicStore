# Task 03 — Upgrade MvcMusicStore to net10.0

## Summary

Full ASP.NET MVC 5 / .NET Framework 4.7.2 → ASP.NET Core / net10.0 migration completed. Build succeeds with 0 errors and 0 warnings.

## Files Modified

### Project File
- `MvcMusicStore.csproj` — net472 → net10.0; removed all System.Web/incompatible packages; kept EntityFramework 6.5.1 and Newtonsoft.Json 13.0.3; removed explicit Microsoft.AspNetCore.Authentication.Cookies (framework-included on net10.0)

### Startup / Configuration
- `Program.cs` (NEW) — ASP.NET Core entry point: Cookie Auth, Session, EF6 initializer, static files, routing; reads connection string from IConfiguration and injects into EF6
- `appsettings.json` (NEW) — migrated connection strings from web.config
- `Global.asax.cs` — replaced with comment-only stub
- `App_Start/BundleConfig.cs` — replaced with comment-only stub
- `App_Start/RouteConfig.cs` — replaced with comment-only stub
- `App_Start/FilterConfig.cs` — replaced with comment-only stub

### Controllers (7/7 migrated)
- `Controllers/HomeController.cs` — using Microsoft.AspNetCore.Mvc; added System.Linq, System.Collections.Generic
- `Controllers/StoreController.cs` — using Microsoft.AspNetCore.Mvc; HttpNotFound→NotFound(); HttpStatusCodeResult→StatusCode(); String.IsNullOrEmpty; System.Linq
- `Controllers/CheckoutController.cs` — using Microsoft.AspNetCore.Mvc; FormCollection→IFormCollection
- `Controllers/ShoppingCartController.cs` — using Microsoft.AspNetCore.Mvc; Server.HtmlEncode→WebUtility.HtmlEncode; removed ChildActionOnly
- `Controllers/StoreManagerController.cs` — using Microsoft.AspNetCore.Mvc + Microsoft.AspNetCore.Authorization; SelectList from Microsoft.AspNetCore.Mvc.Rendering
- `Controllers/ErrorController.cs` — using Microsoft.AspNetCore.Mvc; ActionName("NotFound") to avoid base class collision
- `Controllers/AccountController.cs` — full rewrite: FormsAuthentication/Membership → Cookie Auth; custom MusicStoreUser with SHA256 hashing; fixed namespace MVCUserRoles→MvcMusicStore

### Models
- `Models/UserModels.cs` (NEW) — MusicStoreUser, MusicStoreRole, MusicStoreUserRole for cookie auth
- `Models/MusicStoreEntities.cs` — added Users/Roles/UserRoles DbSets; static ConnectionString property set at startup from IConfiguration
- `Models/AccountModels.cs` — removed System.Web.Security/System.Web.Mvc; fixed namespace MVCUserRoles→MvcMusicStore
- `Models/ShoppingCart.cs` — HttpContextBase→HttpContext; Session.GetString/SetString (ISession)
- `Models/Album.cs`, `Artist.cs`, `Genre.cs` — removed System.Web usings
- `Models/Order.cs` — removed System.Web.Mvc; [Bind] to ASP.NET Core syntax (no Include=)
- `Models/SampleData.cs` — removed System.Web using

### Views
- `Views/_ViewImports.cshtml` (NEW) — Tag Helper registration; namespace usings
- `Views/Shared/_Layout.cshtml` — replaced @Scripts.Render/@Styles.Render with direct tags; Html.RenderAction→@await Component.InvokeAsync(); fixed image path
- `Views/Shared/Error.cshtml` — removed @model System.Web.Mvc.HandleErrorInfo
- `Views/Shared/Components/GenreMenu/Default.cshtml` (NEW) — ViewComponent template
- `Views/Shared/Components/CartSummary/Default.cshtml` (NEW) — ViewComponent template
- `Views/StoreManager/Create.cshtml`, `Edit.cshtml` — jqueryval bundle → direct script tags
- `Views/StoreManager/Index.cshtml` — @CustomHelpers.Truncate → inline Razor ternary
- `Views/Account/Register.cshtml`, `ChangePassword.cshtml`, `LogOn.cshtml` — removed @Membership.MinRequiredPasswordLength; fixed namespace MVCUserRoles→MvcMusicStore

### ViewComponents (NEW)
- `ViewComponents/GenreMenuViewComponent.cs`
- `ViewComponents/CartSummaryViewComponent.cs`

### Static Files (wwwroot)
- `wwwroot/css/` — CSS files from Content/
- `wwwroot/scripts/` — JS files from Scripts/
- `wwwroot/fonts/` — font files
- `wwwroot/images/` — images from Content/Images/

### Deleted
- `App_Code/CustomHelpers.cshtml` — @helper directive not supported in ASP.NET Core; replaced with inline Razor expressions in the consuming view

## Build Result

```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

## Key Technical Decisions

1. **EF6 connection string**: EF6 on .NET Core cannot read appsettings.json automatically. Solution: `MusicStoreEntities.ConnectionString` static property set from `IConfiguration` at startup in Program.cs.
2. **Authentication**: Replaced SqlMembershipProvider with Cookie Authentication + custom user tables (MusicStoreUser). SHA256 password hashing.
3. **ChildActionOnly → ViewComponents**: GenreMenu and CartSummary converted to ViewComponents since `[ChildActionOnly]` doesn't exist in ASP.NET Core.
4. **Microsoft.AspNetCore.Authentication.Cookies**: Removed explicit PackageReference — it's included in the ASP.NET Core shared framework on net10.0.
