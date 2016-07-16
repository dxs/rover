using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace rover
{
	class Server
	{
		StreamSocketListener serverSocket;
		public string[] parse { get; set; }
		private string port;

		public Server(string _port)
		{
			port = _port;
		}

		public async void Start(string port)
		{
			try
			{
				serverSocket = new StreamSocketListener();
				serverSocket.ConnectionReceived += ServerSocket_ConnectionReceived;
				await serverSocket.BindServiceNameAsync(port);
			}
			catch (Exception e)
			{
				e.ToString();
			}
		}

		private async void ServerSocket_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
		{
			while (true)
			{
				try
				{
					Stream inputStream = args.Socket.InputStream.AsStreamForRead();
					StreamReader reader = new StreamReader(inputStream);
					string request = await reader.ReadLineAsync();

					parse = request.Split(':');

					Stream outputStream = args.Socket.OutputStream.AsStreamForWrite();
					StreamWriter writter = new StreamWriter(outputStream);
					await writter.WriteLineAsync("Ok");
					await writter.FlushAsync();
				}
				catch (Exception exp) { serverSocket.Dispose(); }
			}
		}
	}
}
