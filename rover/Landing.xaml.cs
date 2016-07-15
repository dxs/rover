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
using Windows.Networking.Sockets;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace rover
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		Status roverStatus;
		StreamSocketListener serverSocket;
		public MainPage()
		{
			this.InitializeComponent();
			//roverStatus = new Status();
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += Timer_Tick;
			//timer.Start();
			StartServer();
		}

		private async void StartServer()
		{
			try
			{
				serverSocket = new StreamSocketListener();
				serverSocket.ConnectionReceived += ServerSocket_ConnectionReceived;
				await serverSocket.BindServiceNameAsync("5464");
			}
			catch(Exception e)
			{
				val.Text = e.ToString();
			}
		}

		private async void ServerSocket_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
		{
			val.Text = "CONNECTED";
			Stream inputStream = args.Socket.InputStream.AsStreamForRead();
			StreamReader reader = new StreamReader(inputStream);
			string request = await reader.ReadLineAsync();

			Stream outputStream = args.Socket.OutputStream.AsStreamForWrite();
			StreamWriter writter = new StreamWriter(outputStream);
			await writter.WriteLineAsync("Ok");
			await writter.FlushAsync();
		}

		private void Timer_Tick(object sender, object e)
		{
			roverStatus.update();
			xBox.Text = roverStatus.Xaxis.ToString();
			yBox.Text = roverStatus.Yaxis.ToString();
		}
	}
}
