# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade MvcMusicStore from .NET Framework 4.7.2 to .NET 10.0
**Scope**: 1 project (MvcMusicStore.csproj), ASP.NET MVC web application with Entity Framework 6, ~17 NuGet packages

### Selected Strategy
**All-At-Once** — Single project upgraded in one atomic operation.
**Rationale**: 1 project on .NET Framework 4.7.2, no dependency graph to manage, in-place rewrite approach.

## Tasks

### 01-prerequisites: Verify prerequisites and SDK compatibility

Verify that the .NET 10 SDK is installed and compatible with the project. Check if global.json or other SDK pinning files exist that could prevent the upgrade. This is a quick validation step that ensures the environment is ready before any code changes are made.

**Done when**: .NET 10 SDK is confirmed available, any global.json conflicts are resolved, environment is ready for upgrade.

---

### 02-sdk-conversion: Convert project to SDK-style format

MvcMusicStore.csproj uses the old-style project format (with `<Project ToolsVersion=...>`, `packages.config`, and `<Import>` targets). This must be converted to SDK-style (`<Project Sdk="Microsoft.NET.Sdk.Web">`) before the TFM upgrade. The conversion keeps the project on net472 but modernizes the project file structure, migrates packages.config to PackageReference format, and removes legacy MSBuild imports.

Key concerns: the project is a WAP (Web Application Project) with GlobalAsax, web.config transforms, Content/Scripts/Views folders, and legacy build artifacts in `_bin_deployableAssemblies`. The conversion tooling should handle most of this automatically.

**Done when**: Project file is SDK-style, packages.config is removed and replaced with PackageReference in the csproj, project builds on net472 with no errors.

---

### 03-tfm-upgrade: Upgrade TFM to net10.0 and replace incompatible packages

Change the project's TargetFramework from net472 to net10.0. This is the core upgrade task and will surface compilation errors from incompatible APIs, removed packages, and changed behavior.

Key work items:
- Update TargetFramework to net10.0
- Remove packages that are now built into the framework (Microsoft.AspNet.Mvc, Microsoft.AspNet.Razor, Microsoft.AspNet.WebPages, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Microsoft.Web.Infrastructure)
- Remove incompatible packages with no net10.0 equivalent (EntityFramework.SqlServerCompact, Microsoft.SqlServer.Compact — switch to SQLite or SQL Server LocalDB, WebGrease, Microsoft.AspNet.Web.Optimization — replace bundling with static file references, Antlr 3.5 → replace with direct reference or Antlr4 if needed)
- Upgrade recommended packages (EntityFramework 6.3 → 6.5.1, jQuery 3.3.1 → 3.7.1 for security, jQuery.Validation 1.17 → 1.21.0 for security, Newtonsoft.Json 11 → 13.0.4)
- Upgrade bootstrap to latest, replace deprecated Microsoft.jQuery.Unobtrusive.Validation

**Done when**: TargetFramework is net10.0, all packages are resolved (no incompatible or deprecated references blocking build), project restores successfully.

---

### 04-aspnet-core-migration: Migrate ASP.NET MVC code to ASP.NET Core

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

---

### 05-final-validation: Final build validation and cleanup

Build the complete solution, fix any remaining warnings, and verify the upgrade is complete. Check for any leftover legacy artifacts (packages.config, bin deployable assemblies, App_Code references, unused web.config sections). Document any deferred recommendations (e.g., EF Core migration, enabling nullable reference types).

**Done when**: Solution builds with 0 errors and 0 warnings, all projects target net10.0, no legacy artifacts remain.
