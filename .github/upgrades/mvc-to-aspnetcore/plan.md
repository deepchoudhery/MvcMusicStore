# Migration Plan: MvcMusicStore → ASP.NET Core (.NET 10)

## Strategy
Full rewrite of infrastructure (project file, startup, configuration) while preserving business logic, models, and views. Migrate authentication from ASP.NET Membership to ASP.NET Core Identity. Migrate database from EF6+SQL Server Compact to EF Core+SQLite.

## Tasks

### Task 1: Replace Project File (SDK-style)
- Replace `MvcMusicStore.csproj` with SDK-style project targeting `net10.0`
- Remove `packages.config`
- Add NuGet PackageReferences for ASP.NET Core, EF Core, Identity
- Remove legacy build artifacts imports

### Task 2: Create Program.cs (Startup)
- Create `Program.cs` with full ASP.NET Core setup
- Configure services: MVC, EF Core, Identity, Session, Authentication
- Configure middleware pipeline: HTTPS, static files, routing, auth, session
- Remove `Global.asax` / `Global.asax.cs`

### Task 3: Update Configuration
- Create `appsettings.json` with SQLite connection string
- Create `appsettings.Development.json`
- Remove `web.config`, `Web.Debug.config`, `Web.Release.config`

### Task 4: Update Models
- Remove all `using System.Web` references from models
- Update `MusicStoreEntities.cs` from EF6 to EF Core
- Rewrite `SampleData.cs` for EF Core seeding pattern
- Update `ShoppingCart.cs` to use ASP.NET Core `HttpContext`
- Update `Album.cs` to remove `System.Web.Mvc` reference
- Update `Order.cs` to use `Microsoft.AspNetCore.Mvc` `[Bind]`
- Rewrite `AccountModels.cs` (move to MvcMusicStore.Models namespace, remove Membership)

### Task 5: Create ViewComponents
- Create `GenreMenuViewComponent` to replace `[ChildActionOnly]` action on StoreController
- Create `CartSummaryViewComponent` to replace `[ChildActionOnly]` action on ShoppingCartController

### Task 6: Update Controllers
- Update all `using` statements (System.Web.Mvc → Microsoft.AspNetCore.Mvc)
- Update `AccountController` to use ASP.NET Core Identity (UserManager, SignInManager)
- Update `StoreController`: remove [ChildActionOnly], update status code results
- Update `ShoppingCartController`: replace Server.HtmlEncode, update GetCart
- Update `CheckoutController`: replace FormCollection with IFormCollection, update TryUpdateModel
- Update `StoreManagerController`: update HttpStatusCodeResult usages, update [Bind(Include=...)]
- Update `ErrorController`: remove System.Web reference

### Task 7: Create Helpers
- Convert `App_Code/CustomHelpers.cshtml` to a static C# class `Helpers/StringHelper.cs`

### Task 8: Update Views
- Update `_Layout.cshtml`: replace Styles.Render/Scripts.Render with direct links/script tags, replace Html.RenderAction with ViewComponents
- Update `Views/_ViewStart.cshtml`: ensure correct
- Update Account views: fix @model namespaces, remove @Membership.MinRequiredPasswordLength
- Update StoreManager/Create.cshtml and Edit.cshtml: replace @Scripts.Render
- Add `Views/_ViewImports.cshtml` for shared using directives
- Update `Views/Shared/Error.cshtml`

### Task 9: Add EF Core Migrations
- Create initial EF Core migration
- Configure database initialization/seeding on startup

### Task 10: Final Cleanup
- Remove old files: Global.asax, Global.asax.cs, App_Start/, Properties/AssemblyInfo.cs, Views/Web.config
- Run build and fix any remaining errors/warnings
- Verify application configuration
