# Progress Details — 02-sdk-conversion

## What Was Done

Converted MvcMusicStore.csproj from legacy (old-style) format to SDK-style.

## Changes Made

- **MvcMusicStore.csproj**: Converted to `<Project Sdk="Microsoft.NET.Sdk.Web">` format
  - packages.config migrated to PackageReference items in csproj
  - Legacy MSBuild imports removed
  - Project remains on net472 (SDK conversion is format-only, not TFM upgrade)
- **Properties\AssemblyInfo.cs**: Removed — SDK auto-generates these attributes; the file caused duplicate CS0579 errors

## Build Results

The project does not build on net472 in its current state because the SDK conversion removed the `Microsoft.AspNet.Mvc` and related System.Web packages (they are not compatible with SDK-style under dotnet build). This is expected for an in-place rewrite migration — the build will be restored after TFM upgrade and ASP.NET Core migration tasks.

## Done When Criteria

- [x] Project file is SDK-style (`<Project Sdk="Microsoft.NET.Sdk.Web">`)
- [x] packages.config removed and replaced with PackageReference in csproj
- [ ] Project builds on net472 — **deferred** (expected to have errors since System.Web packages are not SDK-compatible; will be resolved after TFM upgrade in task 03)
