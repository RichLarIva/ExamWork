using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ExamWork
{
	public class ConnectionManager
	{
        private Socket _sendClient;

		public void Setup()
		{
            Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            

            sendSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3002));
            sendSocket.Listen(0);
            _sendClient = sendSocket.Accept();
        }


        private void SendData(string personName)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(personName);
            _sendClient.Send(buffer, 0, buffer.Length, 0);
        }

        public string ReturnQuery(string personName, string barCode, int scanCode)
        {
            SendData(personName);
            return ($"INSERT INTO LunchScans(`Barcode`, `ScanCode`) VALUES('{barCode}', '{scanCode}')");
        }


	}
}

