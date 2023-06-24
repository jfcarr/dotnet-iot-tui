namespace SenseHatLib.Models
{
	public class SensorResult
	{
		public SensorResult()
		{
			Data = new SensorData();
			Status = new SensorStatus();
		}

		public SensorData Data { get; set; }
		public SensorStatus Status { get; set; }
	}

	public class SensorData
	{
		public int Altitude { get; set; }
		public string AltitudeUnits { get; set; }

		public int Humidity { get; set; }

		public int Temperature { get; set; }
		public string TemperatureUnits { get; set; }
	}

	public class SensorStatus
	{
		public bool IsValid { get; set; }
		public bool IsSynthetic { get; set; }
		public string ErrorMessage { get; set; }
	}
}