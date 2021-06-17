using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EllerAlg
{
    public class MazeGenerator
    {
        public int Width { get; }
        public List<ECell[]> Maze { get; private set; }

        private Random _rnd;

        private int[] right;
        private int[] bot;

        public MazeGenerator(int width)
        {
            Width = width;
            rowC = width;

            
            right = new int[Width];
            bot = new int[Width];

            for (int i = 0; i < Width; i++)
            {
                right[i] = i;
                bot[i] = i;
            }

            _rnd = new Random();

            Maze = new List<ECell[]>();

            Maze.Add(new ECell[Width]);
            for (int j = 0; j < Width; j++)
            {
                Maze.Last()[j] = new ECell();
            }
        }
        
        public void Generate()
        {
            for (int i = 0; i < Width; i++)
            {
                bot[i] = i;
            }
            var counter = 0;
            for(; ; )
            {
                Maze.Add(SecCreator(ref right, bot));
                PrintWithOutNumbers(0, 1, Maze.Last());
                Thread.Sleep(1000);
            }
        }
        #region Private Methods
        private ECell[] CreateOneRow(int i, int[] right, int[] bot)
        {
            var temp = new ECell[Width];

            for (int c = 0; c < Width; c++)
            {
                temp[c] = new ECell();
            }

            for (int j = 0; j < Width; j++)
            {
                if (j != Width - 1 && j + 1 != right[j] && _rnd.NextDouble() < 0.5)
                {
                    temp[j].Right = false;

                    right[bot[j + 1]] = right[j];

                    bot[right[j]] = bot[j + 1];

                    right[j] = j + 1;

                    bot[j + 1] = j;
                }

                if (j != right[j] && _rnd.NextDouble() < 0.5)
                {
                    right[bot[j]] = right[j];

                    bot[right[j]] = bot[j];

                    right[j] = j;

                    bot[j] = j;
                }
                else
                    temp[j].Bottom = false;
            }
            return temp;
        }
        public int rowC;
        private ECell[] SecCreator(ref int[] right, int[] bot)
        {
            var temp = new ECell[Width];

            for (int c = 0; c < Width; c++)
            {
                temp[c] = new ECell();
            }

            for (int i = 0; i < Width; i++)
            {
                if(i != Width - 1 && _rnd.NextDouble() > 0.5)
                {
                    right[i + 1] = right[i];
                    temp[i].Right = false;
                }
                if(i + rowC != right[i] && _rnd.NextDouble() > 0.5)
                {
                    bot[i] = right[i];
                    temp[i].Bottom = false;
                }
                else
                {
                    bot[i] = rowC;
                }
                rowC++;
            }
            for (int j = 0; j < right.Length; j++)
            {
                right[j] = bot[j];
            }

            return temp;
        }

        private void PrintWithOutNumbers(int start, int h, ECell[] maze)
        {
            for (int i = start; i < h; i++)
            {
                for (int j = 0; j < maze.Length; j++)
                {
                    if (maze[j].Bottom)
                    {
                        Console.Write("__");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    if (maze[j].Right)
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
        #endregion
    }
}
