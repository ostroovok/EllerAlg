using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EllerAlg
{
    public class MazeCreator
    {
        public int Width { get; }
        public int Height { get; private set; }
        public Cell[][] Maze { get; private set; }
        private Random _rnd;

        public MazeCreator(int width, int height)
        {
            Width = width;
            Height = height;
            _rnd = new Random();
        }

        public Cell[][] Generate()
        {
            Maze = new Cell[Height][];
            Cell[] temp = new Cell[Width]; // 1

            for (int j = 0; j < Width; j++)
            {
                temp[j] = new Cell(j);  // 2
            }

            Maze[0] = OneStr(temp);


            for (int i = 1; i < Height; i++)
            {
                Maze[i] = OneStr(CreateNewStr(Maze[i - 1]));
            }

            /*for (int i = 0; i < Maze[0].Length - 1; i++)
            {
                if (Maze.Last()[i].LotsOf == Maze.Last()[i + 1].LotsOf)
                    Maze.Last()[i].Right = true;
                else
                {
                    Maze.Last()[i].Right = false;
                    Maze.Last()[i+1].LotsOf = Maze.Last()[i].LotsOf;
                }    
            }*/

            for (int i = 0; i < Maze[0].Length - 1; i++)
                ToLastStr(new Cell[] { Maze.Last()[i], Maze.Last()[i + 1] }, i);

            foreach (var c in Maze.Last())
            {
                c.Bottom = true;
            }

            //PrintWithOutNumbers(0, Maze.Length, Maze);

            if (Console.ReadKey().Key == ConsoleKey.W)
            {
                Console.WriteLine();
                PrintWithNumbers(0, Maze.Length, Maze);
                Console.WriteLine();
                PrintWithOutNumbers(0, Maze.Length, Maze);
            }
            else
            {
                Console.WriteLine();
                PrintWithOutNumbers(0, Maze.Length, Maze);
            }
                
            return Maze;
        }

        private void ToLastStr(Cell[] input, int i)
        {
            if (input[0].LotsOf != input[1].LotsOf)
            {
                input[0].Right = false;
                input[1].LotsOf = input[0].LotsOf;
            }
        }

        private Cell[] CreateNewStr(Cell[] toInput)
        {
            var count = toInput.Length;
            var input = new Cell[toInput.Length];
            for (int i = 0; i < toInput.Length; i++)
            {
                if (!toInput[i].Bottom)
                {
                    input[i] = new Cell(toInput[i].LotsOf);
                    
                }
                else if(i < input.Length - 1)
                {
                    if (input.Length + i != toInput[i + 1].LotsOf && input.Length + i != toInput[Math.Max(0, i - 1)].LotsOf)
                    {
                        input[i] = new Cell(count + i);
                    }
                    else
                    {
                        count++;
                        input[i] = new Cell(count + i);
                    }
                }
            }

            //if (input[i].LotsOf == input[Math.Max(0, i - 1)].LotsOf && input[i].LotsOf != 0)
                //input[Math.Max(0, i - 1)].Right = true;


            if (input[input.Length - 1] == null)
                input[input.Length-1] = new Cell(count + input.Length - 1);

            for (int i = 0; i < input.Length-1; i++)
            {
                if (input[i].LotsOf == input[i + 1].LotsOf)
                    input[i].Right = true;
                
            }
            

            return input;
        }

        private Cell[] OneStr(Cell[] input)
        {

            input.Last().Right = true;

            //PrintWithNumbers(0, 1, new Cell[][] { input });

            for (int i = 0; i < input.Length-1; i++)  
            {
                if (_rnd.Next(2) == 0 || input[i].LotsOf == input[i + 1].LotsOf)
                    input[i].Right = true;
                else if (!input[i].Right)
                    input[i + 1].LotsOf = input[i].LotsOf;
            }

            var check = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (_rnd.Next(2) == 0)
                {
                    input[i].Bottom = true;
                }
                
            }

            for (int i = 0; i < input.Length; i++)
            {

                if (input[i].Right)
                {
                    for (int j = 0; j < input.Length; j++)
                    {
                        if (!input[j].Bottom && input[i].LotsOf == input[j].LotsOf && !input[j].Right)
                                check = true;
                    }
                    if (!check)
                        input[i].Bottom = false;
                }
                if(check == true)

                check = false;
            }
            //PrintWithNumbers(0, 1, new Cell[][] { input });
            return input;
        }


        public void PrintWithNumbers(int start, int h, Cell[][] maze)
        {
            for (int i = 0; i < maze[0].Length; i++)
            {
                //Console.Write($"___");
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(".");
                Console.ForegroundColor = color;
            }
            Console.WriteLine();
            for (int i = start; i < h; i++)
            {
                for (int j = 0; j < maze[0].Length; j++)
                {
                    if (maze[i][j].Bottom)
                    {
                        Console.Write($"_{maze[i][j].LotsOf}_");
                    }
                    else
                    {
                        Console.Write($" {maze[i][j].LotsOf} ");
                    }
                    if (maze[i][j].Right)
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
                }
                Console.WriteLine();
            }
        }
        public void PrintWithOutNumbers(int start, int h, Cell[][] maze)
        {
            for (int i = 0; i < maze[0].Length; i++)
            {
                Console.Write($"__");
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(".");
                Console.ForegroundColor = color;
            }
            Console.WriteLine();
            for (int i = start; i < h; i++)
            {
                for (int j = 0; j < maze[0].Length; j++)
                {
                    if (maze[i][j].Bottom)
                    {
                        Console.Write("__");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    if (maze[i][j].Right)
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
                }
                Console.WriteLine();
            }
        }
    }
}
/*
 * var input = new Cell[toInput.Length];
            for (int i = 0; i < toInput.Length; i++)
            {
                input[i] = toInput[i];
            }
            foreach (var c in input)
            {
                 c.Right = false;
            }
*/