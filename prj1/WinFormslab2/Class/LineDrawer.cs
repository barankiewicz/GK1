using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Class
{
    static class LineDrawer
    {
        public static void DrawCustomLine(Point p1, Point p2, Color color, Graphics g)
        {
            int w = p2.X - p1.X;
            int h = p2.Y - p1.Y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (longest <= shortest)
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) 
                    dy2 = -1; 
                else if (h > 0) 
                    dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                g.FillRectangle(new SolidBrush(color), p1.X, p1.Y, 1, 1);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    p1.X += dx1;
                    p1.Y += dy1;
                }
                else
                {
                    p1.X += dx2;
                    p1.Y += dy2;
                }
            }
        }
    }
}
