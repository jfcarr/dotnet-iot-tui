# What is IoT?

The term IoT, or Internet of Things, refers to the collective network of connected devices and the technology that facilitates communication between devices and the cloud, as well as between the devices themselves. Thanks to the advent of inexpensive computer chips and high bandwidth telecommunication, we now have billions of devices  connected to the internet. This means everyday devices like toothbrushes, vacuums, cars, and machines can use sensors to collect data and respond intelligently to users.  

The Internet of Things integrates everyday “things” with the internet. Computer Engineers have been adding sensors and processors to everyday objects since the 90s. However, progress was initially slow because the chips were big and bulky. Low power computer chips called RFID tags were first used to track expensive equipment. As computing devices shrank in size, these chips also became smaller, faster, and smarter over time.

# Single-Board Computer vs Microcontroller

A **single-board computer** (SBC) is a complete computer built on a single circuit board.  It has a microprocessor(s), memory, input/output (I/O) and other features required of a fully functional computer, and runs a full operating system.  Examples: Raspberry Pi, Orange Pi, Hummingboard.

A **microcontroller** (or MCU, for microcontroller unit) is a small computer on a single VLSI integrated circuit (IC) chip. A microcontroller contains one or more CPUs (processor cores) along with memory and programmable input/output peripherals. Program memory in the form of ferroelectric RAM, NOR flash or OTP ROM is also often included on chip, as well as a small amount of RAM. Microcontrollers are designed for embedded applications, in contrast to the microprocessors used in personal computers or other general purpose applications consisting of various discrete chips. Examples: Arduino, ESP32.

## Pros and Cons

Single-board computers are more powerful, but take longer to start up, and require an orderly shutdown.  Microcontrollers are "instant-on" and have a smaller footprint, but are much more resource-constrained.

# IoT in .NET

The .NET IoT libraries provide support for direct GPIO programming, and also [device bindings](https://github.com/dotnet/iot/blob/main/src/devices/README.md?WT.mc_id=dotnet-35129-website) for a comprehensive set of sensors, motor controllers, motion sensors, displays, and more.

The libraries require at least ARM v7, meaning they will _not_ work on SBCs like the Raspberry Pi Zero.  They support SBC hardware, not microcontrollers: If you are interested in microcontroller programming in .NET, check out [.NET nanoFramework](https://www.nanoframework.net/).

# Links

* [https://aws.amazon.com/what-is/iot/](https://aws.amazon.com/what-is/iot/)
* [https://en.wikipedia.org/wiki/Microcontroller](https://en.wikipedia.org/wiki/Microcontroller)
* [https://dotnet.microsoft.com/en-us/apps/iot](https://dotnet.microsoft.com/en-us/apps/iot)
