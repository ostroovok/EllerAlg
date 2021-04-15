using System;
using EllerAlg;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //MazeCreator maze = new MazeCreator(4, 8);
            //Cell[][] m = maze.Generate();
            //while (true)
            Maze maze = Eller.Generate(16, 8);

            for (int i = 0; i < 16; i++)
            {
                Console.Write($"__");
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(".");
                Console.ForegroundColor = color;
            }
            Console.WriteLine();
            var k = 0;
            foreach (var c in maze.cells)
            {
                k++;
                if (c.down)
                {
                    Console.Write("__");
                }
                else
                {
                    Console.Write("  ");
                }
                if (c.right)
                {
                    Console.Write("|");
                }
                else
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(".");
                    Console.ForegroundColor = color;
                    
                        
                }

                if (k == 16)
                {
                    Console.WriteLine();
                    k = 0;
                }

            }
        }
    }
}
