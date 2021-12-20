using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    internal class display
    {
        public  display (string[] args)

        {
            int gridSize = 10;
            int Row;
            int Column;
            Console.WriteLine("Welcome to the Battleship game");
            int[,] grid = new int[gridSize, gridSize];
            for (Row = 0; Row < gridSize; Row++)
            {

                Console.WriteLine();
                for (Column = 0; Column < gridSize; Column++)
                    Console.Write(grid[Column, Row] + " ");
            }
        }
    }
}
