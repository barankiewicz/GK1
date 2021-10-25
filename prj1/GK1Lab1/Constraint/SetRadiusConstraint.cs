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
    public class SetRadiusConstraint : ICircleConstraint, IFigureElement
    {
        public int Id { get; }
        public Circle Circle { get; }
        public double ConstraintRadius { get; }

        public SetRadiusConstraint(Circle c, double constRadius, int id)
        {
            Id = id;
            Circle = c;
            ConstraintRadius = constRadius;
        }

        public void Draw(Graphics g, bool selected, IFigure f)
        {
            Point loc = Circle.GetLocation();

            loc.Offset(Id % 2 == 0 ? 15 : -15, 10);

            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc, new Size(16, 16)));

            loc.Offset(2, 2);
            DrawCircleSign(g, loc);


            loc.Offset(11, 8);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font font = new Font("Arial", 9);
            g.DrawString(Id.ToString(), font, new SolidBrush(Color.White), loc, sf);
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
                Point loc = Circle.GetLocation();

                loc.Offset(Id % 2 == 0 ? 15 : -15, 10);

                path.AddRectangle(new Rectangle(loc, new Size(16, 16)));
                isOnLine = path.IsVisible(p);
            }
            return isOnLine;
        }

        public void Offset(Point p)
        {
            return;
        }

        public void DrawCircleSign(Graphics g, Point p)
        {
            g.DrawEllipse(new Pen(new SolidBrush(Color.White)), new Rectangle(p, new Size(8, 8)));
            p.Offset(3, 0);
            p.Offset(-3, -3);
        }

        public void ApplyConstraint(Point p, IFigureElement e, bool rightmost)
        {
            Circle.Radius = ConstraintRadius;
        }

        public bool ContainsElement(IFigureElement e)
        {
            return e == Circle;
        }
    }
}
