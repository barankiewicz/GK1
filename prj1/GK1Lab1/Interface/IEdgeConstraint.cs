using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Interface
{
    public interface IEdgeConstraint : IConstraint
    {
        Edge Edge { get; }
    }
}
