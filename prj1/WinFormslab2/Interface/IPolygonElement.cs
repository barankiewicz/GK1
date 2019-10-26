using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Interface
{
    public interface IPolygonElement
    {
        void Draw(Graphics g, bool selected, bool customLines = false);
        Point GetLocation();
        bool IsClicked(Point p);
        void Offset(Point p);
    }

}
