# Configure the Raspberry Pi and Sense HAT

> This is my working setup. Yours may be different, but this is the one that works for me.

Obtain a Raspberry Pi.  I'm using a [Raspberry Pi 4](https://www.raspberrypi.com/products/raspberry-pi-4-model-b/).

Install the 64-bit version of [Raspberry Pi OS](https://www.raspberrypi.com/software/).

Using the `raspi-config` application, enable the following services:

* **SSH** - Allows remote access to the Raspberry Pi via Secure Shell.
* **I2C** - Enables the serial communication bus, allowing communication with attached devices and sensors.

Obtain a [Sense HAT](https://www.raspberrypi.com/products/sense-hat/).  The Raspberry Pi Sense HAT (_Hardware Attached on Top_) is an add-on board for Raspberry Pi. The Sense HAT is equipped with an 8×8 RGB LED matrix, a five-button joystick, and includes the following sensors:

* Gyroscope
* Accelerometer
* Magnetometer
* Temperature
* Barometric pressure
* Humidity

The Sense HAT can be installed directly on top of the Raspberry Pi.  Keep in mind, however, that the ambient heat from the Raspberry Pi will influence the readings from the temperature sensor.  If you want more accurate readings, you should use a [compatible ribbon cable](https://www.adafruit.com/product/4823).

