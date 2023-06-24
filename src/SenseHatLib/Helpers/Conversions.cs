namespace SenseHatLib.Helpers
{
	public static class ConversionMethods
	{
		public static int CelsiusToFahrenheit(this int inputValue)
		{
			return Convert.ToInt32((inputValue * 1.8) + 32);
		}

		public static int MetersToFeet(this int inputValue)
		{
			return Convert.ToInt32(inputValue * 3.28084);
		}
	}
}