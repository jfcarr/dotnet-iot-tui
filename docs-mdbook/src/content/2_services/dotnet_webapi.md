# .NET WebAPI Project (traditional REST)

## Create the Service

<https://github.com/jfcarr/dotnet-iot-tui/tree/main/src/SenseHatProvider>

Steps:

1. Create the project, using `webapi` as the project type.
1. Add a project reference to `SenseHatLib.csproj`
1. Add .NET IoT bindings.
1. Configure the project for remote access (in `launchsettings.json`)
1. Configure RESTful access to sensor data.

## Service Startup

Open a terminal in the project directory and run:

```bash
dotnet run
```

If you want to start the service and leave it running in the background, add an ampersand to your command:

```bash
dotnet run &
```
## Test with Swagger

Access the service Swagger page at `http://<service ip>:<service port>/swagger`.
