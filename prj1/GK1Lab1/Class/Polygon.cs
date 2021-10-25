using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing.Drawing2D;
using ComputerGraphicsLab1.Class;
using ComputerGraphicsLab1.Interface;
using ComputerGraphicsLab1.Constraint;

namespace ComputerGraphicsLab1
{

    [Serializable]
    public class Polygon : IFigure
    {
        public List<Vertex> vertices;
        public List<Edge> edges;
        protected int r;
        protected float wid;
        protected Interface.IFigureElement selected;
        protected bool closed;

        public bool Closed { get => closed; set => closed = value; }
        public int VertexCount { get => vertices.Count; }
        public int EdgesCount { get => edges.Count; }
        
        public Vertex FirstVertex { get => vertices.FirstOrDefault(); }
        public Vertex LastVertex { get => vertices.LastOrDefault(); }
        public Vertex BeforeLastVertex { get => vertices.ElementAtOrDefault(vertices.Count - 2); }
        public Color FigureColor { get; set; }
        public ICollection<IConstraint> Constraints { get; set; }

        public Polygon(int _r, float _wid, Color col)
        {
            vertices = new List<Vertex>();
            edges = new List<Edge>();
            Constraints = new HashSet<IConstraint>();
            r = _r;
            wid = _wid;
            selected = null;
            FigureColor = col;
        }

        public IFigureElement ClickedOn(Point click)
        {
            foreach (IConstraint c in Constraints)
            {
                if (c.IsClicked(click))
                    return c;
            }

            foreach (Vertex v in vertices)
            {
                if (v.IsClicked(click))
                    return v;
            }

            foreach (Edge e in edges)
            {
                if (e.IsClicked(click))
                    return e;
            }

            return null;
        }

        public void SetSelected(Interface.IFigureElement v)
        {
            selected = v;
        }

        public Interface.IFigureElement GetSelected()
        {
            return selected;
        }

        public Vertex AddVertex(Point loc)
        {
            if (ClickedOn(loc) != null)
                return null;

            Point circleLocation = new Point(loc.X - r, loc.Y - r);
            var toAdd = new Vertex(circleLocation, r, wid);
            vertices.Add(new Vertex(circleLocation, r, wid));
            return toAdd;
        }

