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
        bool localDraw = false;
        Color Global_Color = Color.Black;
        bool isMouseDown = false;
        int i = 0;
        private Graphics g;
        private Point lastPosition;
        //private List<Line> lines = new List<Line>();
        //private List<Rectangle> rectangles = new List<Rectangle>();
        //private List<Сircle> сircles = new List<Сircle>();
        private FigureList NewFigureList = new FigureList();


        public delegate void FigureHandler(int X, int Y, int X2, int Y2, Color color);
        public event FigureHandler OnFigureSelect;


   

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

                //lines.Add(line);
                NewFigureList.Add(line);

                lastPosition = ty;


                this.Invalidate();
            }
        }
        public void DelegateMethod_CreateDrawLine(int X, int Y, int X2, int Y2, Color Global_Color)
        {
            Line line = new Line(X, Y, X2, Y2, Global_Color);
            //if (isMouseDown)
            //{
                

            //    if (lines.Count != 0)
            //    {
            //        lines.Clear();
            //        //lines.Add(line);
            //    }
            //    else
            //    {
            //        //lines.Add(line);
            //        NewFigureList.Add(line);
            //    }
            //}
            this.Invalidate();

            if (isMouseDown)
            {
                localDraw = true; // Устанавливаем в true при нажатии мыши
                using (Graphics g = this.CreateGraphics())
                {
                    line.Draw(g);  // Рисуем временный прямоугольник на форме

                }
            }
            else
            {
                if (localDraw)
                {
                    NewFigureList.Add(line);
                    //rectangles.Add(rect);
                    this.Invalidate();
                    localDraw = false;
                }
            }
            this.Invalidate();
        }
        public void DelegateMethod_CreateDrawRectangle(int X, int Y, int X2, int Y2, Color Global_Color)
        {

            Rectangle rect = new Rectangle(X, Y, X2, Y2, Global_Color);
            if (isMouseDown)
            {
                localDraw = true; // Устанавливаем в true при нажатии мыши
                using (Graphics g = this.CreateGraphics())
                {
                    rect.Draw(g);  // Рисуем временный прямоугольник на форме

                }
            }
            else
            {
                if (localDraw)
                {
                    NewFigureList.Add(rect);
                    //rectangles.Add(rect);
                    this.Invalidate();
                    localDraw = false;
                }
            }
            this.Invalidate();
        }
        public void DelegateMethod_CreateDrawCircle(int X, int Y, int X2, int Y2, Color Global_Color)
        {

            Сircle cir = new Сircle(X, Y,
                (int)Math.Sqrt(Math.Pow(X2 - X, 2) + Math.Pow(Y2 - Y2, 2)),
            Global_Color);

            

                if (isMouseDown)
                {
                    localDraw = true; 
                    using (Graphics g = this.CreateGraphics())
                    {
                        cir.Draw(g);  

                    }
                }
                else
                {
                    if (localDraw)
                    {
                        NewFigureList.Add(cir);
                        //сircles.Add(cir);
                        localDraw = false;
                        this.Invalidate();
                    }
                }
            
        }
        public void DelegateMethod_eraser(int X, int Y, int X2, int Y2, Color eraserColor)
        {
            eraserColor = Color.White;
            if (isMouseDown)
            {
                Point ty = new Point(X2, Y2);
                Line line = new Line(
                    X,
                    Y,
                    X2,
                    Y2,
                    eraserColor);

                //lines.Add(line);
                NewFigureList.Add(line);

                lastPosition = ty;


                this.Invalidate();
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
       
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (OnFigureSelect != null)
            {
                OnFigureSelect(lastPosition.X, lastPosition.Y, e.Location.X, e.Location.Y, Global_Color);
            }
       
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            //foreach (var line in lines)
            //{
            //    line.Draw(e.Graphics);
            //    i++;
            //}

            //foreach (var rectangle in rectangles)
            //{
            //    rectangle.Draw(e.Graphics);
            //    i++;
            //}
            //foreach (var сircle in сircles)
            //{
            //    сircle.Draw(e.Graphics);
            //    i++;
            //}
            foreach (var El in NewFigureList)
            {
                El.Draw(e.Graphics);
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
        private void eraser(object sender, EventArgs e)
        {
            OnFigureSelect = DelegateMethod_eraser;
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
                formatter.Serialize(fs, NewFigureList);


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
      

        private void backButt(object sender, EventArgs e)
        {
            NewFigureList.UndoLastFigure();
            this.Invalidate();
        }
    }
}
