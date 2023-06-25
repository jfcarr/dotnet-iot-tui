namespace SenseHatLib.Helpers
{
	public static class ConversionMethods
	{
		/// <summary>
		/// Given a value in Celsius, return the Fahrenheit equivalent.
		/// </summary>
		/// <param name="inputValue"></param>
		public static int CelsiusToFahrenheit(this int inputValue)
		{
			return Convert.ToInt32((inputValue * 1.8) + 32);
		}

		/// <summary>
		/// Given a value in Meters, return the equivalent in Feet.
		/// </summary>
		/// <param name="inputValue"></param>
		public static int MetersToFeet(this int inputValue)
		{
			return Convert.ToInt32(inputValue * 3.28084);
		}
	}
}