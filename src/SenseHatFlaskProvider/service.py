from flask import Flask
from sense_hat import SenseHat
import json

app = Flask(__name__)


@app.post("/sense/hello")
def hello_msg_sense():
    sense = SenseHat()

    sense.set_rotation(180)
    sense.show_message("Hello world!")

    return "Success!"


@app.get("/Sensor/SensorData/<int:measurement_units>")
def get_current_sensor_data(measurement_units):

    sensor_result = SensorResult()

    try:
        sense = SenseHat()

        sensor_result.data.altitude = 0
        if measurement_units == 2:
            sensor_result.data.altitude = my_utils.meters_to_feet(
                sensor_result.data.altitude)
        sensor_result.data.altitudeUnits = "feet" if measurement_units == 2 else "meters"
        sensor_result.data.formattedAltitude = f"{sensor_result.data.altitude} {sensor_result.data.altitudeUnits}"

        sensor_result.data.humidity = sense.get_humidity()
        sensor_result.data.formattedHumidity = f"{sensor_result.data.humidity} %"

        sensor_result.data.temperature = sense.get_temperature()
        if measurement_units == 2:
            sensor_result.data.temperature = my_utils.celsius_to_fahrenheit(
                sensor_result.data.temperature)
        sensor_result.data.temperatureUnits = "fahrenheit" if measurement_units == 2 else "celsius"
        sensor_result.data.formattedTemperature = f"{sensor_result.data.temperature} {sensor_result.data.temperatureUnits}"

        sensor_result.status.isValid = True
        sensor_result.status.errorMessage = ""
    except Exception as ex:
        sensor_result.status.isValid = False
        sensor_result.status.errorMessage = str(ex)

    return sensor_result.to_json()


@app.put("/Sensor/SetLedWhite")
def set_led_white():
    set_led([255, 255, 255])

    return "Success!"


@app.put("/Sensor/SetLedRed")
def set_led_red():
    set_led([255, 0, 0])

    return "Success!"


def set_led(rgb):
    sense = SenseHat()

    O = rgb

    led_grid = [
        O, O, O, O, O, O, O, O,
        O, O, O, O, O, O, O, O,
        O, O, O, O, O, O, O, O,
        O, O, O, O, O, O, O, O,
        O, O, O, O, O, O, O, O,
        O, O, O, O, O, O, O, O,
        O, O, O, O, O, O, O, O,
        O, O, O, O, O, O, O, O
    ]

    sense.set_pixels(led_grid)


class my_utils:
    @staticmethod
    def celsius_to_fahrenheit(input_celsius):
        return (input_celsius * 1.8) + 32

    def meters_to_feet(input_meters):
        return input_meters * 3.28084


class SensorResult:
    def __init__(self):
        self.data = SensorData()
        self.status = SensorStatus()

    def to_json(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=True, indent=4)


class SensorData:
    def __init__(self):
        self.altitude = 0
        self.altitudeUnits = ""
        self.formattedAltitude = ""
        self.humidity = 0
        self.formattedHumidity = ""
        self.temperature = 0
        self.temperatureUnits = ""
        self.formattedTemperature = ""


class SensorStatus:
    def __init__(self):
        self.isValid = False
        self.isSynthetic = False
        self.errorMessage = ""
