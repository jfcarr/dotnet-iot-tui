using System.Drawing;
using System.Text.Json;
using SenseHatLib.Helpers;
using SenseHatLib.Models;

namespace SenseHatLib.Services
{
	/// <summary>
	/// Manages communication with the Sense HAT.
	/// </summary>
	public class SenseHatClient
	{
		private string _serviceUrlPrefix;
		private string _serviceIpAddress;
		private int _servicePort;

		public SenseHatClient(string serviceUrlPrefix, string serviceIpAddress, int servicePort)
		{
			_serviceUrlPrefix = serviceUrlPrefix;
			_serviceIpAddress = serviceIpAddress;
			_servicePort = servicePort;
		}

		/// <summary>
		/// Retrieve current sensor data.
		/// </summary>
		/// <param name="measurementUnits"></param>
		public SensorResult GetSensorData(MeasurementUnits measurementUnits = MeasurementUnits.Metric)
		{
			try
			{
				using HttpClient client = new();
				var task1 = Task.Run(() => client.GetStringAsync($"{_serviceUrlPrefix}://{_serviceIpAddress}:{_servicePort.ToString()}/Sensor/SensorData/{(int)measurementUnits}"));
				task1.Wait();
				var response = task1.Result;

				var sensorResult = JsonSerializer.Deserialize<SensorResult>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

				return sensorResult;

			}
			catch (System.Exception ex)
			{
				var sensorResult = new SensorResult();

				sensorResult.Status.ErrorMessage = ex.Message;
				sensorResult.Status.IsSynthetic = false;
				sensorResult.Status.IsValid = false;

				return sensorResult;
			}
		}

		/// <summary>
		/// Manipulate the LED pad.
		/// </summary>
		/// <param name="color"></param>
		public string SetLed(Color color)
		{
			try
			{
				using HttpClient client = new();

				var endpoint = string.Empty;

				if (color == Color.White) endpoint = "SetLedWhite";
				if (color == Color.Red) endpoint = "SetLedRed";

				if (endpoint != "")
				{
					var task1 = Task.Run(() => client.PutAsync($"{_serviceUrlPrefix}://{_serviceIpAddress}:{_servicePort.ToString()}/Sensor/{endpoint}", null));
					task1.Wait();
					var response = task1.Result;

					return "Success";
				}
				else
				{
					return "Unsupported color";
				}
			}
			catch (System.Exception ex)
			{
				return ex.Message;
			}
		}
	}
}