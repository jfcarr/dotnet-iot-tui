using SenseHatLib.Services;
using System.Drawing;

namespace BlazorClient.Pages
{
	public partial class Sensor
	{
		private string? currentTemperature;
		private string? currentHumidity;
		private string? currentAltitude;
		private string? statusMessage;

		private SenseHatClient GetSenseHatClient()
		{
			var serviceSettings = new ServiceSettings(
				sectionName: "SensorService",
				urlPrefixPropertyName: "UrlPrefix",
				ipAddressPropertyName: "IpAddress",
				portPropertyName: "Port"
			);

			return new SenseHatClient(serviceSettings.UrlPrefix, serviceSettings.IpAddress, serviceSettings.Port);
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

		private void SetLed(Color color)
		{
			try
			{
				var senseHatClient = GetSenseHatClient();

				statusMessage = senseHatClient.SetLed(color);
			}
			catch (System.Exception ex)
			{
				statusMessage = ex.Message;
			}
		}
	}
}