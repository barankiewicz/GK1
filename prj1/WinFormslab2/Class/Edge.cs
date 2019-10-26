using ComputerGraphicsLab1.Class;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ComputerGraphicsLab1
{
    [Serializable]
    class Edge : Interface.IPolygonElement
    {
        private Vertex v1, v2;

        public Vertex from { get { return v1; } }
        public Vertex to { get { return v2; } }

        public Vertex Rightmost { get { return (v2.Location.X >= v1.Location.X) ? v2 : v1; } }
        public Vertex Leftmost { get { return (v2.Location.X < v1.Location.X) ? v2 : v1; } }

        public Edge(ref Vertex _from, ref Vertex _to)
        {
            v1 = _from;
            v2 = _to;
        }

        public void Draw(Graphics g, bool selected, bool customLines = false)
        {
            if (customLines)
                LineDrawer.DrawCustomLine(from.GetLocation(), to.GetLocation(), selected ? Color.DarkOrange : Color.Black, g);
            else
                g.DrawLine(new Pen(selected ? Color.DarkOrange : Color.Black, 3.5f), from.GetLocation(), to.GetLocation());
        }

        public Point GetLocation()
        {
            return new Point((from.Location.X + to.Location.X) / 2, (from.Location.Y + to.Location.Y) / 2);
        }

        public Vertex GetOther(Vertex v)
        {
            if (v != from && v != to) return null;

            if (from == v)
                return to;
            else
                return from;
        }

        public bool IsRightmost(Vertex v)
        {
            if (v != from && v != to) return false;

            return v.Location.X >= GetOther(v).Location.X;
        }

        public bool IsClicked(Point p)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                using (var pen = new Pen(Color.Black, 3.5f))
                {
                    path.AddLine(from.GetLocation(), to.GetLocation());
                    isOnLine = path.IsOutlineVisible(p, pen);
                }
            }
            return isOnLine;
        }

        public double GetSlope()
        {
            var delY = to.Location.Y - from.Location.Y;
            var delX = to.Location.X - from.Location.X;
            double slope = (double)delY / (double)delX;
            return slope;
        }

        public bool IsNeighbour(Edge e)
        {
            return this.from == e.to || this.to == e.from || this.from == e.from || this.to == e.to;
        }

        public void Offset(Point p)
        {
            from.OffsetLocation(p);
            to.OffsetLocation(p);
        }

        public double GetLength()
        {
            var norLen = Math.Sqrt(Math.Pow((to.Location.Y - from.Location.Y), 2) + Math.Pow((to.Location.X - from.Location.X), 2));
            return norLen == 0 ? 1 : norLen;
        }

        public void SetLength(double len)
        {
            double currentLength = GetLength();

            var k =  len/currentLength;

            var x3 = from.Location.X + k * (to.Location.X - from.Location.X);
            var y3 = from.Location.Y + k * (to.Location.Y - from.Location.Y);

            to.Location = new Point((int)x3, (int)y3);
        }

        internal void SetSlope(double a, bool rightmost)
        {
            var pointToMove = rightmost ? Rightmost : Leftmost;
            var other = GetOther(pointToMove);
            pointToMove.Location = new Point(pointToMove.Location.X, (int)(pointToMove.Location.X * a + other.Location.Y));

        }
    }
}
