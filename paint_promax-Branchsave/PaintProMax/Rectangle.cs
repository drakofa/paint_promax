using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintProMax
{
    [Serializable]
    class Rectangle : Figure
    {
        public Rectangle(int X, int Y, int X2, int Y2, Color color)
            :base(X, Y, X2, Y2)
        {
            this.Color = color;
        }
        public void Draw(Graphics g)
        {
            Pen pen = new Pen(this.Color);
            int left = Math.Min(X, X2);
            int top = Math.Min(Y, Y2);
            int width = Math.Abs(X2 - X);
            int height = Math.Abs(Y2 - Y);
            g.DrawRectangle(pen, left, top, width, height);
        }
    }
}
