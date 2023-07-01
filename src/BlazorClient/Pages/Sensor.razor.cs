using SenseHatLib.Services;

namespace BlazorClient.Pages
{
	public partial class Sensor
	{
		private string? currentTemperature;
		private string? currentHumidity;
		private string? currentAltitude;
		private string? statusMessage;

		private void GetSensorData()
		{
			try
			{
				var senseHatClient = new SenseHatClient("http", "192.168.0.140", 5191);
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
				var senseHatClient = new SenseHatClient("http", "192.168.0.140", 5191);

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