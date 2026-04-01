# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [MvcMusicStore.csproj](#mvcmusicstorecsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 1 | All require upgrade |
| Total NuGet Packages | 17 | 8 need upgrade |
| Total Code Files | 49 |  |
| Total Code Files with Incidents | 5 |  |
| Total Lines of Code | 2689 |  |
| Total Number of Issues | 23 |  |
| Estimated LOC to modify | 0+ | at least 0.0% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [MvcMusicStore.csproj](#mvcmusicstorecsproj) | net472 | 🔴 High | 17 | 0 |  | Wap, Sdk Style = False |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 9 | 52.9% |
| ⚠️ Incompatible | 4 | 23.5% |
| 🔄 Upgrade Recommended | 4 | 23.5% |
| ***Total NuGet Packages*** | ***17*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| Antlr | 3.5.0.2 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | Needs to be replaced with Replace with new package Antlr4=4.6.6 |
| bootstrap | 3.4.1 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | ✅Compatible |
| EntityFramework | 6.3.0 | 6.5.1 | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package upgrade is recommended |
| EntityFramework.SqlServerCompact | 6.3.0 | 4.3.1 | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | ⚠️NuGet package is incompatible |
| jQuery | 3.3.1 | 3.7.1 | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package contains security vulnerability |
| jQuery.Validation | 1.17.0 | 1.21.0 | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package contains security vulnerability |
| Microsoft.AspNet.Mvc | 5.2.7 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package functionality is included with framework reference |
| Microsoft.AspNet.Razor | 3.2.7 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package functionality is included with framework reference |
| Microsoft.AspNet.Web.Optimization | 1.1.3 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | ⚠️NuGet package is incompatible |
| Microsoft.AspNet.WebPages | 3.2.7 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package functionality is included with framework reference |
| Microsoft.CodeDom.Providers.DotNetCompilerPlatform | 2.0.0 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package functionality is included with framework reference |
| Microsoft.jQuery.Unobtrusive.Validation | 3.2.11 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | ⚠️NuGet package is deprecated |
| Microsoft.SqlServer.Compact | 4.0.8876.1 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | ⚠️NuGet package is incompatible |
| Microsoft.Web.Infrastructure | 1.0.0.0 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package functionality is included with framework reference |
| Modernizr | 2.8.3 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | ✅Compatible |
| Newtonsoft.Json | 11.0.1 | 13.0.4 | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | NuGet package upgrade is recommended |
| WebGrease | 1.6.0 |  | [MvcMusicStore.csproj](#mvcmusicstorecsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>⚙️&nbsp;MvcMusicStore.csproj</b><br/><small>net472</small>"]
    click P1 "#mvcmusicstorecsproj"

```

## Project Details

<a id="mvcmusicstorecsproj"></a>
### MvcMusicStore.csproj

#### Project Info

- **Current Target Framework:** net472
- **Proposed Target Framework:** net10.0
- **SDK-style**: False
- **Project Kind:** Wap
- **Dependencies**: 0
- **Dependants**: 0
- **Number of Files**: 92
- **Number of Files with Incidents**: 5
- **Lines of Code**: 2689
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["MvcMusicStore.csproj"]
        MAIN["<b>⚙️&nbsp;MvcMusicStore.csproj</b><br/><small>net472</small>"]
        click MAIN "#mvcmusicstorecsproj"
    end

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

