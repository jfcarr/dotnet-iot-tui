namespace SenseHatLib.Models
{
	/// <summary>
	/// Holds sensor data and status information.
	/// </summary>
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

	/// <summary>
	/// Holds sensor data.
	/// </summary>
	public class SensorData
	{
		public int Altitude { get; set; }
		public string AltitudeUnits { get; set; }
		public string FormattedAltitude
		{
			get
			{
				return $"{Altitude} {AltitudeUnits}";
			}
		}

		public int Humidity { get; set; }
		public string FormattedHumidity
		{
			get
			{
				return $"{Humidity}%";
			}
		}

		public int Temperature { get; set; }
		public string TemperatureUnits { get; set; }
		public string FormattedTemperature
		{
			get
			{
				return $"{Temperature}\u00B0 {TemperatureUnits}";
			}
		}
	}

	/// <summary>
	/// Holds status information.
	/// </summary>
	public class SensorStatus
	{
		/// <summary>
		/// Was the current data successfully retrieved from the Sense HAT?
		/// </summary>
		public bool IsValid { get; set; }

		/// <summary>
		/// Is the current data generated sample data, or is it from the actual Sense HAT?
		/// </summary>
		public bool IsSynthetic { get; set; }

		/// <summary>
		/// Latest error message, if applicable.
		/// </summary>
		public string ErrorMessage { get; set; }
	}
}