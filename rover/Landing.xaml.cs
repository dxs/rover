﻿using System;
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

namespace rover
{
    public sealed partial class MainPage : Page
    {
		Status roverStatus;
		Server server;
		public MainPage()
		{
			this.InitializeComponent();
			//roverStatus = new Status();
			server = new Server("5678");
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(50);
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		private void Timer_Tick(object sender, object e)
		{
			if (server.parse != null)
			{
				pitchBox.Text = server.parse[0];
				rollBox.Text = server.parse[1];
				yawBox.Text = server.parse[2];
			}
		}
	}
}
