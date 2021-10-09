using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab1.Class
{
    static class LineDrawer
    {
        public static void DrawCustomLine(Point p1, Point p2, Color color, Graphics g, bool antialiased)
        {
            if (antialiased)
                DrawXiaolinWuLine(p1, p2, color, g);
            else
                DrawBresenhamLine(p1, p2, color, g);
        }
        private static void DrawBresenhamLine(Point p1, Point p2, Color color, Graphics g)
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


        private static void Plot(Graphics g, double x, double y, double c, Color col)
        {
            int alpha = (int)(c * 255);
            if (alpha > 255) alpha = 255;
            if (alpha < 0) alpha = 0;
            Color color = Color.FromArgb(alpha, col);

            g.FillRectangle(new SolidBrush(color), (int)x, (int)y, 1, 1);
        }

        private static void DrawXiaolinWuLine(Point p0, Point p1, Color color, Graphics g)
        {
            double x0 = p0.X;
            double y0 = p0.Y;

            double x1 = p1.X;
            double y1 = p1.Y;
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            double temp;
            if (steep)
            {
                temp = x0; x0 = y0; y0 = temp;
                temp = x1; x1 = y1; y1 = temp;
            }
            if (x0 > x1)
            {
                temp = x0; x0 = x1; x1 = temp;
                temp = y0; y0 = y1; y1 = temp;
            }

            double dx = x1 - x0;
            double dy = y1 - y0;
            double gradient = dy / dx;

            double xEnd = Round(x0);
            double yEnd = y0 + gradient * (xEnd - x0);
            double xGap = RFPart(x0 + 0.5);
            double xPixel1 = xEnd;
            double yPixel1 = IPart(yEnd);

            if (steep)
            {
                Plot(g, yPixel1, xPixel1, RFPart(yEnd) * xGap, color);
                Plot(g, yPixel1 + 1, xPixel1, FPart(yEnd) * xGap, color);
            }
            else
            {
                Plot(g, xPixel1, yPixel1, RFPart(yEnd) * xGap, color);
                Plot(g, xPixel1, yPixel1 + 1, FPart(yEnd) * xGap, color);
            }
            double intery = yEnd + gradient;

            xEnd = Round(x1);
            yEnd = y1 + gradient * (xEnd - x1);
            xGap = FPart(x1 + 0.5);
            double xPixel2 = xEnd;
            double yPixel2 = IPart(yEnd);
            if (steep)
            {
                Plot(g, yPixel2, xPixel2, RFPart(yEnd) * xGap, color);
                Plot(g, yPixel2 + 1, xPixel2, FPart(yEnd) * xGap, color);
            }
            else
            {
                Plot(g, xPixel2, yPixel2, RFPart(yEnd) * xGap, color);
                Plot(g, xPixel2, yPixel2 + 1, FPart(yEnd) * xGap, color);
            }

            if (steep)
            {
                for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                {
                    Plot(g, IPart(intery), x, RFPart(intery), color);
                    Plot(g, IPart(intery) + 1, x, FPart(intery), color);
                    intery += gradient;
                }
            }
            else
            {
                for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                {
                    Plot(g, x, IPart(intery), RFPart(intery), color);
                    Plot(g, x, IPart(intery) + 1, FPart(intery), color);
                    intery += gradient;
                }
            }
        }

        static double Distance(int x, int y)
        {
            double real_point = Math.Sqrt(Math.Pow(x, 2) - Math.Pow(y, 2));
            return (Math.Ceiling(real_point) - real_point);
        }

        private static int NewColor(double i)
        {
            return (int)Math.Round((i * 127));
        }

        static int IPart(double x) { return (int)x; }

        static int Round(double x) { return IPart(x + 0.5); }

        static double FPart(double x)
        {
            if (x < 0) return (1 - (x - Math.Floor(x)));
            return (x - Math.Floor(x));
        }

        static double RFPart(double x)
        {
            return 1 - FPart(x);
        }

        public static void DrawCustomEllipse(Graphics g, Point Center, int radius, Color color, bool antialiasing)
        {
            if (antialiasing)
                DrawXiaolinWuCircle(g, Center, radius, color);
            else
                DrawBresenhamCircle(g, Center, radius, color);
        }

        private static void DrawBresenhamCircle(Graphics g, Point Center, int radius, Color color)
        {
            int d = (5 - radius * 4) / 4;
            int x = 0;
            int y = radius;

            var centerX = Center.X;
            var centerY = Center.Y;

            do
            {
                // ensure index is in range before setting (depends on your image implementation)
                // in this case we check if the pixel location is within the bounds of the image before setting the pixel
                g.FillRectangle(new SolidBrush(color), centerX + x, centerY + y, 1, 1);
                g.FillRectangle(new SolidBrush(color), centerX + x, centerY - y, 1, 1);
                g.FillRectangle(new SolidBrush(color), centerX - x, centerY + y, 1, 1);
                g.FillRectangle(new SolidBrush(color), centerX - x, centerY - y, 1, 1);

                g.FillRectangle(new SolidBrush(color), centerX + y, centerY + x, 1, 1);
                g.FillRectangle(new SolidBrush(color), centerX + y, centerY - x, 1, 1);
                g.FillRectangle(new SolidBrush(color), centerX - y, centerY + x, 1, 1);
                g.FillRectangle(new SolidBrush(color), centerX - y, centerY - x, 1, 1);

                if (d < 0)
                {
                    d += 2 * x + 1;
                }
                else
                {
                    d += 2 * (x - y) + 1;
                    y--;
                }
                x++;
            } while (x <= y);
        }

        private static void DrawXiaolinWuCircle(Graphics g, Point Center, int radius, Color color)
        {
            int offset_x = Center.X;
            int offset_y = Center.Y;
            int r = radius;
            int x = r;
            int y = -1;
            double t = 0;
            while (x - 1 > y)
            {
                y++;
                double current_distance = Distance(r, y);
                if (current_distance < t)
                {
                    x--;
                }
                //shades
                int transparency = NewColor(current_distance);
                int alpha = transparency;
                int alpha2 = 127 - transparency;
                Color c2 = Color.FromArgb(255, color.R, color.G, color.B);
                Color c3 = Color.FromArgb(alpha2, color.R, color.G, color.B);
                Color c4 = Color.FromArgb(alpha, color.R, color.G, color.B);

                g.FillRectangle(new SolidBrush(c2), new Rectangle((x + offset_x), (y + offset_y), 1, 1));
                g.FillRectangle(new SolidBrush(c4), new Rectangle((x + offset_x - 1), (y + offset_y), 1, 1));
                g.FillRectangle(new SolidBrush(c3), new Rectangle((x + offset_x + 1), (y + offset_y), 1, 1));

                g.FillRectangle(new SolidBrush(c2), new Rectangle((y + offset_x), (x + offset_y), 1, 1));
                g.FillRectangle(new SolidBrush(c4), new Rectangle((y + offset_x), (x + offset_y - 1), 1, 1));
                g.FillRectangle(new SolidBrush(c3), new Rectangle((y + offset_x), (x + offset_y + 1), 1, 1));

                g.FillRectangle(new SolidBrush(c2), new Rectangle((offset_x - x), (y + offset_y), 1, 1));
                g.FillRectangle(new SolidBrush(c4), new Rectangle((offset_x - x + 1), (y + offset_y), 1, 1));
                g.FillRectangle(new SolidBrush(c3), new Rectangle((offset_x - x - 1), (y + offset_y), 1, 1));

                g.FillRectangle(new SolidBrush(c2), new Rectangle((offset_x - y), (x + offset_y), 1, 1));
                g.FillRectangle(new SolidBrush(c4), new Rectangle((offset_x - y), (x + offset_y - 1), 1, 1));
                g.FillRectangle(new SolidBrush(c3), new Rectangle((offset_x - y), (x + offset_y + 1), 1, 1));

                g.FillRectangle(new SolidBrush(c2), new Rectangle((x + offset_x), (offset_y - y), 1, 1));
                g.FillRectangle(new SolidBrush(c4), new Rectangle((x + offset_x - 1), (offset_y - y), 1, 1));
                g.FillRectangle(new SolidBrush(c3), new Rectangle((x + offset_x + 1), (offset_y - y), 1, 1));

                //UP
                g.FillRectangle(new SolidBrush(c2), new Rectangle((y + offset_x), (offset_y - x), 1, 1));
                g.FillRectangle(new SolidBrush(c3), new Rectangle((y + offset_x), (offset_y - x - 1), 1, 1));
                g.FillRectangle(new SolidBrush(c4), new Rectangle((y + offset_x), (offset_y - x + 1), 1, 1));

                g.FillRectangle(new SolidBrush(c2), new Rectangle((offset_x - y), (offset_y - x), 1, 1));
                g.FillRectangle(new SolidBrush(c3), new Rectangle((offset_x - y), (offset_y - x - 1), 1, 1));
                g.FillRectangle(new SolidBrush(c4), new Rectangle((offset_x - y), (offset_y - x + 1), 1, 1));

                g.FillRectangle(new SolidBrush(c2), new Rectangle((offset_x - x), (offset_y - y), 1, 1));
                g.FillRectangle(new SolidBrush(c3), new Rectangle((offset_x - x - 1), (offset_y - y), 1, 1));
                g.FillRectangle(new SolidBrush(c4), new Rectangle((offset_x - x + 1), (offset_y - y), 1, 1));
                t = current_distance;
            }
        }
    }
}
