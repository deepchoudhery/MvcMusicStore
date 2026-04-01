# .NET Version Upgrade

## Strategy
**Selected**: All-at-Once
**Rationale**: Single .NET Framework project (MvcMusicStore, net472). No dependency graph to manage. All-at-Once is the prescribed strategy for single-project solutions.

### Execution Constraints
- Single atomic upgrade — all project changes applied together; validate full solution build after upgrade
- SDK-style conversion first (separate task), then TFM upgrade + code migration
- Build and fix all compilation errors in one bounded pass after upgrade
- Tests validated after atomic upgrade completes

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0
- **Commit Strategy**: Single Commit at End

## Upgrade Options
**Source**: .github/upgrades/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

### Project Structure
- Project Approach: In-place rewrite (web project, 7 controllers, low complexity)

### Compatibility
- Unsupported Packages: Resolve Inline (3 incompatible packages)
- System.Web Adapters: Direct Migration to ASP.NET Core APIs

### Modernization
- Configuration Migration: Auto-migrate to .NET Core Configuration
- Nullable Reference Types: Leave Disabled
- Entity Framework: Keep EF6

## Source Control
- **Source Branch**: master (grafted HEAD)
- **Working Branch**: current branch (no new branch created per instructions)
- **Commit Strategy**: Single Commit at End
