using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllerAlg
{
    public class Cell
    {
        public Vector2Int Vector { get; }
        public bool Right { get; set; } = true;
        public bool Bottom { get; set; } = true;
        public bool Left { get; set; } = true;
        public bool Top { get; set; } = true;

        public Cell(Vector2Int vector) 
        {
            Vector = vector;
        }

        public Cell()
        {
        }
    }
}
