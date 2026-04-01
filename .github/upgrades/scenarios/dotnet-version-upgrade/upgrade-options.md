# Upgrade Options — MvcMusicStore

Assessment: 1 project (MvcMusicStore, net472 ASP.NET MVC), 23 issues, incompatible packages, EF6, old-style csproj, System.Web references

## Strategy

### Upgrade Strategy
Single .NET Framework project with 7 controllers — All-at-Once is the prescribed strategy for single-project solutions.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade the single project atomically in one pass. No dependency graph to manage. |

## Project Structure

### Project Approach
ASP.NET MVC web project with System.Web references. 7 controllers (≤10), single project with low complexity assessment.

| Value | Description |
|-------|-------------|
| **In-place rewrite** (selected) | Replace the Framework web project entirely in one pass. Appropriate for small projects with low controller count. |
| Side-by-side | Creates a new ASP.NET Core project alongside the existing one. Injects scaffold/migrate tasks. |

## Compatibility

### Unsupported Packages
3 incompatible packages detected: EntityFramework.SqlServerCompact, Microsoft.AspNet.Web.Optimization, Microsoft.SqlServer.Compact.

| Value | Description |
|-------|-------------|
| **Resolve Inline** (selected) | Research and resolve each incompatible package within the same upgrade task. Appropriate for ≤3 incompatible packages. |
| Defer Resolution | Generate stubs and create follow-up tasks for replacements. |

### System.Web Adapters
ASP.NET MVC project with System.Web references detected. In-place rewrite selected with small project scope (7 controllers).

| Value | Description |
|-------|-------------|
| **Direct Migration to ASP.NET Core APIs** (selected) | Replace all System.Web usage immediately with native ASP.NET Core equivalents. No compatibility layer needed for small in-place rewrite. |
| Use System.Web Adapters | Adds Microsoft.AspNetCore.SystemWebAdapters compatibility shims. Better for large or side-by-side migrations. |

## Modernization

### Configuration Migration
web.config with standard appSettings (4 keys) and connectionStrings. No custom section handlers, no encryption, no transforms beyond Debug/Release.

| Value | Description |
|-------|-------------|
| **Auto-migrate to .NET Core Configuration** (selected) | Automatically converts web.config to appsettings.json and migrates code to IConfiguration. |
| Manual Migration with Mapping Document | Generates detailed mapping for complex configs. |

### Nullable Reference Types
Target is net10.0 (supports nullable). Single project, high-complexity migration (System.Web, incompatible packages). Large enough that enabling nullable simultaneously adds risk.

| Value | Description |
|-------|-------------|
| **Leave Disabled** (selected) | Does not enable nullable. Maintain existing null handling. Enable separately after migration as a distinct effort. |
| Enable Nullable Reference Types | Adds `<Nullable>enable</Nullable>` to project file. May require code updates. |

### Entity Framework
EF6 6.3.0 detected, target is net10.0. EF6 6.3+ supports .NET Core — upgrading simultaneously with .NET upgrade introduces two sources of breaking changes.

| Value | Description |
|-------|-------------|
| **Keep EF6** (selected) | EF6 6.3+ is compatible with .NET Core. Complete .NET upgrade first, then evaluate EF Core migration separately. Lowest risk. |
| Migrate to EF Core | Migrates Entity Framework simultaneously. Higher risk, only appropriate for very small data layers. |
