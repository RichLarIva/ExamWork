using System;
using System.Net.Sockets;

namespace ExamWork
{
	public class SocketManagement
	{
		private Socket _socket;
		private string _ipEndPoint;
		private int _port;

		public string IPEndPoint { get => _ipEndPoint; set => _ipEndPoint = value; }
		public int Port { get => _port; set => _port = value; }

		public void Connect()
		{
            _socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
			try
			{
				_socket.Connect(_ipEndPoint, _port);
			}
			catch(Exception err)
			{
				Console.WriteLine("Couldn't connect");
			}

        }


	}
}

