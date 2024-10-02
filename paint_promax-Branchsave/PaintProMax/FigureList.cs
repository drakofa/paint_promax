using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace PaintProMax
{
    class FigureList : IEnumerable<Figure>
    {
        private List<Figure> figures = new List<Figure>();

        
        public void Add(Figure figure)
        {
            figures.Add(figure);
        }

       
        public void UndoLastFigure()
        {   
            if (figures.Count > 0)
            {
                figures.RemoveAt(figures.Count - 1);
            }
        }

        public void DrawAll(Graphics g)
        {
            foreach (var figure in figures)
            {
                
                figure.Draw(g);
            }
        }

        public IEnumerator<Figure> GetEnumerator()
        {
            return figures.GetEnumerator();
        }

        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
