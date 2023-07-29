# Terminal.GUI

From the [Terminal.Gui repo](https://github.com/gui-cs/Terminal.Gui):

> Terminal.Gui: A toolkit for building rich console apps for .NET, .NET Core, and Mono that works on Windows, the Mac, and Linux/Unix.

## What's a TUI?

> In computing, text-based user interfaces (TUI) (alternately terminal user interfaces, to reflect a dependence upon the properties of computer terminals and not just text), is a retronym describing a type of user interface (UI) common as an early form of human–computer interaction, before the advent of modern conventional graphical user interfaces (GUIs). Like GUIs, they may use the entire screen area and accept mouse and other inputs. They may also use color and often structure the display using special graphical characters such as ┌ and ╣, referred to in Unicode as the "box drawing" set. The modern context of use is usually a terminal emulator. 
>
> <https://en.wikipedia.org/wiki/Text-based_user_interface>

What are the benefits?

* Performance
* Runnable on headless systems
* Standalone
* Nice balance between the simplicity of a CLI and the enhanced presentation/controls/widgets of a GUI.

## Create the Project

<https://github.com/jfcarr/dotnet-iot-tui/tree/main/src/TerminalGuiClient>

1. Create the project, using `console` as the project type.
1. Add a package reference to `Terminal.Gui`.
1. Add a project reference to `SenseHatLib.csproj`
1. App settings:
   1. Copy the `appsettings.default.json` file to `appsettings.json`, and update the setting values.
   1. Add appsettings content entry to the .csproj file.
1. Implement details in `Program.cs`
