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

	[HttpGet("Heartbeat")]
	public bool GetHeartbeat()
	{
		return true;
	}

	[HttpGet("SensorData")]
	public SensorResult GetSensorData(MeasurementUnits measurementUnits = MeasurementUnits.Metric)
	{
		var sensorManager = new SensorManager();

		var sensorData = sensorManager.GetCurrentSensorData(measurementUnits);

		return sensorData;
	}

	[HttpPut("RefreshLed")]
	public string RefreshLed(bool clearDisplay = true)
	{
		var sensorManager = new SensorManager();

		var result = sensorManager.RefreshLed(clearDisplay);

		return result;
	}
}
