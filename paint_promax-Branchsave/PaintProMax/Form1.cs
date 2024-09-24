using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing.Imaging;


namespace PaintProMax
{
    public partial class Form1 : Form
    {
        Color Global_Color = Color.Black;
        bool isMouseDown = false;
        int i = 0;
        private Graphics g;
        private Point lastPosition;
        private List<Line> lines = new List<Line>();
        private List<Rectangle> rectangles = new List<Rectangle>();
        private List<Сircle> сircles = new List<Сircle>();


        public delegate void FigureHandler(int X, int Y, int X2, int Y2, Color color);
        public event FigureHandler OnFigureSelect;


        //private Figure CreateLine(Point start, Point end, Color color)
        //{
        //    return new Line(start.X, start.Y, end.X, end.Y, color);
        //}

        //// Метод для создания прямоугольника
        //private Figure CreateRectangle(Point start, Point end, Color color)
        //{
        //    return new Rectangle(start.X, start.Y, end.X, end.Y, color);
        //}

        //// Метод для создания круга
        //private Figure CreateCircle(Point start, Point end, Color color)
        //{
        //    int radius = (int)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        //    return new Сircle(start.X, start.Y, radius, color);
        //}


        public void DelegateMethod_CreateDrawCurveLine(int X, int Y, int X2, int Y2, Color Global_Color)
        {
            if (isMouseDown)
            {
                Point ty = new Point(X2, Y2);
                Line line = new Line(
                    X,
                    Y,
                    X2,
                    Y2,
                    Global_Color);

                lines.Add(line);

                lastPosition = ty;


                this.Invalidate();
            }
        }
        public void DelegateMethod_CreateDrawLine(int X, int Y, int X2, int Y2, Color Global_Color)
        {
            if (isMouseDown)
            {
                Line line = new Line(X, Y, X2, Y2, Global_Color);

                if (lines.Count != 0)
                {
                    lines.Clear();
                    lines.Add(line);
                }
                else
                {
                    lines.Add(line);
                }
            }
            this.Invalidate();
        }
        public void DelegateMethod_CreateDrawRectangle(int X, int Y, int X2, int Y2, Color Global_Color)
        {
            if (isMouseDown)
            {
                Rectangle rect = new Rectangle(X, Y, X2, Y2, Global_Color);

                if (rectangles.Count != 0)
                {
                    rectangles.Clear();
                    rectangles.Add(rect);
                }
                else
                {
                    rectangles.Add(rect);
                }
            }
            this.Invalidate();
        }
        public void DelegateMethod_CreateDrawCircle(int X, int Y, int X2, int Y2, Color Global_Color)
        {
            if (isMouseDown)
            {
                Сircle cir = new Сircle(
                    X,
                    Y,
                    (int)Math.Sqrt(Math.Pow(X2 - X, 2) + Math.Pow(Y2 - Y2, 2)),
                Global_Color);

                if (сircles.Count != 0)
                {
                    сircles.Clear();
                    сircles.Add(cir);
                }
                else
                {
                    сircles.Add(cir);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(OnMouseDown);
            this.MouseMove += new MouseEventHandler(OnMouseMove);
            this.MouseUp += new MouseEventHandler(OnMouseUp);

        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                lastPosition = e.Location;
                label1.Text = e.Location.ToString();
            }
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;

                this.Invalidate();
            }
        }
        //private void OnMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (isMouseDown)
        //    {
        //        // Создаем новую линию и добавляем её в список
        //        Line line = new Line(
        //            lastPosition.X,
        //            lastPosition.Y,
        //            e.Location.X,
        //            e.Location.Y,
        //            Color.Black);
        //        lines.Add(line);

        //        lastPosition = e.Location;

        //        // Обновляем только необходимую область для перерисовки
        //        this.Invalidate();
        //    }
        //}
        //private void OnMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (isMouseDown)
        //    {
        //        Line line = new Line(
        //            lastPosition.X,
        //            lastPosition.Y,
        //            e.Location.X,
        //            e.Location.Y,
        //            Color.Black); 

        //        if (lines.Count != 0)
        //        {
        //            lines.Clear();
        //            lines.Add(line);
        //        }
        //        if (lines.Count == 0)
        //        {

        //            lines.Add(line);
        //        }
        //    }
        //    this.Invalidate();

        //}

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && OnFigureSelect != null)
            {
                OnFigureSelect(lastPosition.X, lastPosition.Y, e.Location.X, e.Location.Y, Global_Color);
            }
            //switch (statusDraw)
            //{
            //    case "l":
            //        DelegateMethod_CreateDrawLine(lastPosition, e.Location, Color.Black);
            //        break;
            //    case "cu":
            //        DelegateMethod_CreateDrawCurveLine(lastPosition, e.Location, Color.Black);
            //        break;
            //    case "r":
            //        DelegateMethod_CreateDrawRectangle(lastPosition, e.Location, Color.Black);
            //        break;
            //    case "ci":
            //        DelegateMethod_CreateDrawCircle(lastPosition, e.Location, Color.Black);
            //        break;

            //}
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var line in lines)
            {
                line.Draw(e.Graphics);
                i++;
            }

            foreach (var rectangle in rectangles)
            {
                rectangle.Draw(e.Graphics);
                i++;
            }
            foreach (var сircle in сircles)
            {
                сircle.Draw(e.Graphics);
                i++;
            }
        }


        private void DrawL_Click(object sender, EventArgs e)
        {
            // Линия
            OnFigureSelect = DelegateMethod_CreateDrawLine;
        }

        private void DrawCU_Click(object sender, EventArgs e)
        {
            // произвольная кривая линия
            OnFigureSelect = DelegateMethod_CreateDrawCurveLine;
        }

        private void DrawR_Click(object sender, EventArgs e)
        {
            // прямоугольник
            OnFigureSelect = DelegateMethod_CreateDrawRectangle;
        }

        private void DrawCI_Click(object sender, EventArgs e)
        {
            // круг
            OnFigureSelect = DelegateMethod_CreateDrawCircle;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void ColorSwitch(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Global_Color = colorDialog1.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // создаем объект BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("Complex1.bmp", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, lines);
                formatter.Serialize(fs, rectangles);
                formatter.Serialize(fs, сircles);

            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmaps|*.bmp|jpeps|*.jpg";
            PictureBox PictureBox1 = new PictureBox();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PictureBox1.Image = Bitmap.FromFile(openFileDialog.FileName);
                g = Graphics.FromImage(PictureBox1.Image);
                PictureBox1.Refresh();
            }

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }
    }
}
