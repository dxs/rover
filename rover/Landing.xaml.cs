using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Streams;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace rover
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		bool test = false;
        public MainPage()
		{
			this.InitializeComponent();
        }

		private void ClickMe_Click(object sender, RoutedEventArgs e)
		{
			send();
			
		}

		private constructByte()
		{

			return;
		}

		private async void send()
		{
			// Get a selector string for bus "I2C1"
			string aqs = I2cDevice.GetDeviceSelector("I2C1");

			// Find the I2C bus controller with our selector string
			var dis = await DeviceInformation.FindAllAsync(aqs);
			if (dis.Count == 0)
				return; // bus not found

			// 0x40 is the I2C device address
			var settings = new I2cConnectionSettings(0x07);

			// Create an I2cDevice with our selected bus controller and I2C settings
			using (I2cDevice device = await I2cDevice.FromIdAsync(dis[0].Id, settings))
			{
				byte[] writeBuf = { 0x01, 0x02, 0x03, 0x04 };
				device.Write(writeBuf);
			}

		}
	}
}
