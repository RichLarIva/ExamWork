using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace ExamWork
{
	public class Transmitter
	{
		private Socket _socket;
		private string _ipEndPoint;
		private int _port;
		private string _input = "";

		private TcpClient client;

		public string Input { get => _input; set => _input = value; }
		private IPEndPoint _epRemote;
		private IPEndPoint _epLocal;
		private byte[] _buffer;
        public string IPEndPoint { get => _ipEndPoint; set => _ipEndPoint = value; }
		public int Port { get => _port; set => _port = value; }
		Thread thread;

        public void Connect()
        {
            client = new TcpClient();
            client.Connect("127.0.0.1", 3000);
            NetworkStream clientStream = client.GetStream();

            Thread.Sleep(1000); // Sleep before getting the data
            while (clientStream.DataAvailable)
            {
                byte[] inmessage = new byte[4096];
                int bytesRead = 0;
                try
                {
                    bytesRead = clientStream.Read(inmessage, 0, 4096);
                }
                catch (Exception err)
                {

                }
                ASCIIEncoding encoder = new ASCIIEncoding();
                Console.WriteLine(encoder.GetString(inmessage, 0, bytesRead));
            }
            client.Close();
            Connect(_epRemote);
        }

        private void Connect(IPEndPoint _epRemote)
		{
            client = new TcpClient();
			client.Connect("127.0.0.1", 3000);
			NetworkStream clientStream = client.GetStream();

			Thread.Sleep(1000); // Sleep before getting the data
			while (clientStream.DataAvailable)
			{
				byte[] inmessage = new byte[4096];
				int bytesRead = 0;
				try
				{
					bytesRead = clientStream.Read(inmessage, 0, 4096);
				}
				catch(Exception err)
				{

				}
				ASCIIEncoding encoder = new ASCIIEncoding();
				Console.WriteLine(encoder.GetString(inmessage, 0, bytesRead));
			}
			client.Close();
   //         _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			//Socket sendClient;
			//_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
   //         thread = new Thread(_socket);
   //         thread.IsBackground = true;
   //         thread.Start();
   //         _epLocal = new IPEndPoint(IPAddress.Parse(_ipEndPoint), 8080);
   //         _socket.Bind(_epLocal);
			//_socket.Listen(0);
			//sendClient = _socket.Accept();
   //         // Connect to remote IP and port
   //         _epRemote = new IPEndPoint(IPAddress.Parse(_ipEndPoint), _port);
			//EndPoint tempRemoteEP = (EndPoint)_epRemote;
   //         try
			//{
   //             // Binding the socket to the application.
			//	_socket.Connect(_epRemote);
   //             _buffer = new byte[1500];
   //             _socket.BeginReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref tempRemoteEP, new AsyncCallback(MessageCallBack), _buffer);
   //             Console.WriteLine("Connected");
   //         }
			//catch(Exception err)
			//{
			//	Console.WriteLine("Couldn't connect");
			//}

        }

        private void MessageCallBack(IAsyncResult ar)
        {
			ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
			byte[] message = new byte[1500];
			message = encoding.GetBytes(_input);
			_socket.Send(message);
        }
    }
}

