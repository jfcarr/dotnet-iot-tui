# Blazor

A web application, for displaying the sensor data in a browser.  .NET supports two Blazor project types: `Blazor Server` (runs on the server) and `Blazor WebAssembly` (runs in the browser).  We'll be using the Server type.

## Create the Project

<https://github.com/jfcarr/dotnet-iot-tui/tree/main/src/BlazorClient>

Steps:

1. Create the project, using `blazorserver` as the project type.
1. Add a project reference to `SenseHatLib.csproj`
1. App settings:
   1. Copy the `appsettings.default.json` file to `appsettings.json`, and update the setting values.
   1. Add appsettings content entry to the .csproj file.
1. Sensor page:
   1. Add a new `Sensor.razor` page.
   1. Add a code-behind file for the page: `Sensor.razor.cs`.
   1. Implement code details.
   1. Update `NavMenu.razor` to include the new Sensor page.
