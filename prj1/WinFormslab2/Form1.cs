using ComputerGraphicsLab1.Class;
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
        Polygon polygon;
        Graphics g;
        bool isMiddleClicked;
        bool isLeft;
        Interface.IPolygonElement clickedElement;
        Interface.IPolygonElement clickedElementSecond;
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
            isLeft = false;
            st = new Stopwatch();
            SetStyle(ControlStyles.DoubleBuffer, true);
            isMiddleClicked = false;
            curClick = new Point(0,0);
            this.KeyPreview = true;
            clickedElement = null;
            clickedElementSecond = null;
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
            polygon = GeneratePolygonExample();
            InteractionMode = InteractionMode.EDIT;
            deleteVertexButton.Enabled = false;
            polygon.DrawPolygon(mainWind);
        }

        private Polygon GeneratePolygonExample()
        {
            Polygon ret = new Polygon(r, wid);
            var v1 = ret.AddVertex(new Point(150, 350));

            var v2 = ret.AddVertex(new Point(150, 200));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            var v3 = ret.AddVertex(new Point(250, 100));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            var v4 = ret.AddVertex(new Point(400, 100));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            var v5 = ret.AddVertex(new Point(500, 200));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            var v6 = ret.AddVertex(new Point(500, 350));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            var v7 = ret.AddVertex(new Point(400, 450));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            var v8 = ret.AddVertex(new Point(250, 450));
            ret.AddEdge(ret.BeforeLastVertex, ret.LastVertex);

            ret.AddEdge(ret.LastVertex, ret.FirstVertex);

            return ret;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            menuPanel.Width = (int)(this.Width * 0.2);
            pictureContainer.Width = (int)(this.Width * 0.8);
            
            polygon.DrawPolygon(mainWind);
        }

        private void mainWind_MouseDown(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                st.Start();
                Interface.IPolygonElement clicked = polygon.ClickedOn(loc);
                switch (InteractionMode)
                {
                    case InteractionMode.CREATE_NEW:
                        if(polygon.VertexCount >= 2 && clicked == polygon.FirstVertex)
                        {
                            polygon.AddEdge(polygon.LastVertex, polygon.FirstVertex);
                            polygon.Closed = true;
                            InteractionMode = InteractionMode.EDIT;
                            break;
                        }
                        var v = polygon.AddVertex(e.Location);

                        if (polygon.VertexCount != 1)
                        {
                            polygon.AddEdge(polygon.BeforeLastVertex, polygon.LastVertex);
                        }
                            
                        break;
                    case InteractionMode.EDIT:
                        if (polygon.ClickedOn(loc) == null)
                        {
                            clickedElement = null;
                            polygon.SetSelected(null);
                            polygon.DrawPolygon(mainWind);
                            deleteVertexButton.Enabled = false;
                        }
                        else
                        {
                            clickedElement = clicked;
                            polygon.SetSelected(clicked);
                            polygon.DrawPolygon(mainWind);
                            deleteVertexButton.Enabled = true;
                        }
                        break;
                    case InteractionMode.MOVE:
                        labelOffset = e.Location;
                        break;
                    case InteractionMode.CONSTRAINT_EQUAL:
                        if (polygon.ClickedOn(loc) is Edge)
                        {
                            polygon.AddConstraint(EdgeConstraintType.CONSTRAINT_EQUAL, (Edge)clickedElement, (Edge)polygon.ClickedOn(loc));
                            InteractionMode = InteractionMode.EDIT;
                        } else
                        {
                            clickedElement = null;
                            clickedElementSecond = null;
                            polygon.SetSelected(null);
                            polygon.DrawPolygon(mainWind);
                            InteractionMode = InteractionMode.EDIT;
                        }
                        break;
                    case InteractionMode.CONSTRAINT_PARALLEL:
                        if (clicked is Edge)
                        {
                            polygon.AddConstraint(EdgeConstraintType.CONSTRAINT_PARALLEL, (Edge)clickedElement, (Edge)clicked);
                            InteractionMode = InteractionMode.EDIT;
                        } else
                        {
                            clickedElement = null;
                            clickedElementSecond = null;
                            polygon.SetSelected(null);
                            polygon.DrawPolygon(mainWind);
                            InteractionMode = InteractionMode.EDIT;
                        }
                        break;
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                //Moze cos do wpisania?
            }
            polygon.DrawPolygon(mainWind);
        }

        private void mainWind_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && polygon.GetSelected() != null)
            {
                isMiddleClicked = false;
                curClick = new Point(0, 0);

                int picX = mainWind.Width;
                int contX = (int)(this.Width * 0.8);
                int picY = mainWind.Height;
                int contY = this.Height;
                
                var toSet = new Point(polygon.GetSelected().GetLocation().X, polygon.GetSelected().GetLocation().Y);
                if (toSet.X < 0)
                    toSet.X = 0;
                else if (toSet.X > contX)
                    toSet.X = contX - 4*r;

                if (toSet.Y < 0)
                    toSet.Y = 0;
                else if (toSet.Y > contY)
                    toSet.Y = contY - 6*r;

                //middleClicked.Location = toSet;
                polygon.DrawPolygon(mainWind);
                st.Reset();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                Polygon.WriteToFile(polygon, saveFileDialog.FileName);
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    polygon = Polygon.ReadFromFile(openFileDialog.FileName);
                }
                catch (SerializationException)
                {
                    MessageBox.Show(errorMessage, errorMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            polygon.DrawPolygon(mainWind);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && polygon.GetSelected() is Vertex)
            {
                polygon.DeleteElement(polygon.GetSelected());
                polygon.SetSelected(null);
                deleteVertexButton.Enabled = false;

                polygon.DrawPolygon(mainWind);
            } else if (e.KeyCode == Keys.Delete && polygon.GetSelected() is EdgeConstraint)
            {
                polygon.DeleteElement(polygon.GetSelected());
                polygon.SetSelected(null);

                polygon.DrawPolygon(mainWind);
            }
        }

        private void mainWind_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && InteractionMode == InteractionMode.EDIT && polygon.GetSelected() != null)
            {
                if (st.ElapsedMilliseconds >= 10)
                {
                    Point offset = new Point(e.Location.X - polygon.GetSelected().GetLocation().X, e.Location.Y 
                        - polygon.GetSelected().GetLocation().Y);
                    Point p = polygon.GetSelected().GetLocation();
                    p.Offset(offset);

                    //polygon.GetSelected().Offset(offset);
                    polygon.OffsetElement(polygon.GetSelected(), offset, new List<Edge>(), new List<EdgeConstraint>());
                    //((Vertex)(middleClicked)).Location = p;
                    curClick = e.Location;

                    polygon.DrawPolygon(mainWind);
                    st.Restart();
                }
            } else if (e.Button == MouseButtons.None && InteractionMode == InteractionMode.CREATE_NEW)
            {
                if (st.ElapsedMilliseconds >= 10)
                {
                    polygon.DrawPolygon(mainWind, e.Location);
                    st.Restart();
                }
            } else if (e.Button == MouseButtons.Left && InteractionMode == InteractionMode.MOVE)
            {
                if (st.ElapsedMilliseconds >= 10)
                {
                    Point offset = new Point(e.X - labelOffset.X, e.Y - labelOffset.Y);
                    labelOffset = e.Location;
                    polygon.Offset(offset);
                    polygon.DrawPolygon(mainWind);
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
            polygon = new Polygon(r, wid);
            InteractionMode = InteractionMode.CREATE_NEW;
            Cursor.Current = Cursors.Cross;
            polygon.DrawPolygon(mainWind);
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
                polygon.CustomLines = false;
                ((Button)sender).Text = "Draw Custom Lines (Bresenham Algorithm)";
            }
            else
            {
                customLines = true;
                polygon.CustomLines = true;
                ((Button)sender).Text = "Draw WinForms Lines";
            }

            polygon.DrawPolygon(mainWind);
        }

        private void mainWind_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            Interface.IPolygonElement clicked = polygon.ClickedOn(loc);
            if(InteractionMode == InteractionMode.EDIT && clicked is Edge)
            {
                Edge cl = (Edge)clicked;
                Point newVertexLocation = cl.GetLocation();

                Vertex from = cl.from;
                Vertex to = cl.to;

                polygon.DeleteEdge((Edge)clicked);

                polygon.AddVertex(newVertexLocation);
                polygon.AddEdge(from, polygon.LastVertex);
                polygon.AddEdge(polygon.LastVertex, to);
            }
        }

        private void deleteVertexButton_Click(object sender, EventArgs e)
        {
            if (polygon.GetSelected() is Vertex)
            {
                polygon.DeleteElement(polygon.GetSelected());
                polygon.SetSelected(null);
                deleteVertexButton.Enabled = false;

                polygon.DrawPolygon(mainWind);
            }
        }

        private void equalContraintButton_Click(object sender, EventArgs e)
        {
            if (polygon.GetSelected() is Edge)
            {
                InteractionMode = InteractionMode.CONSTRAINT_EQUAL;
                //polygon.SetSelected(null);
                clickedElement = polygon.GetSelected();

            } else
            {
                MessageBox.Show("First select an edge, then click the constraint button, then select the 2nd edge!");
            }
        }

        private void parallelConstraintButton_Click(object sender, EventArgs e)
        {
            if (polygon.GetSelected() is Edge)
            {
                InteractionMode = InteractionMode.CONSTRAINT_PARALLEL;
                //polygon.SetSelected(null);
                clickedElement = polygon.GetSelected();
            }
            else
            {
                MessageBox.Show("First select an edge, then click the constraint button, then select the 2nd edge!");
            }
        }

        private void removeConstraintButton_Click(object sender, EventArgs e)
        {
            if (polygon.GetSelected() is Edge)
            {
                //polygon.SetSelected(null);
                clickedElement = polygon.GetSelected();
            }
            else
            {
                MessageBox.Show("First select an edge, then click the button!");
            }
        }
    }
}
