using ComputerGraphicsLab1.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Constraint
{
    [Serializable]
    public class TangentConstraint : ICircleEdgeConstraint, IFigureElement
    {
        public int Id { get; set; }
        public Circle Circle => throw new NotImplementedException();

        public Edge Edge => throw new NotImplementedException();

        public void ApplyConstraint()
        {
            throw new NotImplementedException();
        }

        public void ApplyConstraint(Point p, IFigureElement e, bool rightmost)
        {
            throw new NotImplementedException();
        }

        public bool ContainsElement(IFigureElement e)
        {
            throw new NotImplementedException();
        }

        public void Draw(Graphics g, bool selected, IFigure f)
        {
            throw new NotImplementedException();
        }

        public Point GetLocation()
        {
            throw new NotImplementedException();
        }

        public bool IsClicked(Point p)
        {
            throw new NotImplementedException();
        }

        public void Offset(Point p)
        {
            throw new NotImplementedException();
        }
    }
}
