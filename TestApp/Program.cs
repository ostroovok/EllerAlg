using System;
using EllerAlg;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MazeCreator maze = new MazeCreator(8, 4);
            //Cell[][] m = maze.Generate();
            while(true)
                maze.Generate();
        }
    }
}
