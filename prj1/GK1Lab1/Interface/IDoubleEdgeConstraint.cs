using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Interface
{
    public interface IDoubleEdgeConstraint : IConstraint
    {
        Edge Edge1 { get; }
        Edge Edge2 { get; }
    }
}
