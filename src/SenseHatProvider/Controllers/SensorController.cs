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

	/// <summary>
	/// Manipulate LED pad.
	/// </summary>
	/// <param name="clearDisplay"></param>
	[HttpPut("RefreshLed/{clearDisplay}")]
	public string RefreshLed(bool clearDisplay)
	{
		var sensorManager = new SensorManager();

		var result = sensorManager.RefreshLed(clearDisplay);

		return result;
	}
}
