using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphicsLab1.Interface
{
    public interface IFigure
    {
        Color FigureColor { get; set; }
        ICollection<IConstraint> Constraints { get; set; }
        IFigureElement ClickedOn(Point click);

        void SetSelected(IFigureElement v);
        IFigureElement GetSelected();
        void Draw(Graphics g, Point? cursor = null, bool customLines = false, bool isBeingCreated = false, bool antialiasing = false);
        void Offset(Point p);

        void DeleteElement(IFigureElement element);
    }
}
