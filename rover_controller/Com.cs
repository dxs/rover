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
		public async Task<bool> ComConnect(string host, string port)
		{
			try
			{
				clientSocket = new StreamSocket();
				HostName serverHost = new HostName(host);
				string serverPort = port;
				await clientSocket.ConnectAsync(serverHost, serverPort);
				return true;
			}
			catch (Exception ex) { ex.ToString(); ComDisconnect(); return false; }
		}

		/// <summary>
		/// Send data stream from opened socket
		/// </summary>
		/// <param name="input"></param>
		public async void ComWrite(string input)
		{
			try
			{
				Stream streamOut = clientSocket.OutputStream.AsStreamForWrite();
				StreamWriter writer = new StreamWriter(streamOut);
				await writer.WriteLineAsync(input);
				await writer.FlushAsync();
			}
			catch (Exception e) { e.ToString(); ComDisconnect(); }
		}

		/// <summary>
		/// Read data stream from opened socket
		/// </summary>
		/// <returns>string containing the received line</returns>
		public async Task<string> ComRead()
		{
			string read = "::";
			try
			{
				Stream streamIn = clientSocket.InputStream.AsStreamForRead();
				StreamReader reader = new StreamReader(streamIn);
				read = await reader.ReadLineAsync();
			}
			catch (Exception e) { e.ToString(); ComDisconnect(); }
			return read;
		}

		/// <summary>
		/// Close the connection
		/// </summary>
		/// <returns>True if correctly disconnected</returns>
		public bool ComDisconnect()
		{
			clientSocket.Dispose();
			return true;
		}
	}
}
