using System;
namespace src
{
	public class BS_Server
	{
		private Socket passiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		private Socket clientSocket;
		private  byte[] data = new byte[1024];

		private static int port = 1000;

		public BS_Server()
		{
           
        }

        public void StartServer()
        {
         
            passiveSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            passiveSocket.Bind(new System.Net.IPEndPoint(IPAddress.Any, port));
            passiveSocket.Listen(1024);
            Console.WriteLine("Searching for players...");
            clientSocket = passiveSocket.Accept();
            IPEndPoint EP = (IPEndPoint)clientSocket.RemoteEndPoint;
            Console.WriteLine("A new challenger ! Connected to host " + EP.Address);


        }


        public void SendPosition(int X, int Y)
        {

        }

        public void GetPosition(int *X, int *Y)
        {

        }

        public ~BS_Server()
        {

            if (passiveSocket != null)
            {
                passiveSocket.Close();
            }
            if (clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }



    }
}

