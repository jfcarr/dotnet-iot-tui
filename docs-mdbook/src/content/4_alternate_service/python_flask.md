# Python / Flask Project

Using a standard like REST to capture information from a device and make it available means we can swap out the internal details of the service implementation.  This can be as simple as refactoring of an existing implementation, or as drastic as completely replacing the dev stack.

This is an example of recreating our .NET implementation with Python/Flask.

## Create the Project

<https://github.com/jfcarr/dotnet-iot-tui/tree/main/src/SenseHatFlaskProvider>

1. Install Python (you probably already have it).
1. Install libraries for Flask and the Sense HAT:
   1. `pip install flask`
   1. `pip install sense-hat`
1. Create `service.py` file.  Add endpoints and supporting code.

## Start the Service

```bash
flask --app service run --host=0.0.0.0 --port 5191
```
