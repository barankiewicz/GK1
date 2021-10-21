using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Class
{
    [Serializable]
    class EdgeConstraint : Interface.IFigureElement
    {
        private Edge edge1;
        private Edge edge2;
        private ConstraintType edgeConstraintType;
        private int id;

        public EdgeConstraint(ConstraintType type, Edge e1, Edge e2, int id_)
        {
            edgeConstraintType = type;
            edge1 = e1;
            edge2 = e2;
            id = id_;
        }

        public void Draw(Graphics g, bool selected, bool customLines = false, bool isBeingCreated = false)
        {
            Point loc1 = edge1.GetLocation();
            Point loc2 = edge2.GetLocation();
            //var a1 = -1/edge1.GetSlope();
            //var a2 = -1/edge2.GetSlope();

            //loc1.Offset(16, (int)(16 * a1));
            //loc2.Offset(16, (int)(16 * a2));
            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc1, new Size(16, 16)));
            g.FillRectangle(new SolidBrush(selected ? Color.Blue : Color.DarkGreen), new Rectangle(loc2, new Size(16, 16)));


               
            if(edgeConstraintType == ConstraintType.CONSTRAINT_EQUAL) 
            {
                loc1.Offset(3, 6);
                loc2.Offset(3, 6);
                DrawEqualSign(g, loc1);
                DrawEqualSign(g, loc2);
            } else
            {
                loc1.Offset(3, 2);
                loc2.Offset(3, 2);
                DrawParallelSign(g, loc1);
                DrawParallelSign(g, loc2);
                loc1.Offset(0, 4);
                loc2.Offset(0, 4);
            }


            loc1.Offset(9, 5);
            loc2.Offset(9, 5);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font font = new Font("Arial", 9);
            g.DrawString(id.ToString(), font, new SolidBrush(Color.White), loc1, sf);
            g.DrawString(id.ToString(), font, new SolidBrush(Color.White), loc2, sf);
        }

        public Point GetLocation()
        {
            return new Point();
        }

        public Edge GetOtherEdge(Edge e)
        {
            if (!ContainsEdge(e)) return null;
            return (e == edge1) ? edge2 : edge1;
        }

        public bool IsClicked(Point p)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                Point loc1 = edge1.GetLocation();
                Point loc2 = edge2.GetLocation();

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

        public void Offset(Point p, Edge e, bool rightmost)
        {
            if (!ContainsEdge(e)) return;
            var e2 = GetOtherEdge(e);

            if(edgeConstraintType == ConstraintType.CONSTRAINT_EQUAL)
            {
                e2.SetLength(e.GetLength());
            } else if(edgeConstraintType == ConstraintType.CONSTRAINT_PARALLEL)
            {
                double slopeToSet = e.GetSlope();
                e2.SetSlope(slopeToSet, rightmost);
            }
            return;
        }

        public bool ContainsEdge(Edge e)
        {
            return e == edge1 || e == edge2;
        }

        public void DrawEqualSign(Graphics g, Point p)
        {
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, new Size(8, 2)));
            p.Offset(0, -3);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(p, new Size(8, 2)));
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
