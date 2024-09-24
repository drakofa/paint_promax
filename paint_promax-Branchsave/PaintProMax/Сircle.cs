using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintProMax
{
    [Serializable]
    class Сircle : Figure
    {
        private int radius;

        public Сircle(int X , int Y, int Radius, Color Color):base(X,Y)
        {
            radius = Radius;
            this.Color = Color; 
        }

        public int Radius
        {
            get {return radius; }
            set { radius = value; }
        }
        public void Draw(Graphics g)
        {
            Pen pen = new Pen(this.Color);
            g.DrawEllipse(pen, X - radius, Y - radius, radius * 2, radius * 2);
        }
    }
}
