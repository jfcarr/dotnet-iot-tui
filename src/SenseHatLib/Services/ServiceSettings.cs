using Microsoft.Extensions.Configuration;

namespace SenseHatLib.Services
{
	public class ServiceSettings
	{
		private IConfiguration _config;

		private string _settingsFileName;
		private string _sectionName;
		private string _urlPrefixPropertyName;
		private string _ipAddressPropertyName;
		private string _portPropertyName;
		private string _pollingIntervalInSecondsPropertyName;

		public ServiceSettings(
			string settingsFileName = "appsettings.json",
			string sectionName = "Settings",
			string urlPrefixPropertyName = "ServiceUrlPrefix",
			string ipAddressPropertyName = "ServiceIpAddress",
			string portPropertyName = "ServicePort",
			string pollingIntervalInSecondsPropertyName = "PollingIntervalInSeconds"
		)
		{
			_settingsFileName = settingsFileName;

			_sectionName = sectionName;

			_urlPrefixPropertyName = urlPrefixPropertyName;
			_ipAddressPropertyName = ipAddressPropertyName;
			_portPropertyName = portPropertyName;
			_pollingIntervalInSecondsPropertyName = pollingIntervalInSecondsPropertyName;

			_config = new ConfigurationBuilder()
				.AddJsonFile(_settingsFileName)
				.AddEnvironmentVariables()
				.Build();
		}

		public string UrlPrefix { get { return _config.GetValue<string>($"{_sectionName}:{_urlPrefixPropertyName}"); } }

		public string IpAddress { get { return _config.GetValue<string>($"{_sectionName}:{_ipAddressPropertyName}"); } }

		public int Port { get { return _config.GetValue<int>($"{_sectionName}:{_portPropertyName}"); } }

		public int PollingIntervalInSeconds { get { return _config.GetValue<int>($"{_sectionName}:{_pollingIntervalInSecondsPropertyName}"); } }

	}
}