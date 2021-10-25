using ComputerGraphicsLab1.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Constraint
{
    [Serializable]
    public class EqualConstraint : IDoubleEdgeConstraint, IFigureElement
    {
        
        public Edge Edge1 { get; set; }

        public Edge Edge2 { get; set; }

        public int Id { get; set; }

        public EqualConstraint(Edge e1, Edge e2, int id_)
        {
            Edge1 = e1;
            Edge2 = e2;
            Id = id_;
        }

        public void ApplyConstraint(Point p, IFigureElement e, bool rightmost)
        {
            if (!ContainsElement(e)) return;
            var e2 = GetOtherElement(e);


            e2.SetLength(((Edge)e).GetLength());


            return;
        }

        public void Draw(Graphics g, bool selected, IFigure f)
        {
            if(f.HasElement(Edge1) && f.HasElement(Edge2))
            {
                DrawBoth(g, selected, f);
                return;
            }
            Edge e;

            if (f.HasElement(Edge1))
                e = Edge1;
            else
                e = Edge2;
            Point loc1 = e.GetLocation();
            //Point loc2 = e.GetLocation();

            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc1, new Size(16, 16)));
            //g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc2, new Size(16, 16)));

            loc1.Offset(3, 6);
            //loc2.Offset(3, 6);
            DrawEqualSign(g, loc1);
            //DrawEqualSign(g, loc2);

            loc1.Offset(9, 5);
            //loc2.Offset(9, 5);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font font = new Font("Arial", 9);
            g.DrawString(Id.ToString(), font, new SolidBrush(Color.White), loc1, sf);
            //g.DrawString(Id.ToString(), font, new SolidBrush(Color.White), loc2, sf);
        }

        public void DrawBoth(Graphics g, bool selected, IFigure f)
        {

            Point loc1 = Edge1.GetLocation();
            Point loc2 = Edge2.GetLocation();

            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc1, new Size(16, 16)));
            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc2, new Size(16, 16)));

            loc1.Offset(3, 6);
            loc2.Offset(3, 6);
            DrawEqualSign(g, loc1);
            DrawEqualSign(g, loc2);

            loc1.Offset(9, 5);
            loc2.Offset(9, 5);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font font = new Font("Arial", 9);
            g.DrawString(Id.ToString(), font, new SolidBrush(Color.White), loc1, sf);
            g.DrawString(Id.ToString(), font, new SolidBrush(Color.White), loc2, sf);
        }

        public Point GetLocation()
        {
            return new Point();
        }

        public bool IsClicked(Point p)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                Point loc1 = Edge1.GetLocation();
                Point loc2 = Edge2.GetLocation();

                path.AddRectangle(new Rectangle(loc1, new Size(16, 16)));
                path.AddRectangle(new Rectangle(loc2, new Size(16, 16)));
                isOnLine = path.IsVisible(p);
            }
            return isOnLine;
        }

        public void Offset(Point p)
        {
            return;
        }

        public void DrawEqualSign(Graphics g, Point p)
        {
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, new Size(8, 2)));
            p.Offset(0, -3);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, new Size(8, 2)));
        }

        public bool ContainsElement(IFigureElement e)
        {
            return e == Edge1 || e == Edge2;
        }

        public Edge GetOtherElement(IFigureElement e)
        {
            if (!ContainsElement(e)) return null;
            return (e == Edge1) ? Edge2 : Edge1;
        }
    }
}
