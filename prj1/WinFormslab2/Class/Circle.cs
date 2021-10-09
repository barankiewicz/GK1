using ComputerGraphicsLab1.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphicsLab1.Class
{
    [Serializable]
    class Circle : IFigure, IFigureElement
    {
        public double Radius { get; set; }
        public Vertex Center { get; set; }
        public Color FigureColor { get; set; }
        public bool CircleClicked { get; set; }
        public bool CenterClicked { get; set; }

        public Circle(double r, Vertex c, Color col)
        {
            Radius = r;
            Center = c;
            FigureColor = col;
            CircleClicked = false;
            CenterClicked = false;
        }

        public IFigureElement ClickedOn(Point click)
        {
            if (Center.IsClicked(click))
            {
                CircleClicked = false;
                CenterClicked = true;
                return Center;
            }

            if(IsClicked(click))
            {
                CircleClicked = true;
                CenterClicked = false;
                return this;
            }

            CircleClicked = false;
            CenterClicked = false;
            return null;
        }

        public void Draw(Graphics g, Point? cursor = null, bool customLines = false, bool isBeingCreated = false, bool antialiasing = false)
        {
            Center.Draw(g, CenterClicked, customLines);
            if (cursor != null)
                Radius = GetRadius((Point)cursor);

            if (customLines)
                LineDrawer.DrawCustomEllipse(g, Center.GetLocation(), (int)Radius, cursor != null ? Color.Red : CircleClicked ? Color.DarkOrange : FigureColor, antialiasing);
            else
                g.DrawEllipse(
                    new Pen(cursor != null ? Color.Red : CircleClicked ? Color.DarkOrange : FigureColor, 3.5f),
                    (float)(Center.GetLocation().X - Radius),
                    (float)(Center.GetLocation().Y - Radius),
                    (float)(2 * Radius),
                    (float)(2 * Radius)
                    );
        }

        public void Offset(Point p)
        {
            Center.Offset(p);
        }

        public void ChangeSize(Point p)
        {
            this.Radius = GetRadius(p);
        }

        public double GetRadius(Point p)
        {
            return Math.Sqrt(Math.Pow(p.X - Center.Location.X, 2) + Math.Pow(p.Y - Center.Location.Y, 2));
        }

        public Point GetLocation()
        {
            return Center.Location;
        }

        public bool IsClicked(Point p)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                using (var pen = new Pen(Color.Black, 5f))
                {
                    path.AddEllipse(
                        (float)(Center.GetLocation().X - Radius),
                        (float)(Center.GetLocation().Y - Radius),
                        (float)(2 * Radius),
                        (float)(2 * Radius)
                        );
                    isOnLine = path.IsOutlineVisible(p, pen);
                }
            }
            return isOnLine;
        }

        void IFigure.DeleteElement(IFigureElement element)
        {
            return;
        }

        public void SetSelected(IFigureElement v)
        {
            return;
        }

        public IFigureElement GetSelected()
        {
            return new Vertex();
        }
    }
}