        public void DeleteVertex(Vertex v)
        {
            if (VertexCount <= 3 || !vertices.Contains(v)) return;
            List<Edge> temp = new List<Edge>();
            Vertex before = null;
            Vertex after = null;
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].from == v)
                {
                    after = edges[i].to;
                    temp.Add(edges[i]);
                }
                else if (edges[i].to == v)
                {
                    before = edges[i].from;
                    temp.Add(edges[i]);
                } 
            }

            foreach (Edge e in temp)
                DeleteEdge(e);

            vertices.Remove(v);

            if(VertexCount >= 2)
                AddEdge(before, after);
        }

        public void DeleteEdge(Edge e)
        {
            var c = FindConstraint(e);
            if (c != null)
                DeleteConstraint(c);
            edges.Remove(e);
        }

        public void DeleteElement(Interface.IFigureElement element)
        {
            if (element is Edge)
                DeleteEdge((Edge)element);
            else if (element is Vertex)
                DeleteVertex((Vertex)element);
            else
                DeleteConstraint((IConstraint)element);
        }

        private void DeleteConstraint(IConstraint element)
        {
            if(Constraints.Contains(element)) 
                Constraints.Remove(element);
        }

        public void Draw(Graphics g, Point? cursor=null, bool customLines = false, bool isBeingCreated = false, bool antialiasing = false)
        {
            if (vertices.Count == 0) return;

            if (cursor != null && !customLines)
                g.DrawLine(new Pen(Color.Red, 3.5f), LastVertex.GetLocation(), (Point)cursor);
            else if (cursor != null && customLines)
                LineDrawer.DrawCustomLine(LastVertex.GetLocation(), (Point)cursor, Color.Red, g, antialiasing);

            for (int i = 0; i < edges.Count; i++)
            {
                if (selected != null && edges[i] == selected) //If selected, we want to draw it differently
                {
                    edges[i].Draw(g, true, customLines, false, antialiasing);
                    continue;
                }
                edges[i].Draw(g, false, customLines, false, antialiasing);
            }

            for (int i = 0; i < vertices.Count; i++)
            {
                if (selected != null && vertices[i] == selected) //If selected, we want to draw it differently
                {
                    vertices[i].Draw(g, true, customLines);
                    continue;
                }
                vertices[i].Draw(g, false, customLines);
            }

            for (int i = 0; i < Constraints.Count; i++)
            {
                if (selected != null && Constraints.ElementAt(i) == selected) //If selected, we want to draw it differently
                {
                    Constraints.ElementAt(i).Draw(g, true, this);
                    continue;
                }
                Constraints.ElementAt(i).Draw(g, false, this);
            }
        }

        public bool ExistEdge(Vertex v1, Vertex v2)
        {
            foreach (Edge e in edges)
                if ((e.from == v1 && e.to == v2) || (e.from == v2 && e.to == v1))
                    return true;
            return false;
        }

        public bool AddEdge(Vertex v1, Vertex v2)
        {
            if (v1 == null || v2 == null)
                return false;

            //for(int i = 0; i < edges.Count; i++)
            //{
            //    if ((edges[i].from == v1 && edges[i].to == v2) || (edges[i].from == v2 && edges[i].to == v1))
            //    {
            //        edges.Remove(edges[i]);
            //        return false;
            //    }    
            //}
            edges.Add(new Edge(ref v1, ref v2, FigureColor));
            return true;
        }

        public void Offset(Point p)
        {
            foreach(Vertex v in vertices)
            {
                v.Offset(p);
            }
        }

        public Edge GetNeighbourFrom (Vertex v)
        {
            foreach (Edge e in edges)
                if (e.from == v)
                    return e;
            return null;
        }

        public Edge GetNeighbourTo(Vertex v)
        {
            foreach (Edge e in edges)
                if (e.to == v)
                    return e;
            return null;
        }

        public void OffsetElement(IFigureElement e, Point p, List<Edge> touchedEdges, List<IConstraint> touchedConstraits)
        {
            if (touchedEdges.Count == edges.Count)
                return;
            Edge initiallyMoved1;
            Edge initiallyMoved2;
            bool rightmost1;
            bool rightmost2;

            if (e is Vertex)
            {
                Vertex v = (Vertex)e;
                initiallyMoved1 = GetNeighbourFrom(v);
                initiallyMoved2 = GetNeighbourTo(v);
                rightmost1 = initiallyMoved1.IsRightmost(v);
                rightmost2 = initiallyMoved2.IsRightmost(v);
            }
            else if (e is Edge)
            {
                var ed = (Edge)e;
                initiallyMoved1 = GetNeighbourTo(ed.from);
                initiallyMoved2 = GetNeighbourFrom(ed.to);
                rightmost1 = initiallyMoved1.IsRightmost(ed.from);
                rightmost2 = initiallyMoved2.IsRightmost(ed.to);
            }
            else
                return;

            if (touchedEdges.Count == 0)
                e.Offset(p);

            if (!touchedEdges.Contains(initiallyMoved1)) touchedEdges.Add(initiallyMoved1);
            if (!touchedEdges.Contains(initiallyMoved2)) touchedEdges.Add(initiallyMoved2);

            IConstraint constraint1 = FindConstraint(initiallyMoved1);
            IConstraint constraint2 = FindConstraint(initiallyMoved2);

            if (constraint1 != null && !touchedConstraits.Contains(constraint1))
            {
                touchedConstraits.Add(constraint1);
                constraint1.ApplyConstraint(p, initiallyMoved1, rightmost1);
                OffsetElement(initiallyMoved1, p, touchedEdges, touchedConstraits);
            }

            if (constraint2 != null && !touchedConstraits.Contains(constraint2))
            {
                touchedConstraits.Add(constraint2);
                constraint2.ApplyConstraint(p, initiallyMoved2, rightmost2);
                OffsetElement(initiallyMoved2, p, touchedEdges, touchedConstraits);
            }
        }

        public IConstraint FindConstraint(IFigureElement e)
        {
            foreach (IConstraint c in Constraints)
            {
                if (c.ContainsElement(e))
                    return c;
            }
            return null;
        }


        public Point SplitEdge(Edge cl, Point newVertexLocation)
        {

            Vertex from = cl.from;
            Vertex to = cl.to;

            DeleteEdge(cl);

            var toAdd = new Vertex(newVertexLocation, r, wid);
            vertices.Add(toAdd);
            //AddVertex(newVertexLocation);
            AddEdge(from, LastVertex);
            AddEdge(LastVertex, to);
            return newVertexLocation;
        }

        public bool HasElement(IFigureElement element)
        {
            return edges.Contains(element) || vertices.Contains(element);
        }


        public static void WriteToFile(Polygon graph, string path)
        {
            graph.SetSelected(null);
            var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, graph);
            fs.Close();
        }

        public static Polygon ReadFromFile(string path)
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            Polygon obj = (Polygon)bf.Deserialize(fs);
            fs.Close();
            return obj;
        }
    }
}
