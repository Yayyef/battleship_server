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


        public void SendPosition(int X, int Y)
        {
            if(clientSocket==null)
            {
                Console.WriteLine("Start the server before sending positions stupid");
                return;
            }


            
        }

        public string GetPosition(int[] outPos)
        {
            do
            {
                int received = clientSocket.Receive(data);
                string position = Encoding.UTF8.GetString(data, 0, received).ToLower();
            } while (GetCoord(position, outPos);
            return position;
        }

        public void SendResponse(string message)
        {

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

