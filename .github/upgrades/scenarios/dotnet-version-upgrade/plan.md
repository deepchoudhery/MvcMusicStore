# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade MvcMusicStore from net472 (ASP.NET MVC 5 / .NET Framework) to net10.0 (ASP.NET Core)
**Scope**: Single project — MvcMusicStore.csproj (~1,700 LOC, 7 controllers, EF6, System.Web)

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 1 project on .NET Framework 4.7.2, single web application with 7 controllers, low complexity assessment.

## Tasks

### 01-prerequisites: Verify SDK and toolchain for net10.0

Verify that the .NET 10.0 SDK is installed and the solution can proceed with the upgrade. Check global.json for any SDK version pins that would block the upgrade. This is a non-modifying validation task that ensures the environment is ready before structural changes begin.

The project targets net472 with Visual Studio-style non-SDK csproj format. The SDK verification step confirms that `dotnet` tooling for net10.0 is available and validates the global.json (if present) does not restrict to an incompatible SDK version.

**Done when**: .NET 10.0 SDK confirmed installed; any global.json conflicts identified and documented; no blocking environment issues found.

---

### 02-sdk-conversion: Convert MvcMusicStore.csproj to SDK-style format

Convert the non-SDK-style project file (MvcMusicStore.csproj, currently using `<Project ToolsVersion="4.0">` format with packages.config) to modern SDK-style format while keeping the current target framework (net472). This is a structural change only — no TFM change, no code changes.

The conversion involves: replacing the legacy csproj with an SDK-style `<Project Sdk="Microsoft.NET.Sdk.Web">` file, converting packages.config references to `<PackageReference>` elements in the project file, removing auto-generated assembly attributes (AssemblyInfo.cs patterns that SDK generates), and removing explicit file includes that SDK auto-discovers. Assessment identified Project.0001 (needs SDK-style conversion) as mandatory. The packages.config has 17 packages that must be converted to PackageReference format.

**Done when**: MvcMusicStore.csproj is SDK-style format; packages.config removed; project builds successfully on net472; no regressions in package references.

---

### 03-upgrade-mvcmusicstore: Upgrade MvcMusicStore to net10.0 with ASP.NET Core migration

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

### 04-final-validation: Final solution validation

Validate the upgraded solution end-to-end. Confirm the solution builds cleanly with no errors and no warnings. Run any existing tests. Document any deferred items (EF Core migration, enabling Nullable, removing deprecated packages) as follow-on recommendations.

**Done when**: Solution builds with 0 errors and 0 warnings; all tests pass (if any); deferred recommendations documented.

---
