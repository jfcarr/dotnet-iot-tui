using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using SenseHatLib.Services;
using Spectre.Console;
using Spectre.Console.Cli;

internal class Program
{
	private static void Main(string[] args)
	{
		var app = new CommandApp<SensorCommand>();

		app.Run(args);
	}
}

/// <summary>
/// Holds settings retrieved from appsettings.json
/// </summary>
public sealed class AppSettings
{
	public required string ServiceUrlPrefix { get; set; }
	public required string ServiceIpAddress { get; set; }
	public required int ServicePort { get; set; }
	public required int PollingIntervalInSeconds { get; set; }
}

internal sealed class SensorCommand : Command<SensorCommand.Settings>
{
	public sealed class Settings : CommandSettings
	{
		[Description("Action to take. Can be 'retrieve' or 'send'.")]
		[CommandArgument(0, "[action]")]
		public string Action { get; init; }

		[Description("Type of command. If action is 'retrieve', then command type can be 'temperature', 'humidity', 'altitude', or 'all'. For 'send', the type can be 'led-multi' or 'led-white'.")]
		[CommandArgument(1, "[command]")]
		[DefaultValue("Not Specified")]
		public string CommandType { get; init; }
	}

	public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
	{
		IConfiguration config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables()
			.Build();

		var appSettings = config.GetRequiredSection("Settings").Get<AppSettings>();

		if (settings.Action.ToLower() == "retrieve")
		{
			string[] validCommandTypes = { "temperature", "humidity", "altitude", "all" };

			if (Array.IndexOf(validCommandTypes, settings.CommandType.ToLower()) >= 0)
			{
				ExecActionRetrieve(settings.CommandType.ToLower(), appSettings);
			}
			else
			{
				AnsiConsole.MarkupLineInterpolated($"[red]Unknown command type '{settings.CommandType}' for '{settings.Action}'[/]");
			}
		}

		if (settings.Action.ToLower() == "send")
		{
			string[] validCommandTypes = { "led-multi", "led-white" };

			if (Array.IndexOf(validCommandTypes, settings.CommandType.ToLower()) >= 0)
			{
				ExecActionSend(settings.CommandType.ToLower(), appSettings);
			}
			else
			{
				AnsiConsole.MarkupLineInterpolated($"[red]Unknown command type '{settings.CommandType}' for '{settings.Action}'[/]");
			}
		}

		return 0;
	}

	private void ExecActionRetrieve(string commandType, AppSettings appSettings)
	{
		var senseHatClient = new SenseHatClient(appSettings.ServiceUrlPrefix, appSettings.ServiceIpAddress, appSettings.ServicePort);
		var results = senseHatClient.GetSensorData(SenseHatLib.Helpers.MeasurementUnits.Imperial);

		if (results.Status.IsValid)
		{
			var table = new Table();
			table.AddColumn("Sensor Data Type");
			table.AddColumn("Value");

			switch (commandType)
			{
				case "temperature":
					table.AddRow(commandType, results.Data.FormattedTemperature);
					break;

				case "humidity":
					table.AddRow(commandType, results.Data.FormattedHumidity);
					break;

				case "altitude":
					table.AddRow(commandType, results.Data.FormattedAltitude);
					break;

				case "all":
					table.AddRow("altitude", results.Data.FormattedAltitude);
					table.AddRow("humidity", results.Data.FormattedHumidity);
					table.AddRow("temperature", results.Data.FormattedTemperature);
					break;

				default:
					break;
			}

			AnsiConsole.Write(table);
		}
		else
		{
			AnsiConsole.MarkupLineInterpolated($"[red]{results.Status.ErrorMessage}[/]");
		}
	}

	private void ExecActionSend(string commandType, AppSettings appSettings)
	{
		var senseHatClient = new SenseHatClient(appSettings.ServiceUrlPrefix, appSettings.ServiceIpAddress, appSettings.ServicePort);

		var result = string.Empty;
		switch (commandType)
		{
			case "led-white":
				result = senseHatClient.SetLed(true);
				break;

			case "led-multi":
				result = senseHatClient.SetLed(false);
				break;

			default:
				break;
		}
		if (result == "Success")
			AnsiConsole.MarkupLineInterpolated($"[green]{result}[/]");
		else
			AnsiConsole.MarkupLineInterpolated($"[red]{result}[/]");
	}
}