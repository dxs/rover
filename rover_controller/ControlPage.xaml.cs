using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace rover_controller
{
    public sealed partial class MainPage : Page
    {
		Com com;
		bool isConnected = false;
		Accelerometer accel;
		string ip = "192.168.1.110";
		string port = "5678";
		double pitch = 0.0;
		double speed = 0;

		public MainPage()
        {
            this.InitializeComponent();
			ip = ipbox.Text;
			com = new Com();
			sliderSpeed.ValueChanged += SliderSpeed_ValueChanged;
			commandBarConnection.Icon = new FontIcon
			{
				FontFamily = new FontFamily("Segoe MDL2 Assets"),
				Glyph = "\uE952"
			};
			
			SetupAccelerometer();
			RequestScreenLight();

			
        }

		private void RequestScreenLight()
		{
			var display = new DisplayRequest();
			try { display.RequestActive(); }
			catch (Exception ex) { ex.ToString(); }
			var screen = ApplicationView.GetForCurrentView();
			if (!screen.IsFullScreenMode)
				screen.TryEnterFullScreenMode();
		}

		private void SetupAccelerometer()
		{
			accel = Accelerometer.GetDefault();
			if (accel != null)
			{
				accel.ReportInterval = accel.MinimumReportInterval;
				accel.ReadingChanged += new TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs>(ReadingChanged);
			}
		}

		private async void ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
		{
			AccelerometerReading reading = null;

			await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
			{
				reading = args.Reading;
				pitchBox.Text = String.Format("{0,5:0.00000}", reading.AccelerationX); //this one concern us
				rollBox.Text = String.Format("{0,5:0.00000}", reading.AccelerationY);
				yawBox.Text = String.Format("{0,5:0.00000}", reading.AccelerationZ);
				string send = pitchBox.Text + ":" + rollBox.Text + ":" + yawBox.Text;
				if (isConnected)
				{
					com.ComWrite(send);
					string input = await com.ComRead();
					DisplayValue(input);
				}
				computeAccel(reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ);
				correctionBox.Text = String.Format("{0,5:0.00000}", pitch);
				tilt.Value = pitch;
			});	
		}

		private void computeAccel(double _pitch, double _roll, double _yaw)
		{
			if (speed == 0)
				speed = 1;
			pitch = _pitch;//en pourcent
			pitch *= 100;
			pitch *= 1 - Math.Abs(speed) / 150;
			if (pitch < -100)
				pitch = -100;
			if (pitch > 100)
				pitch = 100;
			pitch = (int)pitch;
		}

		private void DisplayValue(string input)
		{
			batteryBox.Text = input;
		}

		private void SliderSpeed_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			speed = (int)sliderSpeed.Value;
			speedBox.Text = sliderSpeed.Value.ToString();
		}

		private async void commandBarConnection_Click(object sender, RoutedEventArgs e)
		{
			commandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;
			if (isConnected == false)
				isConnected = await com.ComConnect(ip, port);
			else
				isConnected = !com.ComDisconnect();

			if (isConnected)
			{
				statusBox.Text = "Connected";
				statusBox.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);

				commandBarConnection.IsChecked = true;
				commandBarConnection.Content = "Disconnect";
				commandBarConnection.Label = "Disconnect";
				commandBarConnection.Icon = new FontIcon
				{
					FontFamily = new FontFamily("Segoe MDL2 Assets"),
					Glyph = "\uE106"
				};
			}
			else
			{
				statusBox.Text = "Disconnected" + com.error;
				statusBox.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
				commandBarConnection.IsChecked = false;
				commandBarConnection.Content = "Connect";
				commandBarConnection.Label = "Connect";
				commandBarConnection.Icon = new FontIcon
				{
					FontFamily = new FontFamily("Segoe MDL2 Assets"),
					Glyph = "\uE952"
				};
			}
		}

		private void commandBarSettings_Click(object sender, RoutedEventArgs e)
		{
			commandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;
		}

	}
}
