# 01-prerequisites: Verify SDK and toolchain for net10.0

Verify that the .NET 10.0 SDK is installed and the solution can proceed with the upgrade. Check global.json for any SDK version pins that would block the upgrade. This is a non-modifying validation task that ensures the environment is ready before structural changes begin.

The project targets net472 with Visual Studio-style non-SDK csproj format. The SDK verification step confirms that `dotnet` tooling for net10.0 is available and validates the global.json (if present) does not restrict to an incompatible SDK version.

**Done when**: .NET 10.0 SDK confirmed installed; any global.json conflicts identified and documented; no blocking environment issues found.

---
