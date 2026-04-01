# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0 (LTS)
- **Commit Strategy**: After Each Task

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

### Project Structure
- Project Approach: In-place rewrite

### Compatibility
- Unsupported Packages: Resolve Inline (4 incompatible packages)
- System.Web Adapters: Direct Migration to ASP.NET Core APIs

### Modernization
- Entity Framework: Keep EF6
- Nullable Reference Types: Leave Disabled

## Strategy
**Selected**: All-at-Once
**Rationale**: Single .NET Framework project (net472), single web app, no dependency graph to manage.

### Execution Constraints
- Single atomic upgrade — all projects updated together
- Validate full solution build after upgrade
- SDK-style conversion is a separate task from TFM upgrade
- In-place rewrite: replace System.Web with ASP.NET Core APIs directly
- No System.Web Adapters shims — direct migration only
