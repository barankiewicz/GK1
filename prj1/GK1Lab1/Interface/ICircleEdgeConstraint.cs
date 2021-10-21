using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Interface
{
    public interface ICircleEdgeConstraint : IConstraint
    {
        Circle Circle { get; }
        Edge Edge { get; }
    }
}
