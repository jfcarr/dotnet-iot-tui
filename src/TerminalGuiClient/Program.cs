using System.Text.Json;
using System.Timers;
using Microsoft.Extensions.Configuration;
using SenseHatLib.Helpers;
using SenseHatLib.Models;
using SenseHatLib.Services;
using Terminal.Gui;

internal class Program
{
	private static void Main(string[] args)
	{
		Application.Run<ExampleWindow>();

		Application.Shutdown();
	}
}

/// <summary>
/// Terminal.Gui handler
/// </summary>
public class ExampleWindow : Window
{
	private SenseHatClient senseHatClient;
	private System.Timers.Timer servicePollTimer;
	private Label temperatureLabel, temperatureValue;
	private Label humidityLabel, humidityValue;
	private Label altitudeLabel, altitudeValue;
	private Label statusLabel, statusValue;
	private CheckBox pollService;
	private Label unitsLabel;
	private ComboBox unitsCombo;
	private Button btnGetSensorData, btnSetLedWhite, btnSetLedMulti;

	/// <summary>
	/// Initialize the Terminal.Gui application.
	/// </summary>
	public ExampleWindow()
	{
		var serviceSettings = new ServiceSettings();

		senseHatClient = new SenseHatClient(serviceSettings.UrlPrefix, serviceSettings.IpAddress, serviceSettings.Port);

		servicePollTimer = new System.Timers.Timer(serviceSettings.PollingIntervalInSeconds * 1000);

		Title = "Sensor Dashboard (Ctrl+Q to quit)";

		temperatureLabel = new Label()
		{
			Text = "Temperature:",
			Y = 1,  // row
			X = 1,  // column
		};

		temperatureValue = new Label()
		{
			Text = "---",
			Y = 1,
			X = 15,
		};

		humidityLabel = new Label()
		{
			Text = "Humidity:",
			Y = 2,
			X = 1,
		};

		humidityValue = new Label()
		{
			Text = "---",
			Y = 2,
			X = 15,
		};

		altitudeLabel = new Label()
		{
			Text = "Altitude:",
			Y = 3,
			X = 1,
		};

		altitudeValue = new Label()
		{
			Text = "---",
			Y = 3,
			X = 15,
		};

		statusLabel = new Label()
		{
			Text = "Status:",
			Y = 5,
			X = 1,
		};

		statusValue = new Label()
		{
			Text = "Ready",
			Y = 5,
			X = 15,
		};

		pollService = new CheckBox("Poll the service automatically")
		{
			Y = 7,
			X = 1
		};

		unitsLabel = new Label()
		{
			Text = "Units:",
			Y = 9,
			X = 1,
		};
		var items = new List<string>() { "Metric", "Imperial" };
		unitsCombo = new ComboBox()
		{
			Y = 9,
			X = 15,
			Height = Dim.Fill(3),
			Width = Dim.Percent(30),
			HideDropdownListOnClick = true
		};
		unitsCombo.SetSource(items);
		unitsCombo.SelectedItem = 1;

		btnGetSensorData = new Button()
		{
			Text = "Get Sensor Data",
			Y = unitsCombo.Y + 2,
			X = 1,
			IsDefault = false,
		};

		btnSetLedWhite = new Button()
		{
			Text = "Set LED White",
			Y = btnGetSensorData.Y,
			X = Pos.Right(btnGetSensorData) + 1,
			IsDefault = false,
		};

		btnSetLedMulti = new Button()
		{
			Text = "Set LED Multi",
			Y = btnGetSensorData.Y,
			X = Pos.Right(btnSetLedWhite) + 1,
			IsDefault = false,
		};

		servicePollTimer.Elapsed += (Object source, ElapsedEventArgs e) =>
		{
			if (pollService.Checked)
			{
				UpdateSensorDisplay();
			}
		};
		servicePollTimer.Enabled = true;

		btnGetSensorData.Clicked += () =>
		{
			UpdateSensorDisplay();
		};

		btnSetLedWhite.Clicked += () =>
		{
			statusValue.Text = senseHatClient.SetLed(true);
		};

		btnSetLedMulti.Clicked += () =>
		{
			statusValue.Text = senseHatClient.SetLed(false);
		};

		Add(
			temperatureLabel, temperatureValue,
			humidityLabel, humidityValue,
			altitudeLabel, altitudeValue,
			statusLabel, statusValue,
			pollService,
			unitsLabel, unitsCombo,
			btnGetSensorData, btnSetLedWhite, btnSetLedMulti
		);

		btnGetSensorData.SetFocus();
	}

	/// <summary>
	/// Populate the UI elements with sensor data.
	/// </summary>
	private void UpdateSensorDisplay()
	{
		var result = senseHatClient.GetSensorData((unitsCombo.SelectedItem == 1) ? MeasurementUnits.Imperial : MeasurementUnits.Metric);

		temperatureValue.Text = result.Data.FormattedTemperature;
		humidityValue.Text = result.Data.FormattedHumidity;
		altitudeValue.Text = result.Data.FormattedAltitude;

		statusValue.Text = $"[{DateTime.Now.ToString("h:mm:ss tt")}] {((string.IsNullOrEmpty(result.Status.ErrorMessage)) ? "Success" : result.Status.ErrorMessage)}";
	}
}