using SenseHatLib.Models;
using System.Drawing;
using SenseHatLib.Services;

internal class Program
{
	private static void Main(string[] args)
	{
		var serviceSettings = new ServiceSettings();

		if (args.Length == 1)
		{
			var myService = new SenseHatClient(
				serviceSettings.UrlPrefix,
				serviceSettings.IpAddress,
				serviceSettings.Port
			);

			SensorResult results;

			switch (args[0].ToLower())
			{
				case "temperature":
					results = myService.GetSensorData(SenseHatLib.Helpers.MeasurementUnits.Imperial);
					Console.WriteLine((results.Status.IsValid) ? $"Temperature is {results.Data.FormattedTemperature}" : results.Status.ErrorMessage);
					break;

				case "humidity":
					results = myService.GetSensorData(SenseHatLib.Helpers.MeasurementUnits.Imperial);
					Console.WriteLine((results.Status.IsValid) ? $"Humidity is {results.Data.FormattedHumidity}" : results.Status.ErrorMessage);
					break;

				case "altitude":
					results = myService.GetSensorData(SenseHatLib.Helpers.MeasurementUnits.Imperial);
					Console.WriteLine((results.Status.IsValid) ? $"Altitude is {results.Data.FormattedAltitude}" : results.Status.ErrorMessage);
					break;

				case "led-white":
					var result1 = myService.SetLed(Color.White);
					Console.WriteLine(result1);
					break;

				case "led-red":
					var result2 = myService.SetLed(Color.Red);
					Console.WriteLine(result2);
					break;

				default:
					ShowHelpMessage();
					break;
			}
		}
		else
		{
			ShowHelpMessage();
		}
	}

	private static void ShowHelpMessage()
	{
		Console.WriteLine("Not sure what you want to do.  You can use any of these arguments: 'temperature', 'humidity', 'altitude', 'led-red', or 'led-white'");
	}
}
