using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace rover_controller
{
	class Com
	{
		StreamSocket clientSocket;
		public Com()
		{

		}

		/// <summary>
		/// Use this to create a connection to a host
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		public async void ComConnect(string host, string port)
		{
			try
			{

				clientSocket = new StreamSocket();
				HostName serverHost = new HostName("192.168.1.118");
				string serverPort = "5464";
				await clientSocket.ConnectAsync(serverHost, serverPort);
			}
			catch (Exception ex) { ex.ToString(); }
		}

		/// <summary>
		/// Send data stream from opened socket
		/// </summary>
		/// <param name="input"></param>
		public async void ComWrite(string input)
		{
			Stream streamOut = clientSocket.OutputStream.AsStreamForWrite();
			StreamWriter writer = new StreamWriter(streamOut);
			await writer.WriteLineAsync(input);
			await writer.FlushAsync();
		}

		/// <summary>
		/// Read data stream from opened socket
		/// </summary>
		/// <returns>string containing the received line</returns>
		public async Task<string> ComRead()
		{
			Stream streamIn = clientSocket.InputStream.AsStreamForRead();
			StreamReader reader = new StreamReader(streamIn);
			string read = await reader.ReadLineAsync();
			return read;
		}
	}
}
