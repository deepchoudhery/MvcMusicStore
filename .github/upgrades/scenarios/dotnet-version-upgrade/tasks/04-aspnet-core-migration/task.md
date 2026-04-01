# 04-aspnet-core-migration: Migrate ASP.NET MVC code to ASP.NET Core

Convert the application code from ASP.NET MVC (System.Web) to ASP.NET Core MVC. This is the largest task and involves replacing the entire pipeline and bootstrapping:

- Replace Global.asax.cs with Program.cs + startup configuration
- Replace System.Web.Mvc references with Microsoft.AspNetCore.Mvc
- Replace System.Web.Optimization bundling with direct HTML links to CDN or static files
- Migrate web.config app settings and connection strings to appsettings.json
- Update controllers, views, and models for ASP.NET Core conventions
- Replace HttpContext.Current usages with IHttpContextAccessor or injected HttpContext
- Migrate authentication/authorization setup (FormsAuthentication → ASP.NET Core Identity or Cookie auth)
- Update Razor views (remove legacy @Styles.Render/@Scripts.Render helper calls)
- Ensure Entity Framework 6 is configured for use in ASP.NET Core DI container
- Remove Web.Debug.config / Web.Release.config transforms (replaced by appsettings.{env}.json)

**Done when**: All System.Web references removed, application compiles against net10.0 ASP.NET Core, all controllers/views/models build without errors.
