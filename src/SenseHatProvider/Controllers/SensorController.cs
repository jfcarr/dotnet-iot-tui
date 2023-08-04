using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using SenseHatLib.Helpers;
using SenseHatLib.Models;
using SenseHatProvider.Services;

namespace SenseHatProvider.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorController : ControllerBase
{
	private readonly ILogger<SensorController> _logger;

	public SensorController(ILogger<SensorController> logger)
	{
		_logger = logger;
	}

	/// <summary>
	/// Return a boolean indicating that the service is accepting requests, and the Sense HAT can be initialized.
	/// </summary>
	[HttpGet("Heartbeat")]
	public bool GetHeartbeat()
	{
		var sensorManager = new SensorManager();

		return sensorManager.InitSenseHat();
	}

	/// <summary>
	/// Retrieve sensor data.
	/// </summary>
	/// <param name="measurementUnits"></param>
	[HttpGet("SensorData/{measurementUnits}")]
	public SensorResult GetSensorData(MeasurementUnits measurementUnits)
	{
		var sensorManager = new SensorManager();

		var sensorData = sensorManager.GetCurrentSensorData(measurementUnits);

		return sensorData;
	}

	[HttpPut("SetLedWhite")]
	public string SetLedWhite()
	{
		var sensorManager = new SensorManager();

		var result = sensorManager.SetLed(Color.White);

		return result;
	}

	[HttpPut("SetLedRed")]
	public string SetLedRed()
	{
		var sensorManager = new SensorManager();

		var result = sensorManager.SetLed(Color.Red);

		return result;
	}
}
