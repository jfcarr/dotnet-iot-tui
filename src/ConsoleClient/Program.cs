using Microsoft.Extensions.Configuration;
using SenseHatLib.Models;
using SenseHatLib.Services;

internal class Program
{
	private static void Main(string[] args)
	{
		IConfiguration config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables()
			.Build();

		if (args.Length == 1)
		{
			var myService = new SenseHatClient(
				config.GetValue<string>("Settings:ServiceUrlPrefix"),
				config.GetValue<string>("Settings:ServiceIpAddress"),
				config.GetValue<int>("Settings:ServicePort")
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
					myService.SetLed(true);
					break;

				case "led-multi":
					myService.SetLed(false);
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
		Console.WriteLine("Not sure what you want to do.  You can use any of these arguments: 'temperature', 'humidity', 'altitude', 'led-multi', or 'led-white'");
	}
}
