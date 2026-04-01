# 03-upgrade-mvcmusicstore: Upgrade MvcMusicStore to net10.0 with ASP.NET Core migration

Upgrade MvcMusicStore from net472/ASP.NET MVC 5 to net10.0/ASP.NET Core. This is an in-place rewrite of the web framework layer — replacing System.Web, ASP.NET MVC 5, and related .NET Framework dependencies with their ASP.NET Core equivalents.

Key work areas based on assessment findings:
- **TFM change**: Update TargetFramework from net472 to net10.0
- **Package updates**: Remove/replace incompatible packages (EntityFramework.SqlServerCompact → remove/replace; Microsoft.AspNet.Web.Optimization → remove, replace bundle references with direct HTML tags; Microsoft.SqlServer.Compact → remove; Microsoft.AspNet.Mvc 5.x → removed, included in framework; Antlr → replace with Antlr4; jQuery/jQuery.Validation → update to latest). Security vulnerabilities in jQuery 3.3.1 and jQuery.Validation 1.17.0 must be addressed.
- **System.Web removal**: Direct migration — replace HttpContext.Current with IHttpContextAccessor, replace MVC 5 routing/filters/action results with ASP.NET Core equivalents
- **Global.asax → Program.cs/Startup**: Convert application initialization (Feature.1000) — move route registration, bundle config, filter config, DB initialization to Program.cs middleware pipeline
- **Bundling/minification**: System.Web.Optimization not available (Feature.0001) — replace bundle references in views with direct `<link>` and `<script>` tags pointing to content files
- **Configuration**: Migrate web.config appSettings and connectionStrings to appsettings.json; migrate config access to IConfiguration injection
- **Authentication**: Convert Forms Authentication to ASP.NET Core Identity / Cookie Authentication
- **EF6**: Keep EF6 (6.3+ supports .NET Core); upgrade package from 6.3.0 to 6.5.1
- **Code fixes**: Fix all compilation errors introduced by API surface changes

**Done when**: MvcMusicStore builds on net10.0 with zero errors and zero warnings; all incompatible packages resolved; System.Web references removed; ASP.NET Core middleware pipeline configured; configuration migrated to appsettings.json.

---
