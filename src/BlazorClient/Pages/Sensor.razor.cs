using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SenseHatLib.Services;

namespace BlazorClient.Pages
{
	public sealed class SensorServiceSettings
	{
		public required string UrlPrefix { get; set; }
		public required string IpAddress { get; set; }
		public required int Port { get; set; }
	}

	public partial class Sensor
	{
		[Inject]
		private IConfiguration? configuration { get; set; }
		private SensorServiceSettings sensorServiceSettings;

		private string? currentTemperature;
		private string? currentHumidity;
		private string? currentAltitude;
		private string? statusMessage;

		private SensorServiceSettings GetSettings()
		{
			return new SensorServiceSettings()
			{
				UrlPrefix = configuration.GetValue<string>("SensorService:UrlPrefix"),
				IpAddress = configuration.GetValue<string>("SensorService:IpAddress"),
				Port = configuration.GetValue<int>("SensorService:Port")
			};
		}

		private SenseHatClient GetSenseHatClient()
		{
			sensorServiceSettings = GetSettings();

			return new SenseHatClient(sensorServiceSettings.UrlPrefix, sensorServiceSettings.IpAddress, sensorServiceSettings.Port);
		}

		private void GetSensorData()
		{
			try
			{
				var senseHatClient = GetSenseHatClient();

				var result = senseHatClient.GetSensorData(SenseHatLib.Helpers.MeasurementUnits.Imperial);

				currentTemperature = result.Data.FormattedTemperature;
				currentHumidity = result.Data.FormattedHumidity;
				currentAltitude = result.Data.FormattedAltitude;

				statusMessage = (string.IsNullOrEmpty(result.Status.ErrorMessage)) ? "OK" : result.Status.ErrorMessage;
			}
			catch (System.Exception ex)
			{
				statusMessage = ex.Message;
			}
		}

		private void SetLed(bool clearDisplay)
		{
			try
			{
				var senseHatClient = GetSenseHatClient();

				senseHatClient.SetLed(clearDisplay);

				statusMessage = "OK";
			}
			catch (System.Exception ex)
			{
				statusMessage = ex.Message;
			}
		}
	}
}