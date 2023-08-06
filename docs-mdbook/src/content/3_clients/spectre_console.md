# Spectre.Console

From the [Spectre.Console repo](https://github.com/spectreconsole/spectre.console):

> A .NET library that makes it easier to create beautiful, cross platform, console applications.  It is heavily inspired by the excellent [Rich library](https://github.com/willmcgugan/rich) for Python. For detailed usage instructions, please refer to the documentation at <https://spectreconsole.net/>.

## Spectre.Console vs Terminal.GUI

Using Terminal.Gui, you can create applications that look more like traditional desktop apps than you can with Spectre.Console.  But, Terminal.Gui has a critical dependency that introduces a limitation: It requires a graphical stack for rendering.  Usually, this is fine: You'll be running your Terminal.Gui applications from a command prompt in Windows, or in a terminal from your Linux desktop.  But, for something like a Linux server that boots to a CLI, your Terminal.Gui applications _will not run_ because there's no graphical stack.

Spectre.Console applications do not follow the "desktop application" paradigm.  They are closer to console applications, but with enhanced colors, and rendering of other things like tables, but in a scrolling view.  Most importantly, though, Spectre.Console applications do _not_ require a graphical environment.  So, for a purely text-based environment, Spectre.Console fills this need.

## Create the Project

<https://github.com/jfcarr/dotnet-iot-tui/tree/main/src/SpectreClient>

1. Create the project, using `console` as the project type.
1. Add package references:
   1. Spectre.Console.Cli (_adds nice command line argument handling_)
   1. Spectre.Console
1. Add a project reference to `SenseHatLib.csproj`
1. App settings:
   1. Copy the `appsettings.default.json` file to `appsettings.json`, and update the setting values.
   1. Add appsettings content entry to the .csproj file.
1. Implement details in `Program.cs`
