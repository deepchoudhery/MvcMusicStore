# 03-tfm-upgrade: Upgrade TFM to net10.0 and replace incompatible packages

Change the project's TargetFramework from net472 to net10.0. This is the core upgrade task and will surface compilation errors from incompatible APIs, removed packages, and changed behavior.

Key work items:
- Update TargetFramework to net10.0
- Remove packages that are now built into the framework (Microsoft.AspNet.Mvc, Microsoft.AspNet.Razor, Microsoft.AspNet.WebPages, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Microsoft.Web.Infrastructure)
- Remove incompatible packages with no net10.0 equivalent (EntityFramework.SqlServerCompact, Microsoft.SqlServer.Compact — switch to SQLite or SQL Server LocalDB, WebGrease, Microsoft.AspNet.Web.Optimization — replace bundling with static file references, Antlr 3.5 → replace with direct reference or Antlr4 if needed)
- Upgrade recommended packages (EntityFramework 6.3 → 6.5.1, jQuery 3.3.1 → 3.7.1 for security, jQuery.Validation 1.17 → 1.21.0 for security, Newtonsoft.Json 11 → 13.0.4)
- Upgrade bootstrap to latest, replace deprecated Microsoft.jQuery.Unobtrusive.Validation

**Done when**: TargetFramework is net10.0, all packages are resolved (no incompatible or deprecated references blocking build), project restores successfully.
