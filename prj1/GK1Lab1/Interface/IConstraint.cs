using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Interface
{
    public interface IConstraint : IFigureElement
    {
        int Id { get; }
        void ApplyConstraint(Point p, IFigureElement e, bool rightmost);

        void Draw(Graphics g, bool selected, IFigure f);

        bool ContainsElement(IFigureElement e);
    }
}
