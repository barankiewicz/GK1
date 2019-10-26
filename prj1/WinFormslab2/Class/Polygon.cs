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

namespace ComputerGraphicsLab1
{

    [Serializable]
    class Polygon
    {
        private List<Vertex> vertices;
        private List<Edge> edges;
        private List<EdgeConstraint> constraints;
        private int r;
        private float wid;
        private Interface.IPolygonElement selected;
        private bool closed;
        private bool customLines;

        public bool CustomLines { get => customLines; set => customLines = value; }
        public bool Closed { get => closed; set => closed = value; }
        public int VertexCount { get => vertices.Count; }
        public int EdgesCount { get => edges.Count; }
        
        public Vertex FirstVertex { get => vertices.FirstOrDefault(); }
        public Vertex LastVertex { get => vertices.LastOrDefault(); }
        public Vertex BeforeLastVertex { get => vertices.ElementAtOrDefault(vertices.Count - 2); }

        public Polygon(int _r, float _wid, bool customLines_ = false)
        {
            vertices = new List<Vertex>();
            edges = new List<Edge>();
            constraints = new List<EdgeConstraint>();
            r = _r;
            wid = _wid;
            selected = null;
            customLines = customLines_;
        }

        public Interface.IPolygonElement ClickedOn(Point click)
        {
            foreach (EdgeConstraint c in constraints)
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

        public void SetSelected(Interface.IPolygonElement v)
        {
            selected = v;
        }

        public Interface.IPolygonElement GetSelected()
        {
            return selected;
        }

        public void AddConstraint(EdgeConstraintType type, Edge e1, Edge e2)
        {
            if (type == EdgeConstraintType.CONSTRAINT_PARALLEL && e1.IsNeighbour(e2))
            {
                MessageBox.Show("Can't add the Parallel Constraint between neighbouring edges!");
                return;
            }

            var c1 = FindConstraint(e1);
            var c2 = FindConstraint(e2);

            if(c1 != null || c2 != null)
            {
                MessageBox.Show("There's already a relation on one of the edges!");
                return;
            }
            constraints.Add(new EdgeConstraint(type, e1, e2, constraints.Count + 1));
            constraints.Last().Offset(new Point(0, 0), e1, false);
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
            if(c != null)
                constraints.Remove(FindConstraint(e));
            edges.Remove(e);
        }

        public EdgeConstraint FindConstraint(Edge e)
        {
            foreach(EdgeConstraint c in constraints)
            {
                if (c.ContainsEdge(e))
                    return c;
            }
            return null;
        }

        public void DeleteElement(Interface.IPolygonElement element)
        {
            if (element is Edge)
                DeleteEdge((Edge)element);
            else if (element is Vertex)
                DeleteVertex((Vertex)element);
            else
                DeleteteConstraint((EdgeConstraint)element);
        }

        private void DeleteteConstraint(EdgeConstraint element)
        {
            constraints.Remove(element);
        }

        public void DrawPolygon(PictureBox p, Point? cursor=null)
        {
            Bitmap bmp = new Bitmap(p.Width, p.Width);
            Graphics g = Graphics.FromImage(bmp);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (cursor != null && !customLines)
                g.DrawLine(new Pen(Color.Red, 3.5f), LastVertex.GetLocation(), (Point)cursor);
            else if (cursor != null && customLines)
                LineDrawer.DrawCustomLine(LastVertex.GetLocation(), (Point)cursor, Color.Red, g);

            for (int i = 0; i < edges.Count; i++)
            {
                if (selected != null && edges[i] == selected) //If selected, we want to draw it differently
                {
                    edges[i].Draw(g, true, CustomLines);
                    continue;
                }
                edges[i].Draw(g, false, CustomLines);
            }

            for (int i = 0; i < vertices.Count; i++)
            {
                if (selected != null && vertices[i] == selected) //If selected, we want to draw it differently
                {
                    vertices[i].Draw(g, true, CustomLines);
                    continue;
                }
                vertices[i].Draw(g, false, CustomLines);
            }

            for (int i = 0; i < constraints.Count; i++)
            {
                if (selected != null && constraints[i] == selected) //If selected, we want to draw it differently
                {
                    constraints[i].Draw(g, true, false);
                    continue;
                }
                constraints[i].Draw(g, false, false);
            }

            if (p.Image != null)
                p.Image.Dispose();

            g.Dispose(); 
            p.Image = (Image)bmp;
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
            edges.Add(new Edge(ref v1, ref v2));
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

        public void OffsetElement(Interface.IPolygonElement e, Point p, List<Edge> touchedEdges, List<EdgeConstraint> touchedConstraits)
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
            } else
            {
                var ed = (Edge)e;
                initiallyMoved1 = GetNeighbourTo(ed.from);
                initiallyMoved2 = GetNeighbourFrom(ed.to);
                rightmost1 = initiallyMoved1.IsRightmost(ed.from);
                rightmost2 = initiallyMoved2.IsRightmost(ed.to);
            }
            if (touchedEdges.Count == 0)
                e.Offset(p);

            if(!touchedEdges.Contains(initiallyMoved1)) touchedEdges.Add(initiallyMoved1);
            if(!touchedEdges.Contains(initiallyMoved2)) touchedEdges.Add(initiallyMoved2);

            EdgeConstraint constraint1 = FindConstraint(initiallyMoved1);
            EdgeConstraint constraint2 = FindConstraint(initiallyMoved2);

            if(constraint1 != null && !touchedConstraits.Contains(constraint1))
            {
                touchedConstraits.Add(constraint1);
                constraint1.Offset(p, initiallyMoved1, rightmost1);
                OffsetElement(initiallyMoved1, p, touchedEdges, touchedConstraits);
            }

            if (constraint2 != null && !touchedConstraits.Contains(constraint2))
            {
                touchedConstraits.Add(constraint2);
                constraint2.Offset(p, initiallyMoved2, rightmost2);
                OffsetElement(initiallyMoved2, p, touchedEdges, touchedConstraits);
            }
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
