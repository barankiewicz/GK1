using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Interface
{
    interface IConstraint
    {
        IFigure Figure1 { get; }

        void ApplyConstraint();
    }
}
