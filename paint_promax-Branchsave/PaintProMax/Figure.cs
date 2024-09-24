using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintProMax
{
    [Serializable]
    class Figure
    {
        ///////////////////////
        private int x;
        private int y;
        ///////////////////////
        private int x2;
        private int y2;
        ///////////////////////
        private Color color;
        public Figure(int X , int Y, int X2, int Y2 , Color Color)
        {
            x = X;
            y = Y;
            x2 = X2;
            y2 = Y2;
            color = Color;
        }
        public Figure(int X, int Y, int X2, int Y2)
        {
            x = X;
            y = Y;
            x2 = X2;
            y2 = Y2;
        }
        public Figure(int X, int Y)
        {
            x = X;
            y = Y;
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int X2
        {
            get { return x2; }
            set { x2 = value; }
        }
        public int Y2
        {
            get { return y2; }
            set { y2 = value; }
        }


        public Color Color
        {
            get { return color; } 
            set { color = value; } 
        }


    }
}
