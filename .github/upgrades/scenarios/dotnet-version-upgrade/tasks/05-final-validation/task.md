# 05-final-validation: Final build validation and cleanup

Build the complete solution, fix any remaining warnings, and verify the upgrade is complete. Check for any leftover legacy artifacts (packages.config, bin deployable assemblies, App_Code references, unused web.config sections). Document any deferred recommendations (e.g., EF Core migration, enabling nullable reference types).

**Done when**: Solution builds with 0 errors and 0 warnings, all projects target net10.0, no legacy artifacts remain.
