# MvcMusicStore .NET 10 Upgrade - Assessment

## Source Application
- **Framework**: .NET Framework 4.7.2
- **Project Type**: ASP.NET MVC 5 Web Application
- **ORM**: Entity Framework 6
- **Authentication**: ASP.NET Membership Provider (Forms Authentication)

## Target
- **Framework**: .NET 10 (net10.0)
- **Project Type**: ASP.NET Core MVC
- **ORM**: Entity Framework Core 10
- **Authentication**: ASP.NET Core Identity

## Key Migration Areas

### 1. Project System
- Old: XML-based .csproj with NuGet via packages.config
- New: SDK-style .csproj with PackageReference

### 2. Application Entry Point
- Old: Global.asax / HttpApplication
- New: Program.cs (minimal hosting model)

### 3. Controllers
- Old: System.Web.Mvc.Controller
- New: Microsoft.AspNetCore.Mvc.Controller
- Changes: HttpStatusCodeResult → BadRequest()/NotFound(), ActionResult → IActionResult

### 4. Entity Framework
- Old: EF6 with DbContext, DropCreateDatabaseIfModelChanges seeding
- New: EF Core 10 with IdentityDbContext, EnsureCreated + manual seeding

### 5. Authentication
- Old: ASP.NET Membership (Membership.ValidateUser, FormsAuthentication)
- New: ASP.NET Core Identity (UserManager, SignInManager)

### 6. Views
- Old: System.Web.Optimization (Styles.Render/Scripts.Render), Html.RenderAction for child actions
- New: Direct static file references, ViewComponents replacing child actions

### 7. Session
- Old: System.Web.HttpContext.Session (direct string indexing)
- New: ISession.GetString/SetString

### 8. Configuration
- Old: web.config (connectionStrings, appSettings, system.web)
- New: appsettings.json, Program.cs middleware configuration

## Packages Replaced
| Old Package | New Package |
|---|---|
| Microsoft.AspNet.Mvc 5.2.7 | Built into ASP.NET Core |
| EntityFramework 6.3.0 | Microsoft.EntityFrameworkCore.SqlServer 10.0.5 |
| Microsoft.AspNet.Identity.* | Microsoft.AspNetCore.Identity.EntityFrameworkCore 10.0.5 |
| System.Web.Optimization | Removed (static files) |
| WebGrease/Antlr | Removed |
| Microsoft.SqlServer.Compact | Removed (using LocalDB) |
