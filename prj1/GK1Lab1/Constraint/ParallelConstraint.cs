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
    public class ParallelConstraint : IDoubleEdgeConstraint, IFigureElement
    {

        public Edge Edge1 { get; set; }

        public Edge Edge2 { get; set; }

        public int Id { get; set; }

        public void ApplyConstraint()
        {
            throw new NotImplementedException();
        }

        public void Draw(Graphics g, bool selected, Point? cursor = null)
        {
            Point loc1 = Edge1.GetLocation();
            Point loc2 = Edge2.GetLocation();

            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc1, new Size(16, 16)));
            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc2, new Size(16, 16)));

                loc1.Offset(3, 2);
                loc2.Offset(3, 2);
                DrawParallelSign(g, loc1);
                DrawParallelSign(g, loc2);
                loc1.Offset(0, 4);
                loc2.Offset(0, 4);


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

        public bool ContainsEdge(Edge e)
        {
            return e == Edge1 || e == Edge2;
        }

        public Edge GetOtherEdge(Edge e)
        {
            if (!ContainsEdge(e)) return null;
            return (e == Edge1) ? Edge2 : Edge1;
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

        public void DrawParallelSign(Graphics g, Point p)
        {
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, new Size(2, 8)));
            p.Offset(3, 0);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, new Size(2, 8)));
            p.Offset(-3, -3);
        }
    }
}
