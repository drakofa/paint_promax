using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintProMax
{
    class CurveLine:Figure
    {
        private List<Point> points;

        public CurveLine(Color color) : base(0, 0)
        {
            this.Color = color;
            points = new List<Point>();
        }
        public void AddPoint(int x, int y)
        {
            points.Add(new Point(x, y));
        }
        public void Draw(Graphics g)
        {
            if (points.Count > 1)
            {
                Pen pen = new Pen(this.Color);
                g.DrawCurve(pen, points.ToArray()); 
            }
        }
    }
}
