using ComputerGraphicsLab1.Constraint;
using ComputerGraphicsLab1.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphicsLab1.Class
{
    [Serializable]
    public class FigureSet
    {
        ICollection<IFigure> Figures { get; }
        public IFigure SelectedFigure { get; set; }
        public IFigureElement SelectedElement { get; set; }

        ICollection<IConstraint> Constraints { get; }
        public bool AntiAliasing { get; set; }

        public bool CustomLines { get; set; }

        int Id { get; set; }

        public FigureSet()
        {
            Figures = new List<IFigure>();
            Constraints = new List<IConstraint>();
            CustomLines = false;
            Id = 1;
        }

        public void AddFigure(IFigure fig)
        {
            Figures.Add(fig);
        }

        public void RemoveFigure(IFigure fig)
        {
            foreach (IConstraint c in fig.Constraints)
                DeleteConstraint(c);
            if (Figures.Contains(fig))
                Figures.Remove(fig);
        }

        public void RefreshClickedOnFigureAndElement(Point? click = null)
        {
            SelectedFigure = null;
            SelectedElement = null;

            foreach (IFigure fig in Figures)
                fig.SetSelected(null);
            if (click == null)
            {
                SelectedFigure = null;
                SelectedElement = null;

                foreach (IFigure fig in Figures)
                    fig.SetSelected(null);
            }
            else
            {
                SelectedFigure = ClickedOnFigure((Point)click);

                if(SelectedFigure == null)
                {
                    SelectedFigure = null;
                    SelectedElement = null;

                    foreach (IFigure fig in Figures)
                        fig.SetSelected(null);
                } else
                {
                    SelectedElement = SelectedFigure.ClickedOn((Point)click);
                    SelectedFigure.SetSelected(SelectedElement);
                }

            }
                
        }

        private IFigure ClickedOnFigure(Point click)
        {
            int i = Figures.Count - 1;
            for (; i>=0 && Figures.ElementAt(i).ClickedOn(click) == null; i--) ;
            if (i == -1)
                return null;


            return Figures.ElementAt(i);
        }

        public void Draw(PictureBox p, Point? cursor = null)
        {
            Bitmap bmp = new Bitmap(p.Width, p.Width);
            Graphics g = Graphics.FromImage(bmp);

            for(int i = Figures.Count - 1; i >= 0; i--)
            {
                Figures.ElementAt(i).Draw(g, cursor, CustomLines, false, AntiAliasing);
                cursor = null;
            }

            if (p.Image != null)
                p.Image.Dispose();

            g.Dispose();
            p.Image = (Image)bmp;
        }

        public void Offset(Point p)
        {
            foreach (IFigure f in Figures)
                f.Offset(p);
        }

        public void OffsetElement(IFigure f, IFigureElement e, Point p, List<IFigure> touchedFigures, List<Edge> touchedEdges, List<IConstraint> touchedConstraits)
        {
            if (touchedFigures.Count == Figures.Count)
                return;
            Edge initiallyMoved1;
            Edge initiallyMoved2;
            Edge initiallyMoved3 = null;
            bool rightmost1;
            bool rightmost2;
            bool rightmost3 = false;
            IFigure f2;

            if (e is Vertex)
            {
                Vertex v = (Vertex)e;
                Polygon pol = (Polygon)f;
                initiallyMoved1 = pol.GetNeighbourFrom(v);
                initiallyMoved2 = pol.GetNeighbourTo(v);
                rightmost1 = initiallyMoved1.IsRightmost(v);
                rightmost2 = initiallyMoved2.IsRightmost(v);
            }
            else if (e is Edge)
            {
                var ed = (Edge)e;
                Polygon pol = (Polygon)f;
                initiallyMoved1 = pol.GetNeighbourTo(ed.from);
                initiallyMoved2 = pol.GetNeighbourFrom(ed.to);
                initiallyMoved3 = ed;
                rightmost1 = initiallyMoved1.IsRightmost(ed.from);
                rightmost2 = initiallyMoved2.IsRightmost(ed.to);
            }
            else if (e is Circle)
            {
                var cir = (Circle)f;
                cir.ChangeSize(p);

                IConstraint constr = FindConstraint(cir);
                if (constr != null)
                    constr.ApplyConstraint(p, cir, false);

                return;
            }
            else
                return;

            if (touchedEdges.Count == 0)
                e.Offset(p);

            if (!touchedEdges.Contains(initiallyMoved1)) touchedEdges.Add(initiallyMoved1);
            if (!touchedEdges.Contains(initiallyMoved2)) touchedEdges.Add(initiallyMoved2);
            if (initiallyMoved3 != null && !touchedEdges.Contains((Edge)e)) touchedEdges.Add((Edge)e);

            IConstraint constraint1 = FindConstraint(initiallyMoved1);
            IConstraint constraint2 = FindConstraint(initiallyMoved2);
            IConstraint constraint3 = FindConstraint(initiallyMoved3);

            if (constraint1 != null && !touchedConstraits.Contains(constraint1))
            {
                touchedConstraits.Add(constraint1);
                constraint1.ApplyConstraint(p, initiallyMoved1, rightmost1);
                OffsetElement(f, initiallyMoved1, p, touchedFigures, touchedEdges, touchedConstraits);

                if(constraint1 is EqualConstraint)
                {
                    var eq = (EqualConstraint)constraint1;
                    if(!f.HasElement(eq.Edge1))
                    {
                        f2 = FindFigure(eq.Edge1);
                        OffsetElement(f2, eq.Edge1, p, touchedFigures, touchedEdges, touchedConstraits);
                    }

                    if (!f.HasElement(eq.Edge2))
                    {
                        f2 = FindFigure(eq.Edge2);
                        OffsetElement(f2, eq.Edge2, p, touchedFigures, touchedEdges, touchedConstraits);
                    }
                }
            }

            if (constraint2 != null && !touchedConstraits.Contains(constraint2))
            {
                touchedConstraits.Add(constraint2);
                constraint2.ApplyConstraint(p, initiallyMoved2, rightmost2);
                OffsetElement(f, initiallyMoved2, p, touchedFigures, touchedEdges, touchedConstraits);

                if (constraint2 is EqualConstraint)
                {
                    var eq = (EqualConstraint)constraint2;
                    if (!f.HasElement(eq.Edge1))
                    {
                        f2 = FindFigure(eq.Edge1);
                        OffsetElement(f2, eq.Edge1, p, touchedFigures, touchedEdges, touchedConstraits);
                    }

                    if (!f.HasElement(eq.Edge2))
                    {
                        f2 = FindFigure(eq.Edge2);
                        OffsetElement(f2, eq.Edge2, p, touchedFigures, touchedEdges, touchedConstraits);
                    }
                }
            }

            if (constraint3 != null && !touchedConstraits.Contains(constraint3))
            {
                touchedConstraits.Add(constraint3);
                constraint3.ApplyConstraint(p, e, rightmost3);
                OffsetElement(f, (Edge)e, p, touchedFigures, touchedEdges, touchedConstraits);

                if (constraint3 is EqualConstraint)
                {
                    var eq = (EqualConstraint)constraint3;
                    if (!f.HasElement(eq.Edge1))
                    {
                        f2 = FindFigure(eq.Edge1);
                        OffsetElement(f2, eq.Edge1, p, touchedFigures, touchedEdges, touchedConstraits);
                    }

                    if (!f.HasElement(eq.Edge2))
                    {
                        f2 = FindFigure(eq.Edge2);
                        OffsetElement(f2, eq.Edge2, p, touchedFigures, touchedEdges, touchedConstraits);
                    }
                }
            }
        }

        private IFigure FindFigure(IFigureElement e)
        {
            foreach (IFigure f in Figures)
                if (f.HasElement(e))
                    return f;
            return null;
        }

        public IConstraint FindConstraint(IFigureElement e)
        {
            if (e == null)
                return null;
            foreach (IConstraint c in Constraints)
            {
                if (c.ContainsElement(e))
                    return c;
            }
            return null;
        }

        public static void WriteToFile(FigureSet set, string path)
        {
            var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, set);
            fs.Close();
        }

        public static FigureSet ReadFromFile(string path)
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            FigureSet obj = (FigureSet)bf.Deserialize(fs);
            fs.Close();
            return obj;
        }

        public void AddEqualConstraint(Edge e1, Edge e2, Polygon p1, Polygon p2)
        {
            var c1 = FindConstraint(e1);
            var c2 = FindConstraint(e2);

            if (e1 == null || e2 == null)
            {
                MessageBox.Show("Click on the 2nd edge!");
                return;
            }

            if (c1 != null || c2 != null)
            {
                MessageBox.Show("There's already a relation on one of the edges!");
                return;
            }

            var c = new EqualConstraint(e1, e2, Constraints.Count + 1);
            Constraints.Add(c);
            p1.Constraints.Add(c);
            p2.Constraints.Add(c);

            OffsetElement(p1, e1, new Point(0, 0), new List<IFigure>(), new List<Edge>(), new List<IConstraint>());
            Id++;
        }

        public void AddParallelConstraint(Edge e1, Edge e2, Polygon p1, Polygon p2)
        {
            if (e1.IsNeighbour(e2))
            {
                MessageBox.Show("Can't add the Parallel Constraint between neighbouring edges!");
                return;
            }

            var c1 = FindConstraint(e1);
            var c2 = FindConstraint(e2);

            if (e1 == null || e2 == null)
            {
                MessageBox.Show("Click on the 2nd edge!");
                return;
            }

            if (c1 != null || c2 != null)
            {
                MessageBox.Show("There's already a relation on one of the edges!");
                return;
            }

            var c = new ParallelConstraint(e1, e2, Constraints.Count + 1);
            Constraints.Add(c);
            p1.Constraints.Add(c);
            p2.Constraints.Add(c);

            OffsetElement(p1, e1, new Point(0, 0), new List<IFigure>(), new List<Edge>(), new List<IConstraint>());
            Id++;
        }

        internal void AddSetRadiusConstraint(Circle c, double constRadius)
        {
            var con = new SetRadiusConstraint(c, constRadius, Constraints.Count + 1);
            Constraints.Add(con);
            c.Constraints.Add(con);

            OffsetElement(c, c, new Point(0, 0), new List<IFigure>(), new List<Edge>(), new List<IConstraint>());
            Id++;
        }

        internal void AddSetLengthConstraint(Edge e, double constLength, Polygon p)
        {
            var c1 = FindConstraint(e);

            if (c1 != null)
            {
                MessageBox.Show("There's already a relation on one of the edges!");
                return;
            }

            var con = new SetLengthConstraint(e, constLength, Constraints.Count + 1);
            Constraints.Add(con);
            p.Constraints.Add(con);

            OffsetElement(p, e, new Point(0, 0), new List<IFigure>(), new List<Edge>(), new List<IConstraint>());
            Id++;
        }

        internal void DeleteConstraint(IConstraint c)
        {
            //Id--;
            Constraints.Remove(c);
        }

        public void DeleteAllConstraints(Edge cl)
        {
            var con = FindConstraint(cl);
            var fig = FindFigure(cl);

            if(con != null)
            {
                if (con is EqualConstraint)
                {
                    var eq = (EqualConstraint)con;

                    if (!fig.HasElement(eq.Edge1))
                    {
                        var fig2 = FindFigure(eq.Edge1);
                        fig2.DeleteElement(eq);
                    }

                    if (!fig.HasElement(eq.Edge2))
                    {
                        var fig2 = FindFigure(eq.Edge2);
                        fig2.DeleteElement(eq);
                    }

                    fig.DeleteElement(con);

                }
                else
                {
                    fig.DeleteElement(con);
                }
                DeleteConstraint(con);
            }
        }

        public void DeleteAllConstraints(Vertex v)
        {
            var fig = (Polygon)FindFigure(v);

            var e1 = fig.GetNeighbourFrom(v);
            var e2 = fig.GetNeighbourTo(v);

            DeleteAllConstraints(e1);
            DeleteAllConstraints(e2);
        }

        public void DeleteAllConstraints(IConstraint c)
        {
            if(c is EqualConstraint)
            {
                var eq = (EqualConstraint)c;

                DeleteAllConstraints(eq.Edge1);
            } else
            {
                DeleteConstraint(c);
            }
        }
    }
}
