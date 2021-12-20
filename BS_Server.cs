using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

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


   
        public string GetPosition(int[] outPos)
        {
           
            int received = clientSocket.Receive(data);
            string position = Encoding.ASCII.GetString(data, 0, received).ToLower();
            if(!GetCoord(position,outPos))
            {
                Console.WriteLine("Bad input from client. Aborting...");
                return "";
            }

            return position;
        }

        public void SendResponse(string message)
        {
            if (clientSocket == null)
            {
                Console.WriteLine("Start the server before sending positions stupid");
                return;
            }


            data = Encoding.ASCII.GetBytes(message);
            clientSocket.Send(data);
        }

        public string GetResponse()
        {
             if (clientSocket == null)
             {
                Console.WriteLine("Start the server before sending positions stupid");
                return "" ;
             }

            int received = clientSocket.Receive(data);
            return Encoding.ASCII.GetString(data, 0, received).ToLower();

        }
        ~BS_Server()
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

        public bool GetCoord(string position, int [] coord)
        {
            if (position.Length < 2)
                return false;
            coord[1] = position[0] - 'a';
            coord[2] = int.Parse(position.Substring(1))-1;
            return true;
        }

    }
}

