using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllerAlg
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool Right { get; set; } = false;
        public bool Bottom { get; set; } = false;
        public int LotsOf { get; set; }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Cell(Vector2Int vector) 
        {
            X = vector.X;
            Y = vector.Y;
        }
        
        public Cell(int lotsOf) => LotsOf = lotsOf;
    }
}
