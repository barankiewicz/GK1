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
        public bool AntiAliasing { get; set; }

        public bool CustomLines { get; set; }

        public FigureSet()
        {
            Figures = new List<IFigure>();
            CustomLines = false;
        }

        public void AddFigure(IFigure fig)
        {
            Figures.Add(fig);
        }

        public void RemoveFigure(IFigure fig)
        {
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
    }
}
