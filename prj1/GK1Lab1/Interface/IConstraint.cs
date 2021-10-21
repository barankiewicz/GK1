using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Interface
{
    public interface IConstraint
    {
        int Id { get; }
        void ApplyConstraint();

        void Draw(Graphics g, bool selected, Point? cursor = null);
    }
}
