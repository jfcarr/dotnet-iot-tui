using System.Drawing;
using Iot.Device.Common;
using Iot.Device.SenseHat;
using SenseHatLib.Helpers;
using SenseHatLib.Models;

namespace SenseHatProvider.Services
{
	public class SensorManager
	{
		public SensorResult GetCurrentSensorData(MeasurementUnits measurementUnits = MeasurementUnits.Metric)
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

		public string RefreshLed(bool clearDisplay)
		{
			try
			{
				using (SenseHat sh = new SenseHat())
				{
					if (clearDisplay)
					{
						sh.LedMatrix.Fill(Color.White);
					}
					else
					{
						sh.LedMatrix.Fill(Color.Purple);
						sh.LedMatrix.SetPixel(0, 0, Color.Red);
						sh.LedMatrix.SetPixel(1, 0, Color.Green);
						sh.LedMatrix.SetPixel(2, 0, Color.Blue);
					}
				}

				return "OK";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
