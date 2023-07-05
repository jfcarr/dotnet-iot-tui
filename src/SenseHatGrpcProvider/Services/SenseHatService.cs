using Grpc.Core;
using Iot.Device.Common;
using Iot.Device.SenseHat;
using SenseHatLib.Helpers;
using SenseHatLib.Models;


namespace SenseHatGrpcProvider.Services
{
	public class SenseHatService : SenseHatConnector.SenseHatConnectorBase
	{
		private readonly ILogger<SenseHatService> _logger;
		public SenseHatService(ILogger<SenseHatService> logger)
		{
			_logger = logger;
		}

		public override Task<SensorDataReply> GetSensorData(SensorDataRequest request, ServerCallContext context)
		{
			MeasurementUnits measurementUnits;

			switch (request.MeasurementUnits)
			{
				case 1:
					measurementUnits = MeasurementUnits.Metric;
					break;
				case 2:
					measurementUnits = MeasurementUnits.Imperial;
					break;
				default:
					measurementUnits = MeasurementUnits.Imperial;
					break;
			}

			var sensorResult = GetCurrentSensorData(measurementUnits);

			return Task.FromResult(new SensorDataReply
			{
				Altitude = sensorResult.Data.Altitude,
				AltitudeUnits = sensorResult.Data.AltitudeUnits,
				FormattedAltitude = sensorResult.Data.FormattedAltitude,
				Humidity = sensorResult.Data.Humidity,
				FormattedHumidity = sensorResult.Data.FormattedHumidity,
				Temperature = sensorResult.Data.Temperature,
				TemperatureUnits = sensorResult.Data.TemperatureUnits,
				FormattedTemperature = sensorResult.Data.FormattedTemperature,
				IsValid = sensorResult.Status.IsValid,
				IsSynthetic = sensorResult.Status.IsSynthetic,
				ErrorMessage = sensorResult.Status.ErrorMessage
			});
		}

		private SensorResult GetCurrentSensorData(MeasurementUnits measurementUnits = MeasurementUnits.Metric)
		{
			var sensorResult = new SensorResult();

			try
			{
				using (SenseHat sh = new SenseHat())
				{

					sensorResult.Data.Altitude = Convert.ToInt32(WeatherHelper.CalculateAltitude(sh.Pressure, WeatherHelper.MeanSeaLevel, sh.Temperature));
					if (measurementUnits == MeasurementUnits.Imperial)
						sensorResult.Data.Altitude = sensorResult.Data.Altitude.MetersToFeet();
					sensorResult.Data.AltitudeUnits = (measurementUnits == MeasurementUnits.Imperial) ? "feet" : "meters";

					sensorResult.Data.Temperature = Convert.ToInt32(sh.Temperature);
					if (measurementUnits == MeasurementUnits.Imperial)
						sensorResult.Data.Temperature = sensorResult.Data.Temperature.CelsiusToFahrenheit();
					sensorResult.Data.TemperatureUnits = (measurementUnits == MeasurementUnits.Imperial) ? "fahrenheit" : "celsius";

					sensorResult.Data.Humidity = Convert.ToInt32(sh.Humidity);

					sensorResult.Status.IsValid = true;
					sensorResult.Status.ErrorMessage = string.Empty;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{ex.Source} {ex.Message}");

				sensorResult.Status.ErrorMessage = ex.Message;
				sensorResult.Status.IsValid = false;

				if (ex.Message.Contains("Error 13."))  // Sensor is unavailable. Generate synthetic data.
				{
					sensorResult.Data.Altitude = 500;
					if (measurementUnits == MeasurementUnits.Imperial)
						sensorResult.Data.Altitude = sensorResult.Data.Altitude.MetersToFeet();
					sensorResult.Data.AltitudeUnits = (measurementUnits == MeasurementUnits.Imperial) ? "feet" : "meters";

					sensorResult.Data.Temperature = 22;
					if (measurementUnits == MeasurementUnits.Imperial)
						sensorResult.Data.Temperature = sensorResult.Data.Temperature.CelsiusToFahrenheit();
					sensorResult.Data.TemperatureUnits = (measurementUnits == MeasurementUnits.Imperial) ? "fahrenheit" : "celsius";

					sensorResult.Data.Humidity = 55;
					sensorResult.Status.IsSynthetic = true;
				}
				else
				{
					sensorResult.Status.IsSynthetic = false;
				}
			}

			return sensorResult;
		}


	}
}