using Grpc.Net.Client;
using SenseHatGrpcClient;
using SenseHatLib.Services;

internal class Program
{
	private static void Main(string[] args)
	{
		try
		{
			var serviceSettings = new ServiceSettings();

			using (var channel = GrpcChannel.ForAddress($"{serviceSettings.UrlPrefix}://{serviceSettings.IpAddress}:{serviceSettings.Port}"))
			{
				var client = new SenseHatConnector.SenseHatConnectorClient(channel);

				var result = client.GetSensorData(new SensorDataRequest { MeasurementUnits = 2 });  // Fahrenheit

				Console.WriteLine($"Altitude is {result.FormattedAltitude}");
				Console.WriteLine($"Humidity is {result.FormattedHumidity}");
				Console.WriteLine($"Temperature is {result.FormattedTemperature}");
				Console.WriteLine("");
				Console.WriteLine($"Data is {((result.IsValid) ? "valid" : "invalid")}.");
				Console.WriteLine("");
				Console.WriteLine($"Status: {((string.IsNullOrEmpty(result.ErrorMessage)) ? "OK" : result.ErrorMessage)}");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Client error: {ex.Message}");
		}
	}
}