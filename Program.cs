using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace src
{
    internal class Program
    {

        public static int[] coordinates = new int[2];

        static void Main(string[] args)
        {

            GuessingGrid g = new GuessingGrid(10, 10);
            ShipGrid battleField = new ShipGrid(10, 10);

            List<Ship> myTroops = new List<Ship>()
            {
            new Ship(3), new Ship(4), new Ship(5)
            };

            bool isOutOfBounds = true;

            //boucle pour placer les bateau
            // i the ship position
            for (int i = 0; i < myTroops.Count; i++)
            {
                do
                {
                    Console.WriteLine("Please enter the position of ship n°" + (i + 1) + ". It takes " + myTroops[i].length + " square(s)");
                    string position = Console.ReadLine().ToLower();
                    if (position.Length < 2)
                    {
                        Console.WriteLine("Incorrect format");
                        continue;
                    }
                    int firstCoord = position[0] - 'a', secondCoord = position[1] - '1';
                    Console.WriteLine("In what direction should it lay ?");
                    string direction = Console.ReadLine().ToLower();
                    isOutOfBounds = !battleField.AddShip(myTroops[i], firstCoord, secondCoord, direction);

                } while (isOutOfBounds);


            }

            battleField.Display();

            BS_Server server = new BS_Server();
            server.StartServer();

            int clientShips = 3;

            while (clientShips > 0 && myTroops.Count > 0)
            {
                string enemyChoice = server.GetPosition(coordinates);
                Console.WriteLine("The ennemy is firing at " + enemyChoice);
                Ship s = battleField.grid[coordinates[1], coordinates[0]];
                if (s != null)
                {
                    battleField.grid[coordinates[1], coordinates[0] = null;
                    s.health--;
                    battleField.Display();
                    if (s.health == 0)
                    {
                        Console.WriteLine("... and BOOM you got hit !");
                        server.SendResponse("sunk");
                        myTroops.Remove(s);
                        Console.WriteLine("your ship got destroyed :'(");
                        Console.WriteLine("you have " + myTroops.Count + " ship(s) left");

                    }
                    else
                    {
                        Console.WriteLine("... and BOOM you got hit !");
                        server.SendResponse("hit");
                    }

                }

                else
                {
                    battleField.Display();
                    Console.WriteLine("... and he missed ! What a loser");
                    server.SendResponse("missed");
                }


            }



        }

    }
}