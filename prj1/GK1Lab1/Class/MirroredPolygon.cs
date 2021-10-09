using ComputerGraphicsLab1.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphicsLab1.Class
{
    [Serializable]
    public class MirroredPolygon : IFigure
    {
        internal int _r;
        internal float _wid;

        public Color FigureColor { get; set; }
        public MirrorType MirrorType { get; set; }
        public Point MirrorPoint { get; set; }
        public ICollection<IFigure> Figures { get; }
        public IFigure SelectedFigure { get; set; }
        public IFigureElement SelectedElement { get; set; }

        protected IFigureElement selected;

        public MirroredPolygon(int _r, float _wid, Color col, MirrorType typ)
        {
            _r = _r;
            _wid = _wid;
            FigureColor = col;
            Figures = new List<IFigure>();
            Figures.Add(new Polygon(_r, _wid, col));
            MirrorType = typ;
            MirrorPoint = new Point(-300, -300);
        }

        public List<IFigureElement> GetMirrorElements(IFigureElement el)
        {
            if (el is Vertex)
                return GetMirrorVertices((Vertex)el);
            else
                return GetMirrorEdges((Edge)el);
        }

        public List<IFigureElement> GetMirrorEdges(Edge e)
        {
            var leftmost = e.Leftmost;
            var rightmost = e.Rightmost;
            var ret = new List<IFigureElement>();

            switch (MirrorType)
            {
                case MirrorType.HORIZONTAL:
                    ret.Add(FindEdge(MirrorByMirrorPoint(leftmost.Location, true, false), MirrorByMirrorPoint(rightmost.Location, true, false)));
                    break;
                case MirrorType.VERTICAL:
                    ret.Add(FindEdge(MirrorByMirrorPoint(leftmost.Location, false, true), MirrorByMirrorPoint(rightmost.Location, false, true)));
                    break;
                case MirrorType.VERTICAL_HORIZONTAL:
                    ret.Add(FindEdge(MirrorByMirrorPoint(leftmost.Location, true, false), MirrorByMirrorPoint(rightmost.Location, true, false)));
                    ret.Add(FindEdge(MirrorByMirrorPoint(leftmost.Location, false, true), MirrorByMirrorPoint(rightmost.Location, false, true)));
                    break;
                case MirrorType.POINT:
                    ret.Add(FindEdge(MirrorByMirrorPoint(leftmost.Location, true, true), MirrorByMirrorPoint(rightmost.Location, true, true)));
                    break;
            }
            return ret;
        }


        public List<IFigureElement> GetMirrorVertices(Vertex v)
        {
            var loc = v.Location;
            var ret = new List<IFigureElement>();

            switch (MirrorType)
            {
                case MirrorType.HORIZONTAL:
                    ret.Add(FindVertex(MirrorByMirrorPoint(v.Location, true, false)));
                    break;
                case MirrorType.VERTICAL:
                    ret.Add(FindVertex(MirrorByMirrorPoint(v.Location, false, true)));
                    break;
                case MirrorType.VERTICAL_HORIZONTAL:
                    ret.Add(FindVertex(MirrorByMirrorPoint(v.Location, true, false)));
                    ret.Add(FindVertex(MirrorByMirrorPoint(v.Location, false, true)));
                    break;
                case MirrorType.POINT:
                    ret.Add(FindVertex(MirrorByMirrorPoint(v.Location, true, true)));
                    break;
            }
            return ret;
        }

        public Vertex FindVertex(Point p)
        {
            foreach (IFigure f in Figures)
            {
                var poly = (Polygon)f;
                var v = poly.vertices.Where(x => x.Location == p).FirstOrDefault();
                if (v != null) return v;
            }
            return null;
        }

        public Edge FindEdge(Point p1, Point p2)
        {
            foreach (IFigure f in Figures)
            {
                var poly = (Polygon)f;
                var e = poly.edges.Where(x => (x.from.Location == p1 && x.to.Location == p2) || (x.to.Location == p1 && x.from.Location == p2)).FirstOrDefault();
                if (e != null) return e;
            }
            return null;
        }


        public void Draw(Graphics g, Point? cursor = null, bool customLines = false, bool isBeingCreated = false, bool antialiasing = false)
        {
            foreach (IFigure f in Figures)
                f.Draw(g, cursor, customLines, isBeingCreated, antialiasing);
            var bounds = g.VisibleClipBounds;
            using (Pen dashed_pen = new Pen(Color.Purple, 2))
            {
                dashed_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                switch (MirrorType)
                {
                    case MirrorType.HORIZONTAL:
                        g.DrawLine(dashed_pen, new PointF(bounds.Left, MirrorPoint.Y), new PointF(bounds.Right, MirrorPoint.Y));
                        break;
                    case MirrorType.VERTICAL:
                        g.DrawLine(dashed_pen, new PointF(MirrorPoint.X, bounds.Top), new PointF(MirrorPoint.X, bounds.Bottom));
                        break;
                    case MirrorType.VERTICAL_HORIZONTAL:
                        g.DrawLine(dashed_pen, new PointF(bounds.Left, MirrorPoint.Y), new PointF(bounds.Right, MirrorPoint.Y));
                        g.DrawLine(dashed_pen, new PointF(MirrorPoint.X, bounds.Top), new PointF(MirrorPoint.X, bounds.Bottom));
                        break;
                    case MirrorType.POINT:
                        g.FillEllipse(new SolidBrush(Color.Purple), new Rectangle(new Point(MirrorPoint.X, MirrorPoint.Y), new Size(5, 5)));
                        break;
                }
            }
        }

        public void GenerateAllMirroredPolygons()
        {
            var source = (Polygon)Figures.First();
            switch (MirrorType)
            {
                case MirrorType.HORIZONTAL:
                    Figures.Add(GenerateMirroredPolygon(source, true, false));
                    break;
                case MirrorType.VERTICAL:
                    Figures.Add(GenerateMirroredPolygon(source, false, true));
                    break;
                case MirrorType.VERTICAL_HORIZONTAL:
                    Figures.Add(GenerateMirroredPolygon(source, false, true));
                    Figures.Add(GenerateMirroredPolygon(source, true, false));
                    break;
                case MirrorType.POINT:
                    Figures.Add(GenerateMirroredPolygon((Polygon)Figures.First(), true, true));
                    break;
            }
        }

        public Polygon GenerateMirroredPolygon(Polygon poly, bool horizontal, bool vertical)
        {
            var ret = ObjectCopier.Clone<Polygon>(poly);

            for(int i = 0; i < ret.vertices.Count; i++)
            {
                var sv = ret.vertices[i];
                var svLoc = sv.Location;
                var newPt = new Point(svLoc.X, svLoc.Y);

                if (horizontal)
                    newPt.Y = 2 * MirrorPoint.Y - svLoc.Y;

                if (vertical)
                    newPt.X = 2 * MirrorPoint.X - svLoc.X;

                sv.Location = newPt;
            }

            return ret;
        }

        public IFigureElement ClickedOn(Point click)
        {
            int i = Figures.Count - 1;
            for (; i >= 0 && Figures.ElementAt(i).ClickedOn(click) == null; i--) ;
            return i == -1 ? null : Figures.ElementAt(i).ClickedOn(click);
        }

        public IFigure ClickedOnFigure(Point click)
        {
            int i = Figures.Count - 1;
            for (; i >= 0 && Figures.ElementAt(i).ClickedOn(click) == null; i--) ;
            return i == -1 ? null : Figures.ElementAt(i);
        }

        public void SetSelected(IFigureElement v)
        {
            SelectedElement = null;
            foreach (IFigure f in Figures)
                f.SetSelected(null);
            var fig = FindFigure(v);
            if(fig != null)
            {
                fig.SetSelected(v);
                SelectedElement = v;
            } 
        }

        public IFigureElement GetSelected()
        {
            return SelectedElement;
        }


        public void DeleteElement(IFigureElement element)
        {
            foreach (IFigure f in Figures)
                f.DeleteElement(element);
        }

        public void Offset(Point p)
        {
            var newMir = new Point(MirrorPoint.X + p.X, MirrorPoint.Y + p.Y);

            MirrorPoint = newMir;
            foreach (IFigure f in Figures)
                f.Offset(p);
        }

        public IFigure FindFigure(Point p)
        {
            foreach (IFigure f in Figures)
                if (f.ClickedOn(p) != null)
                    return f;
            return null;
        }

        public IFigure FindFigure(IFigureElement v)
        {
            foreach (IFigure f in Figures)
            {
                var poly = (Polygon)f;
                if (poly.vertices.Where(x => x == v).FirstOrDefault() != null)
                    return poly;
                if (poly.edges.Where(x => x == v).FirstOrDefault() != null)
                    return poly;
            }
            return null;
        }

        public void SplitEdges(Edge e)
        {
            var mirrorElements = GetMirrorElements(e);
            mirrorElements.Add(e);

            foreach(Polygon f in Figures)
            {
                foreach(Edge ed in mirrorElements)
                {
                    if (f.edges.Contains(ed))
                    {
                        f.SplitEdge(ed, ed.GetLocation());
                        break;
                    }
                }
            }
        }

        public void DeleteVertices(Vertex v)
        {
            var mirrorElements = GetMirrorElements(v);
            mirrorElements.Add(v);

            foreach (Polygon f in Figures)
            {
                foreach (Vertex ve in mirrorElements)
                {
                    if (f.vertices.Contains(ve))
                    {
                        f.DeleteVertex(ve);
                        break;
                    }
                }
            }
        }

        public void OffsetElements(IFigureElement el, Point offset)
        {
            var mirrorElements = GetMirrorElements(el);
            Polygon sourcePoly = (Polygon)FindFigure(el);

            switch (MirrorType)
            {
                case MirrorType.HORIZONTAL:
                    var horOffset = MirrorOffset(offset, true, false);
                    mirrorElements.ElementAt(0).Offset(horOffset);
                    break;
                case MirrorType.VERTICAL:
                    var verOffset = MirrorOffset(offset, false, true);
                    mirrorElements.ElementAt(0).Offset(verOffset);
                    break;
                case MirrorType.POINT:
                    var ptOffset = MirrorOffset(offset, true, true);
                    mirrorElements.ElementAt(0).Offset(ptOffset);
                    break;
                case MirrorType.VERTICAL_HORIZONTAL:
                    var horrOffset = MirrorOffset(offset, true, false);
                    var verrOffset = MirrorOffset(offset, false, true);
                    mirrorElements.ElementAt(0).Offset(horrOffset);
                    mirrorElements.ElementAt(1).Offset(verrOffset);
                    break;
            }

            el.Offset(offset);
        }

        public Point MirrorOffset(Point offset, bool horizontal, bool vertical)
        {
            var ret = new Point(offset.X, offset.Y);

            if (horizontal)
                ret.Y = -ret.Y;

            if (vertical)
                ret.X = -ret.X;

            return ret;
        }

        public Point MirrorByMirrorPoint(Point pt, bool horizontal, bool vertical)
        {
            var ret = new Point(pt.X, pt.Y);

            if (horizontal)
                ret.Y = 2 * MirrorPoint.Y - pt.Y;

            if (vertical)
                ret.X = 2 * MirrorPoint.X - pt.X;

            return ret;
        }
    }
}
