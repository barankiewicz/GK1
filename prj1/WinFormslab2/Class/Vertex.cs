using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ComputerGraphicsLab1
{
    [Serializable]
    class Vertex : Interface.IPolygonElement
    {
        private Color color;
        private Point locc;
        private int pro;
        private float wid;

        public int Radius { get { return pro; } }
        public float Width { get { return wid; } }
        public Point Location { get { return locc; } set { locc = value; } }

        public Vertex(Point _loc, int _r, float _wid)
        {
            color = Color.Black;
            locc = _loc;
            pro = _r;
            wid = _wid;
        }

        public Vertex()
        {
            color = Color.Black;
            locc = new Point(0, 0);
            pro = 5;
            wid = 3.5f;
        }

        public void Draw(Graphics g, bool selected, bool customLines = false)
        {
            Point loc = Point.Empty;
            int rad = customLines ? 2 : Radius;
            g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(customLines ? new Point(Location.X + Radius / 2, Location.Y + Radius / 2) : Location, new Size(2 * rad, 2 * rad)));
            var temp = new Pen(Color.DarkOrange, wid + 1);

            if (selected)
                g.FillEllipse(new SolidBrush(Color.DarkOrange), new Rectangle(customLines ? new Point(Location.X + Radius/2, Location.Y + Radius / 2) : Location, new Size(2 * rad, 2 * rad)));
            else
                g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(customLines ? new Point(Location.X + Radius / 2, Location.Y + Radius / 2) : Location, new Size(2 * rad, 2 * rad)));
        }

        public Point GetLocation()
        {
            var newLoc = Location;
            newLoc.Offset(Radius, Radius);
            return newLoc;
        }

        public void SetLocation(Point loc)
        {
            loc.Offset(Radius, Radius);
            locc = loc;
        }

        public void OffsetLocation(Point loc)
        {
            locc.Offset(loc.X, loc.Y);
        }

        public void SetColor(Color _color)
        {
            color = _color;
        }

        public bool IsClicked(Point p)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(new Rectangle(Location, new Size(2 * Radius, 2 * Radius)));
                isOnLine = path.IsVisible(p);
            }
            return isOnLine;
        }

        public void Offset(Point p)
        {
            locc.Offset(p.X, p.Y);
        }
    }
}
