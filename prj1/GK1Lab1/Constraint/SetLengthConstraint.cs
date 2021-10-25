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
    public class SetLengthConstraint : IEdgeConstraint, IFigureElement
    {
        public Edge Edge { get; }
        public int Id { get; set; }

        public double ConstraintLength { get; }

        public SetLengthConstraint(Edge e1, double len, int id_)
        {
            Edge = e1;
            ConstraintLength = len;
            Id = id_;
        }

        public void ApplyConstraint(Point p, IFigureElement e, bool rightmost)
        {
            if (!ContainsElement(e)) return;

            Edge.SetLength(ConstraintLength);

            return;
        }

        public bool ContainsElement(IFigureElement e)
        {
            return e == Edge;
        }

        public void Draw(Graphics g, bool selected, IFigure f)
        {
            Point loc1 = Edge.GetLocation();

            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc1, new Size(16, 16)));

            loc1.Offset(3, 2);
            DrawLSign(g, loc1);
            loc1.Offset(0, 4);


            loc1.Offset(9, 5);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font font = new Font("Arial", 9);
            g.DrawString(Id.ToString(), font, new SolidBrush(Color.White), loc1, sf);
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
                Point loc1 = Edge.GetLocation();

                path.AddRectangle(new Rectangle(loc1, new Size(16, 16)));
                isOnLine = path.IsVisible(p);
            }
            return isOnLine;
        }

        public void Offset(Point p)
        {
            return;
        }

        public void DrawLSign(Graphics g, Point p)
        {
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, new Size(2, 8)));
            p.Offset(0, 8);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, new Size(5, 2)));
            p.Offset(-3, 0);
        }
    }
}
