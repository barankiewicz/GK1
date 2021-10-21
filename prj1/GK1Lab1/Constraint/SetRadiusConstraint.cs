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
    public class SetRadiusConstraint : ICircleConstraint, IFigureElement
    {
        public int Id { get; }
        public Circle Circle { get; }
        public double ConstraintRadius { get; }

        public SetRadiusConstraint(int id, Circle c, double constRadius)
        {
            Id = id;
            Circle = c;
            ConstraintRadius = constRadius;
        }

        public void ApplyConstraint()
        {
            Circle.Radius = ConstraintRadius;
        }

        public void Draw(Graphics g, bool selected, Point? cursor = null)
        {
            Point loc = Circle.GetLocation();

            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc, new Size(16, 16)));

            loc.Offset(3, 2);
            loc.Offset(3, 2);
            DrawCircleSign(g, loc);
            loc.Offset(0, 4);
            loc.Offset(0, 4);


            loc.Offset(9, 5);
            loc.Offset(9, 5);

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
            g.FillEllipse(new SolidBrush(Color.White), new Rectangle(p, new Size(8, 8)));
            p.Offset(3, 0);
            p.Offset(-3, -3);
        }
    }
}
