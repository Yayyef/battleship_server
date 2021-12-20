using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace src
{
    internal class Program
    {

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

          
            int clientShips = 3;

            //while (clientShips > 0 && myTroops.Count > 0)
            //{
            //    game loop
            //}


;
        }
    }
}