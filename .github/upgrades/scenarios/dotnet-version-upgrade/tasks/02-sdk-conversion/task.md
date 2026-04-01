# 02-sdk-conversion: Convert project to SDK-style format

MvcMusicStore.csproj uses the old-style project format (with `<Project ToolsVersion=...>`, `packages.config`, and `<Import>` targets). This must be converted to SDK-style (`<Project Sdk="Microsoft.NET.Sdk.Web">`) before the TFM upgrade. The conversion keeps the project on net472 but modernizes the project file structure, migrates packages.config to PackageReference format, and removes legacy MSBuild imports.

Key concerns: the project is a WAP (Web Application Project) with GlobalAsax, web.config transforms, Content/Scripts/Views folders, and legacy build artifacts in `_bin_deployableAssemblies`. The conversion tooling should handle most of this automatically.

**Done when**: Project file is SDK-style, packages.config is removed and replaced with PackageReference in the csproj, project builds on net472 with no errors.
