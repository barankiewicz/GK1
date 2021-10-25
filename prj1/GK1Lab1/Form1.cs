using ComputerGraphicsLab1.Class;
using ComputerGraphicsLab1.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphicsLab1
{
    public partial class Form1 : Form
    {
        InteractionMode InteractionMode;
        int r;
        float wid;
        FigureSet figSet;
        Graphics g;
   
        IFigureElement clickedElement;
        IFigure selectedFigure;
        Point curClick;
        Point labelOffset;
        Stopwatch st;
        string errorMessage;
        bool customLines;

        string graphLoaded = "";
        string graphLoadFailed = "";
        string graphSaved = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            st = new Stopwatch();
            SetStyle(ControlStyles.DoubleBuffer, true);
  
            curClick = new Point(0,0);
            this.KeyPreview = true;
            clickedElement = null;
            selectedFigure = null;
            menuPanel.Width = (int)(this.Width * 0.2);
            pictureContainer.Width = (int)(this.Width * 0.8);
            Screen myScreen = Screen.FromControl(this);
            Rectangle area = myScreen.WorkingArea;
            mainWind.Width = area.Width;
            mainWind.Height = area.Height;
            g = mainWind.CreateGraphics();
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            customLines = false;
            Point labelOffset = Point.Empty;

            r = 5;
            wid = 2f;
            //figSet = new FigureSet();
            figSet = GeneratePolygonExample();
            InteractionMode = InteractionMode.EDIT;
            deleteVertexButton.Enabled = false;
            figSet.Draw(mainWind);
        }

        private FigureSet GeneratePolygonExample()
        {
            FigureSet set = new FigureSet();

            //Figures
            Polygon ret = new Polygon(r, wid, Color.Black);
            ret.AddVertex(new Point(150, 350));

            ret.AddVertex(new Point(150, 200));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            ret.AddVertex(new Point(250, 100));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            ret.AddVertex(new Point(400, 100));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            ret.AddVertex(new Point(500, 200));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            ret.AddVertex(new Point(500, 350));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            ret.AddVertex(new Point(400, 450));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            ret.AddVertex(new Point(250, 450));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            ret.AddEdge(ret.LastVertex, ret.FirstVertex);

            Polygon pol2 = new Polygon(r, wid, Color.Black);
            pol2.AddVertex(new Point(550, 250));

            pol2.AddVertex(new Point(700, 275));
            pol2.AddEdge(pol2.BeforeLastVertex, pol2.LastVertex);

            pol2.AddVertex(new Point(600, 575));
            pol2.AddEdge(pol2.BeforeLastVertex, pol2.LastVertex);

            pol2.AddVertex(new Point(500, 450));
            pol2.AddEdge(pol2.BeforeLastVertex, pol2.LastVertex);

            pol2.AddEdge(pol2.LastVertex, pol2.FirstVertex);

            pol2.Offset(new Point(50, 0));

            Circle c = new Circle(100, new Vertex(new Point(400, 600), r, wid), Color.Black);

            Circle c2 = new Circle(75, new Vertex(new Point(650, 575), r, wid), Color.Black);

            set.AddFigure(ret);
            set.AddFigure(pol2);
            set.AddFigure(c);
            set.AddFigure(c2);

            //Constraints

            set.AddSetLengthConstraint(ret.edges[ret.edges.Count - 2], ret.edges[ret.edges.Count - 2].GetLength(), ret);
            set.AddSetLengthConstraint(pol2.edges[pol2.edges.Count - 2], pol2.edges[pol2.edges.Count - 2].GetLength(), pol2);
            set.AddEqualConstraint(ret.edges[ret.edges.Count - 3], pol2.edges[pol2.edges.Count - 3], ret, pol2);
            set.AddSetRadiusConstraint(c, 100);


            return set;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            menuPanel.Width = (int)(this.Width * 0.2);
            pictureContainer.Width = (int)(this.Width * 0.8);
            
            figSet.Draw(mainWind);
        }

        private void mainWind_MouseDown(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            
            if (e.Button == MouseButtons.Left)
            {
                st.Start();
                switch (InteractionMode)
                {
                    case InteractionMode.CREATE_NEW:
                        figSet.RefreshClickedOnFigureAndElement(loc);
                        if (selectedFigure is Circle)
                        {
                            //Case for circle
                            var circle = (Circle)selectedFigure;
                            var center = circle.Center;

                            if (center.Location.X == -300 && center.Location.Y == -300)
                                circle.Center.SetLocation(loc);
                            else
                            {
                                circle.Radius = circle.GetRadius(loc);
                                selectedFigure = null;
                                InteractionMode = InteractionMode.EDIT;
                                break;
                            }
                        }
                        else if (selectedFigure is Polygon)
                        {
                            var poly = (Polygon)selectedFigure;
                            var clicked = poly.ClickedOn(loc);
                            if (poly.VertexCount >= 3 && clicked == poly.FirstVertex)
                            {
                                poly.AddEdge(poly.LastVertex, poly.FirstVertex);
                                poly.Closed = true;
                                selectedFigure = null;
                                InteractionMode = InteractionMode.EDIT;
                                break;
                            }
                            var v = poly.AddVertex(e.Location);

                            if (poly.VertexCount != 1)
                                poly.AddEdge(poly.BeforeLastVertex, poly.LastVertex);
                        } 
                        break;
                    case InteractionMode.EDIT:
                        figSet.RefreshClickedOnFigureAndElement(loc);
                        if (figSet.SelectedElement != null && figSet.SelectedElement is Vertex)
                            deleteVertexButton.Enabled = true;
                        else
                            deleteVertexButton.Enabled = false;
                        break;
                    case InteractionMode.MOVE:
                        figSet.RefreshClickedOnFigureAndElement(loc);
                        labelOffset = e.Location;
                        break;
                    case InteractionMode.CONSTRAINT_EQUAL:
                        if (figSet.SelectedElement is Edge)
                        {
                            var prevClicked = figSet.SelectedElement;
                            var prevClickedFigure = figSet.SelectedFigure;
                            figSet.RefreshClickedOnFigureAndElement(loc);
                            figSet.AddEqualConstraint((Edge)prevClicked, (Edge)figSet.SelectedElement, (Polygon)prevClickedFigure, (Polygon)figSet.SelectedFigure);
                            InteractionMode = InteractionMode.EDIT;
                        }
                        else
                        {
                            figSet.RefreshClickedOnFigureAndElement(null);
                            figSet.Draw(mainWind);
                            InteractionMode = InteractionMode.EDIT;
                        }
                        break;
                    case InteractionMode.CONSTRAINT_PARALLEL:
                        if (figSet.SelectedElement is Edge)
                        {
                            var prevClicked = figSet.SelectedElement;
                            var prevClickedFigure = figSet.SelectedFigure;
                            figSet.RefreshClickedOnFigureAndElement(loc);
                            figSet.AddParallelConstraint((Edge)prevClicked, (Edge)figSet.SelectedElement, (Polygon)prevClickedFigure, (Polygon)figSet.SelectedFigure);
                            InteractionMode = InteractionMode.EDIT;
                        }
                        else
                        {
                            figSet.RefreshClickedOnFigureAndElement(null);
                            figSet.Draw(mainWind);
                            InteractionMode = InteractionMode.EDIT;
                        }
                        break;
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                //Moze cos do wpisania?
            }
            figSet.Draw(mainWind);
        }

        private void mainWind_MouseUp(object sender, MouseEventArgs e)
        {
            IFigure fig = figSet.SelectedFigure;
            IFigureElement clicked = figSet.SelectedElement;
            if (e.Button == MouseButtons.Left && clicked != null)
            {
                curClick = new Point(0, 0);

                int picX = mainWind.Width;
                int contX = (int)(this.Width * 0.8);
                int picY = mainWind.Height;
                int contY = this.Height;
                
                var toSet = new Point(clicked.GetLocation().X, clicked.GetLocation().Y);
                if (toSet.X < 0)
                    toSet.X = 0;
                else if (toSet.X > contX)
                    toSet.X = contX - 4*r;

                if (toSet.Y < 0)
                    toSet.Y = 0;
                else if (toSet.Y > contY)
                    toSet.Y = contY - 6*r;

                figSet.Draw(mainWind);
                st.Reset();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(InteractionMode != InteractionMode.MOVE && InteractionMode != InteractionMode.EDIT)
            {
                MessageBox.Show("You can only export the project in MOVE or EDIT modes!");
                return;
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                FigureSet.WriteToFile(figSet, saveFileDialog.FileName);
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    figSet = FigureSet.ReadFromFile(openFileDialog.FileName);
                }
                catch (SerializationException)
                {
                    MessageBox.Show(errorMessage, errorMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            figSet.Draw(mainWind);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && figSet.SelectedElement is IConstraint)
            {
                var fig = figSet.SelectedFigure;
                var c = (IConstraint)figSet.SelectedElement;
                fig.DeleteElement(figSet.SelectedElement);
                figSet.DeleteAllConstraints(c);
                figSet.RefreshClickedOnFigureAndElement(null);
            }
            if(e.KeyCode == Keys.Delete && figSet.SelectedFigure is Circle)
            {
                figSet.RemoveFigure(figSet.SelectedFigure);
                figSet.RefreshClickedOnFigureAndElement(null);
                figSet.Draw(mainWind);
                return;
            } else if (e.KeyCode == Keys.Delete && figSet.SelectedElement is Vertex)
            {
                var fig = figSet.SelectedFigure;
                if(fig is Polygon)
                {
                    figSet.DeleteAllConstraints((Vertex)figSet.SelectedElement);
                    fig.DeleteElement(figSet.SelectedElement);
                }
                
                figSet.RefreshClickedOnFigureAndElement(null);
                deleteVertexButton.Enabled = false;
                figSet.Draw(mainWind);
            } else if (e.KeyCode == Keys.Delete && figSet.SelectedElement is IConstraint)
            {
                var fig = figSet.SelectedFigure;
                var c = (IConstraint)figSet.SelectedElement;
                fig.DeleteElement(figSet.SelectedElement);
                figSet.DeleteConstraint(c);
                figSet.RefreshClickedOnFigureAndElement(null);
            }

            figSet.Draw(mainWind);
        }

        private void mainWind_MouseMove(object sender, MouseEventArgs e)
        {
            var fig = figSet.SelectedFigure;
            var el = figSet.SelectedElement;
            if (e.Button == MouseButtons.Left && InteractionMode == InteractionMode.EDIT && el != null)
            {
                if (st.ElapsedMilliseconds >= 10)
                {

                    Point offset = new Point(e.Location.X - el.GetLocation().X, e.Location.Y 
                        - el.GetLocation().Y);
                    Point p = el.GetLocation();
                    p.Offset(offset);

                    //polygon.GetSelected().Offset(offset);
                    if(fig is Polygon)
                    {
                        figSet.OffsetElement(fig, el, offset, new List<IFigure>(), new List<Edge>(), new List<IConstraint>());
                        //poly.OffsetElement(el, offset, new List<Edge>());
                    } else if(fig is Circle && el is Circle)
                    {
                        var circ = (Circle)fig;
                        figSet.OffsetElement(circ, el, e.Location, new List<IFigure>(), new List<Edge>(), new List<IConstraint>());
                        //circ.ChangeSize(e.Location);
                    } else if (fig is Circle && el is Vertex)
                    {
                        fig.Offset(offset);
                    }

                    curClick = e.Location;
                    figSet.Draw(mainWind);
                    st.Restart();
                }
            } else if (e.Button == MouseButtons.None && InteractionMode == InteractionMode.CREATE_NEW)
            {
                if (st.ElapsedMilliseconds >= 10)
                {
                    figSet.Draw(mainWind, e.Location);
                    st.Restart();
                }
            } else if (e.Button == MouseButtons.Left && InteractionMode == InteractionMode.MOVE)
            {
                if (st.ElapsedMilliseconds >= 10)
                {
                    Point offset = new Point(e.X - labelOffset.X, e.Y - labelOffset.Y);
                    labelOffset = e.Location;

                    if (fig == null)
                        figSet.Offset(offset);
                    else
                        fig.Offset(offset);
                    figSet.Draw(mainWind);
                    st.Restart();
                }
            }

        }
        
        private void moveButton_Click(object sender, EventArgs e)
        {
            InteractionMode = InteractionMode.MOVE;
            Cursor.Current = Cursors.Hand;
        }

        private void newPolygonButton_Click(object sender, EventArgs e)
        {
            var fig = new Polygon(r, wid, Color.Black);
            InteractionMode = InteractionMode.CREATE_NEW;
            Cursor.Current = Cursors.Cross;

            selectedFigure = fig;
            figSet.AddFigure(fig);
            figSet.Draw(mainWind);
        }

        private void newCircleButton_Click(object sender, EventArgs e)
        {
            var fig = new Circle(1, new Vertex(new Point(-300, -300), r, wid), Color.Black);
            InteractionMode = InteractionMode.CREATE_NEW;
            Cursor.Current = Cursors.Cross;

            selectedFigure = fig;
            figSet.AddFigure(fig);
            figSet.Draw(mainWind);
        }

        private void editPolygonButton_Click(object sender, EventArgs e)
        {
            InteractionMode = InteractionMode.EDIT;
            Cursor.Current = Cursors.Default;
        }

        private void mainWind_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void customLinesButton_Click(object sender, EventArgs e)
        {
            if (customLines)
            {
                customLines = false;
                figSet.CustomLines = false;
                ((Button)sender).Text = "Draw Custom Lines/Circles (Bresenham Algorithm)";
            }
            else
            {
                customLines = true;
                figSet.CustomLines = true;
                ((Button)sender).Text = "Draw WinForms Lines/Circles";
            }

            figSet.Draw(mainWind);
        }

        private void mainWind_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            figSet.RefreshClickedOnFigureAndElement(loc);
            IFigure fig = figSet.SelectedFigure;
            IFigureElement clicked = figSet.SelectedElement;
            if(InteractionMode == InteractionMode.EDIT && clicked is Edge)
            {
                Polygon poly = new Polygon(1,1,Color.Black);
                Edge cl = (Edge)clicked;
                if (fig is Polygon)
                {
                    poly = (Polygon)fig;
                    figSet.DeleteAllConstraints(cl);
                    poly.SplitEdge(cl, cl.GetLocation());
                }   
                
            }
        }

        private void deleteVertexButton_Click(object sender, EventArgs e)
        {
            if (figSet.SelectedElement is Vertex)
            {
                if(figSet.SelectedFigure is Polygon)
                {
                    var poly = (Polygon)figSet.SelectedFigure;
                    poly.DeleteElement(figSet.SelectedElement);
                } else if (figSet.SelectedFigure is Circle)
                {
                    figSet.RemoveFigure(figSet.SelectedFigure);
                } 

                deleteVertexButton.Enabled = false;
                figSet.RefreshClickedOnFigureAndElement(null);
                figSet.Draw(mainWind);
            }
        }

        private void deleteFigureButton_Click(object sender, EventArgs e)
        {
            if(figSet.SelectedFigure != null)
            {
                figSet.RemoveFigure(figSet.SelectedFigure);
                figSet.RefreshClickedOnFigureAndElement(null);
                figSet.Draw(mainWind);
            }
                
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            figSet.AntiAliasing = ((CheckBox)sender).Checked;
            figSet.Draw(mainWind);
        }

        private void newConstraintRadius_Click(object sender, EventArgs e)
        {
            if (figSet.SelectedFigure is Circle)
            {
                var cir = (Circle)(figSet.SelectedFigure);

                using (RadiusForm form2 = new RadiusForm(cir.Radius))
                {
                    form2.ShowDialog();
                    if (form2.endValue != -1)
                        figSet.AddSetRadiusConstraint(cir, form2.endValue);
                }
                figSet.Draw(mainWind);
            }
            else
            {
                MessageBox.Show("First select a circle, then click the constraint button!");
            }
        }

        private void newConstraintLength_Click(object sender, EventArgs e)
        {
            if (figSet.SelectedElement is Edge)
            {
                var pol = (Polygon)(figSet.SelectedFigure);
                var ed = (Edge)(figSet.SelectedElement);

                using (RadiusForm form2 = new RadiusForm(ed.GetLength()))
                {
                    form2.ShowDialog();
                    if (form2.endValue != -1)
                        figSet.AddSetLengthConstraint(ed, form2.endValue, pol);
                }
                figSet.Draw(mainWind);
            }
            else
            {
                MessageBox.Show("First select a circle, then click the constraint button!");
            }
        }

        private void newConstraintEqualEdges_Click(object sender, EventArgs e)
        {
            if (figSet.SelectedElement is Edge)
            {
                InteractionMode = InteractionMode.CONSTRAINT_EQUAL;
                clickedElement = figSet.SelectedElement;
            }
            else
            {
                MessageBox.Show("First select an edge, then click the constraint button, then select the 2nd edge!");
            }
        }

        private void newConstraintParallel_Click(object sender, EventArgs e)
        {
            if (figSet.SelectedElement is Edge)
            {
                InteractionMode = InteractionMode.CONSTRAINT_PARALLEL;
                clickedElement = figSet.SelectedElement;
            }
            else
            {
                MessageBox.Show("First select an edge, then click the constraint button, then select the 2nd edge!");
            }
        }
    }
}
