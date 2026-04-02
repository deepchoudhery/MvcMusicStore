# Assessment: MvcMusicStore Migration to ASP.NET Core (.NET 10)

## Overview
MvcMusicStore is a classic ASP.NET MVC 5 application (targeting .NET 4.7.2) that implements an online music store. It uses Entity Framework 6 with SQL Server Compact, ASP.NET Membership for authentication, and System.Web.Optimization for bundling.

## Current Technology Stack
- **Framework**: ASP.NET MVC 5 (.NET 4.7.2)
- **ORM**: Entity Framework 6.3 (with SQL Server Compact 4.0)
- **Authentication**: ASP.NET Membership + Forms Authentication
- **Authorization**: Role-based via AspNetSqlRoleProvider
- **Bundling**: System.Web.Optimization
- **Database**: SQL Server Compact (.sdf file)

## Components Requiring Migration

### Project Infrastructure
| Component | Current | Target | Effort |
|---|---|---|---|
| Project file | Legacy .csproj (net472) | SDK-style (net10.0) | Low |
| Startup | Global.asax / App_Start/*.cs | Program.cs | Medium |
| Configuration | web.config | appsettings.json | Low |
| Packages | packages.config | PackageReference | Low |

### Authentication / Authorization
| Component | Current | Target | Effort |
|---|---|---|---|
| Authentication | ASP.NET Membership + FormsAuth | ASP.NET Core Identity | High |
| AccountController | Membership.ValidateUser, FormsAuthentication | UserManager<IdentityUser>, SignInManager | High |
| Roles | SqlRoleProvider | ASP.NET Core Identity Roles | Medium |
| Session management | HttpSessionState | ISession (ASP.NET Core) | Medium |

### Controllers
| Controller | Issues | Effort |
|---|---|---|
| AccountController | Uses Membership, FormsAuth, MVCUserRoles namespace | High |
| CheckoutController | Uses FormCollection, HttpNotFound | Low |
| HomeController | Uses HttpNotFound | Low |
| ShoppingCartController | Uses Server.HtmlEncode | Low |
| StoreController | Uses HttpStatusCodeResult, HttpNotFound, [ChildActionOnly] | Low |
| StoreManagerController | Uses [Bind(Include=...)], HttpStatusCodeResult | Low |
| ErrorController | Minor namespace changes | Low |

### Models
| Model | Issues | Effort |
|---|---|---|
| MusicStoreEntities.cs | EF6 DbContext (System.Data.Entity) | Medium |
| SampleData.cs | EF6 DropCreateDatabaseIfModelChanges | Medium |
| ShoppingCart.cs | HttpContextBase, Controller type | Medium |
| Album.cs | System.Web.Mvc reference | Low |
| Order.cs | System.Web.Mvc [Bind] attribute | Low |
| AccountModels.cs | System.Web.Security (Membership), MVCUserRoles namespace | High |

### Views
| View | Issues | Effort |
|---|---|---|
| _Layout.cshtml | Styles.Render, Scripts.Render, Html.RenderAction | Medium |
| Account/Register.cshtml | Uses @Membership.MinRequiredPasswordLength | Low |
| Account/LogOn.cshtml | MVCUserRoles.Models namespace in @model | Low |
| Account/Register.cshtml | MVCUserRoles.Models namespace in @model | Low |
| StoreManager/Index.cshtml | CustomHelpers.Truncate usage | Low |
| StoreManager/Create.cshtml | @Scripts.Render | Low |
| StoreManager/Edit.cshtml | @Scripts.Render | Low |

### Helpers / ViewComponents
| Component | Current | Target | Effort |
|---|---|---|---|
| App_Code/CustomHelpers.cshtml | @helper Razor syntax | Static C# helper class | Low |
| StoreController.GenreMenu | [ChildActionOnly] partial action | ViewComponent | Medium |
| ShoppingCartController.CartSummary | [ChildActionOnly] partial action | ViewComponent | Medium |

## Migration Risks
1. **Authentication**: Full replacement of ASP.NET Membership requires careful handling of password hashing compatibility (breaking change - existing passwords won't work).
2. **SQL Server Compact**: Not supported in EF Core. Will use SQLite for dev/test.
3. **ChildActionOnly**: No direct equivalent in ASP.NET Core - must use ViewComponents.
4. **FormCollection**: Replaced by IFormCollection in ASP.NET Core.
5. **TryUpdateModel**: Works differently in ASP.NET Core - use model binding instead.
6. **HttpStatusCodeResult**: Replaced by StatusCode()/BadRequest()/NotFound() etc.

## Dependencies to Add
- `Microsoft.EntityFrameworkCore.Sqlite` (replaces EF6 + SQL Server Compact)
- `Microsoft.EntityFrameworkCore.Tools` (for migrations)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (replaces Membership)
- `Microsoft.AspNetCore.Authentication.Cookies` (built into ASP.NET Core)
