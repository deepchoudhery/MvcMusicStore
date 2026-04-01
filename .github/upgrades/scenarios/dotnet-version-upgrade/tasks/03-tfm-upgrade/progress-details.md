# Progress Details — 03-tfm-upgrade

## What Was Done

Updated TargetFramework from net472 → net10.0 and cleaned up the csproj.

## Changes Made

**MvcMusicStore.csproj** — complete rewrite to clean SDK-style:
- `TargetFramework`: net472 → **net10.0**
- `Nullable`: disable (leave off for migration)
- `ImplicitUsings`: enable
- **Removed** packages: Microsoft.AspNet.Mvc 5.2.7, Microsoft.AspNet.Razor, Microsoft.AspNet.WebPages, Microsoft.Web.Infrastructure, EntityFramework.SqlServerCompact, Microsoft.SqlServer.Compact, WebGrease, Antlr, jQuery, jQuery.Validation, Microsoft.jQuery.Unobtrusive.Validation, Modernizr, bootstrap (static asset packages removed; JS/CSS will be managed as wwwroot static files in task 04)
- **Removed** legacy cruft: `<Reference>` items, `<Content Include>` for static files, SQL CE `<None Include>` items, PublishDatabaseSettings block
- **Kept/upgraded**: EntityFramework 6.3.0 → **6.5.1**, Newtonsoft.Json 11.0.1 → **13.0.4**

## Final csproj
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>disable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="EntityFramework" Version="6.5.1" />
    <PackageReference Include="Newtonsoft.Json" Version="13.0.4" />
  </ItemGroup>
</Project>
```

## Restore Result
`dotnet restore` — ✅ **Success** — EF 6.5.1 and Newtonsoft.Json 13.0.4 resolve cleanly for net10.0.

## Build Errors (Expected — Task 04 Must Fix)
All errors are System.Web / ASP.NET MVC 5 APIs missing in .NET 10:
- CS0234: System.Web.HttpApplication, System.Web.Mvc, System.Web.Optimization, System.Web.Routing, System.Web.Security
- CS0246: ActionResult, Controller, BundleCollection, HttpContextBase, MembershipCreateStatus, and all MVC 5 attribute types

## Done When Criteria
- [x] TargetFramework is net10.0
- [x] All incompatible packages removed
- [x] EF 6.5.1 and Newtonsoft.Json 13.0.4 kept
- [x] `dotnet restore` succeeds
- [ ] Project builds — deferred to task 04 (ASP.NET Core migration)
