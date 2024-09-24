using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Windows.Forms.LinkLabel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PaintProMax
{
    [Serializable]
    class Line:Figure 
    {
        public Line(int X, int Y, int X2, int Y2, Color color)
             : base(X, Y, X2, Y2)
        {
            this.Color = color; 
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(this.Color);
            g.DrawLine(pen, X, Y, X2, Y2);
            //BinaryFormatter formatter = new BinaryFormatter();
            //using (FileStream fs = new FileStream("Complex.bmp", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, g);

            //    //Console.WriteLine("Объект сериализован");
            //}
        }
    }
}
