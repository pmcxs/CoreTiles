using System;
using SixLabors.ImageSharp.PixelFormats;

namespace CoreTiles.Drawing
{
    public static partial class LineDrawingExtensions
    {
        public static class XiaolinWuAlgoriithm
        {
            static int ipart(double x)
            {
                return (int)x;
            }

            static double fpart(double x)
            {
                if (x < 0)
                    return 1 - (x - Math.Floor(x));
                return x - Math.Floor(x);
            }

            static double rfpart(double x)
            {
                return 1 - fpart(x);
            }

            static void swap(ref double item1, ref double item2)
            {
                double temp = item1;
                item1 = item2;
                item2 = temp;
            }

            public static void DrawLine(Span<Rgba32> g, double x0, double y0, double x1, double y1, Rgba32 color, int width, int height)
            {

                bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

                if (steep)
                {
                    swap(ref x0, ref y0);
                    swap(ref x1, ref y1);
                }

                if (x0 > x1)
                {
                    swap(ref x0, ref x1);
                    swap(ref y0, ref y1);
                }

                double dx = x1 - x0;
                double dy = y1 - y0;
                double gradient = dy / dx;

                // handle first endpoint
                double xend = Math.Round(x0);
                double yend = y0 + gradient * (xend - x0);
                double xgap = rfpart(x0 + 0.5);
                double xpxl1 = xend; // this will be used in the main loop
                double ypxl1 = ipart(yend);

                if (steep)
                {
                    DrawingUtils.SetPixel(g, ypxl1, xpxl1, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(rfpart(yend) * xgap)), width, height);
                    DrawingUtils.SetPixel(g, ypxl1 + 1, xpxl1, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(fpart(yend) * xgap)), width, height);
                }
                else
                {
                    DrawingUtils.SetPixel(g, xpxl1, ypxl1, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(rfpart(yend) * xgap)), width, height);
                    DrawingUtils.SetPixel(g, xpxl1, ypxl1 + 1, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(fpart(yend) * xgap)), width, height);
                }

                // first y-intersection for the main loop
                double intery = yend + gradient;

                // handle second endpoint
                xend = Math.Round(x1);
                yend = y1 + gradient * (xend - x1);
                xgap = fpart(x1 + 0.5);
                double xpxl2 = xend; // this will be used in the main loop
                double ypxl2 = ipart(yend);

                if (steep)
                {
                    DrawingUtils.SetPixel(g, ypxl2, xpxl2, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(rfpart(yend) * xgap)), width, height);
                    DrawingUtils.SetPixel(g, ypxl2 + 1, xpxl2, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(fpart(yend) * xgap)), width, height);
                }
                else
                {
                    DrawingUtils.SetPixel(g, xpxl2, ypxl2, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(rfpart(yend) * xgap)), width, height);
                    DrawingUtils.SetPixel(g, xpxl2, ypxl2 + 1, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(fpart(yend) * xgap)), width, height);
                }

                if (steep)
                {
                    for (double x = xpxl1 + 1; x <= xpxl2 - 1; x++)
                    {
                        DrawingUtils.SetPixel(g, ipart(intery), x, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(rfpart(intery))), width, height);
                        DrawingUtils.SetPixel(g, ipart(intery) + 1, x, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(fpart(intery))), width, height);
                        intery = intery + gradient;
                    }
                }
                else
                {
                    for (double x = xpxl1 + 1; x <= xpxl2 - 1; x++)
                    {
                        DrawingUtils.SetPixel(g, x, ipart(intery), new Rgba32(color.R, color.G, color.B, Convert.ToSingle(rfpart(intery))), width, height);
                        DrawingUtils.SetPixel(g, x, ipart(intery) + 1, new Rgba32(color.R, color.G, color.B, Convert.ToSingle(fpart(intery))), width, height);
                        intery = intery + gradient;
                    }

                }

            }
        }
    }
}
