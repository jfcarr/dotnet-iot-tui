# .NET gRPC Project

## Create the Service

<https://github.com/jfcarr/dotnet-iot-tui/tree/main/src/SenseHatGrpcProvider>

Steps:

1. Create the project, using `grpc` as the project type.
1. Add a project reference to `SenseHatLib.csproj`
1. Add .NET IoT bindings.
1. Configure the project for remote access (in `launchsettings.json`)
1. Update the protocol buffer file with sensor-specific request and response messages.
1. Implement the service messages.

## Service Startup

Open a terminal in the project directory and run:

```bash
dotnet run
```

If you want to start the service and leave it running in the background, add an ampersand to your command:

```bash
dotnet run &
```

## Testing

We won't implement a tester as part of this discussion, but if you want to explore this further on your own, here are some options for implementing a gRPC test runner:

* [Postman](https://www.postman.com/)
* [gRPCurl](https://github.com/fullstorydev/grpcurl) / [gRPC UI](https://github.com/fullstorydev/grpcui)

