# 02-sdk-conversion: Convert MvcMusicStore.csproj to SDK-style format

Convert the non-SDK-style project file (MvcMusicStore.csproj, currently using `<Project ToolsVersion="4.0">` format with packages.config) to modern SDK-style format while keeping the current target framework (net472). This is a structural change only — no TFM change, no code changes.

The conversion involves: replacing the legacy csproj with an SDK-style `<Project Sdk="Microsoft.NET.Sdk.Web">` file, converting packages.config references to `<PackageReference>` elements in the project file, removing auto-generated assembly attributes (AssemblyInfo.cs patterns that SDK generates), and removing explicit file includes that SDK auto-discovers. Assessment identified Project.0001 (needs SDK-style conversion) as mandatory. The packages.config has 17 packages that must be converted to PackageReference format.

**Done when**: MvcMusicStore.csproj is SDK-style format; packages.config removed; project builds successfully on net472; no regressions in package references.

---
