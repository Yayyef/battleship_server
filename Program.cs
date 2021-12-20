using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace src
{
    internal class Program
    {
        public static Socket passiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static Socket clientSocket;
        public static byte[] data = new byte[1024];

        private static int port = 1000;

        static void Main(string[] args)
        {

            //GuessingGrid g = new GuessingGrid(10, 10);
            ShipGrid battleField = new ShipGrid(10, 10);

            List<Ship> troop = new List<Ship>()
            {
            new Ship(3), new Ship(4), new Ship(5)
            };
            bool isOutOfBounds = true;

            //boucle pour placer les bateau
            // i the number of ships
            for (int i = 0; i < troop.Count; i++)
            {
                do
                {
                    Console.WriteLine("Please enter the position of ship n°" + (i + 1) + ". It takes " + troop[i].length + " square(s)");
                    string position = Console.ReadLine().ToLower();
                    if (position.Length != 2)
                    {
                        Console.WriteLine("Incorrect format");
                        continue;
                    }
                    int firstCoord = position[0] - 'a', secondCoord = position[1] - '1';
                    Console.WriteLine("In what direction should it lay ?");
                    string direction = Console.ReadLine().ToLower();
                    isOutOfBounds = !battleField.AddShip(troop[i], firstCoord, secondCoord, direction);

                } while (isOutOfBounds);


            }

            //battleField.Display();

            //Console.CancelKeyPress += delegate
            //{
            //    if (passiveSocket != null)
            //    {
            //        passiveSocket.Close();
            //    }
            //    if (clientSocket != null)
            //    {
            //        clientSocket.Shutdown(SocketShutdown.Both);
            //        clientSocket.Close();
            //    }
            //};

            //passiveSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //passiveSocket.Bind(new System.Net.IPEndPoint(IPAddress.Any, port));
            //passiveSocket.Listen(1024);
            //Console.WriteLine("Searching for players...");
            //clientSocket = passiveSocket.Accept();
            //IPEndPoint EP = (IPEndPoint)clientSocket.RemoteEndPoint;
            //Console.WriteLine("A new challenger ! Connected to host " + EP.Address);

            //int clientShips = 3;

            //while (clientShips > 0 && troop.Count > 0)
            //{
            //    game loop
            //}


;
        }
    }
}