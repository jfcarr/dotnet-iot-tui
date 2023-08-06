# .NET IoT/TUI

This repo contains supporting material for the ["Dayton .NET Developers Group: .NET IoT and Terminal.Gui" talk](https://www.meetup.com/gem-city-tech/events/tlfjbtyfclblb/).

Date and Time | Location
---------|----------
August 8th, 2023 at 6pm | [Innovation Hub in downtown Dayton, Ohio.](https://goo.gl/maps/Q1vuLmJmniJoFFmG7)

High-level documentation is published [here](https://jfcarr.github.io/dotnet-iot-tui/).

## Repo Layout


Directory | Description | Project Type
----------|-------------|-------------
docs | Generated documentation, from docs-mdbook.
docs-mdbook | Documentation, in markdown. | mdbook
rest-profiles | REST call profiles | VS Code "REST Client" extension
src/BlazorClient | REST client | .NET Blazor Server
src/ConsoleClient | REST client | .NET Console
src/SenseHatFlaskProvider | REST service | Python/Flask
src/SenseHatGrpcClient | gRPC client | .NET Console
src/SenseHatGrpcProvider | gRPC service | .NET gRPC
src/SenseHatLib | Shared functions | .NET ClassLib
src/SenseHatProvider | REST service | .NET WebApi
src/SpectreClient | REST client | .NET Console w/ Spectre library
src/TerminalGuiClient | REST client | .NET Console w/ Terminal.GUI library
