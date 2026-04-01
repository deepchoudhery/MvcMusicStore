# Upgrade Options — MvcMusicStore

Assessment: 1 project (MvcMusicStore, net472 WAP), 17 NuGet packages (4 incompatible), System.Web MVC app, Entity Framework 6.3.0

## Strategy

### Upgrade Strategy
Single .NET Framework web project (net472) — All-at-Once is fixed per single-project rule.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Single project, no dependency graph to manage. Upgrade in one atomic pass. |

## Project Structure

### Project Approach
ASP.NET Framework MVC project (System.Web) that is small (single project, minimal controllers) — in-place rewrite is appropriate.

| Value | Description |
|-------|-------------|
| **In-place rewrite** (selected) | Replace the Framework web project entirely in one pass. Appropriate for small, focused project. |
| Side-by-side | Creates a new ASP.NET Core project alongside the old one; appropriate for large/complex projects requiring live continuity. |

## Compatibility

### Unsupported Packages
4 incompatible packages detected: EntityFramework.SqlServerCompact, Microsoft.AspNet.Web.Optimization, Microsoft.SqlServer.Compact, and Antlr (needs replacement with Antlr4).

| Value | Description |
|-------|-------------|
| **Resolve Inline** (selected) | Research and resolve each incompatible package within the same task. Small count (4) is manageable inline. |
| Defer Resolution | Generate stubs and create follow-up tasks. Better for > 3 packages without known replacements. |

### System.Web Adapters
ASP.NET Framework MVC project with System.Web references detected. In-place rewrite selected so Direct Migration is recommended.

| Value | Description |
|-------|-------------|
| **Direct Migration to ASP.NET Core APIs** (selected) | No adapter shims; all System.Web usage replaced with native ASP.NET Core equivalents. Cleaner result for in-place rewrite. |
| Use System.Web Adapters | Adds compatibility shims for HttpContext.Current. Better for side-by-side migration. |

## Modernization

### Entity Framework
EntityFramework 6.3.0 detected targeting net10.0 — EF6 supports .NET Core, so keeping EF6 avoids dual sources of breaking changes.

| Value | Description |
|-------|-------------|
| **Keep EF6** (selected) | EF6 6.3+ is compatible with .NET Core. Complete the .NET version upgrade first, then evaluate EF Core migration separately. |
| Migrate to EF Core | Migrate EF simultaneously with .NET upgrade. Higher risk. |

### Nullable Reference Types
Single project, target is net10.0. Project is not small/simple (has ASP.NET MVC surface area) — leave disabled to focus on migration.

| Value | Description |
|-------|-------------|
| **Leave Disabled** (selected) | Does not enable nullable. Maintains existing null handling. Enable separately after migration. |
| Enable Nullable Reference Types | Adds `<Nullable>enable</Nullable>`. May require code updates to address warnings. |
