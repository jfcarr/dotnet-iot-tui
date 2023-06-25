using System.Text.Json;
using SenseHatLib.Helpers;
using SenseHatLib.Models;

namespace SenseHatLib.Services
{
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

		public SensorResult GetSensorData(MeasurementUnits measurementUnits = MeasurementUnits.Metric)
		{
			try
			{
				using HttpClient client = new();
				var task1 = Task.Run(() => client.GetStringAsync($"{_serviceUrlPrefix}://{_serviceIpAddress}:{_servicePort.ToString()}/Sensor/SensorData?measurementUnits={measurementUnits}"));
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

		public string SetLed(bool clear = false)
		{
			try
			{
				using HttpClient client = new();

				var clearDisplayValue = (clear) ? "true" : "false";
				var task1 = Task.Run(() => client.PutAsync($"{_serviceUrlPrefix}://{_serviceIpAddress}:{_servicePort.ToString()}/Sensor/RefreshLed?clearDisplay={clear}", null));
				task1.Wait();
				var response = task1.Result;

				return "Success";
			}
			catch (System.Exception ex)
			{
				return ex.Message;
			}
		}
	}
}