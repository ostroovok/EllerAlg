using System;
using EllerAlg;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MazeGenerator maze = new MazeGenerator(30);
            maze.Generate();
        }
    }
}
