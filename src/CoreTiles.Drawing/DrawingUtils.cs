using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static CoreTiles.Drawing.LineDrawingExtensions;

namespace CoreTiles.Drawing
{
    public static class DrawingUtils
    {

        /// <summary>
        /// Draws a line between two points using Bresenham's algorithm with a supplied color and thickness
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        public static void SetImageBytes(Span<Rgba32> span, int x1, int y1, int x2, int y2, Rgba32 color, int thickness, int width, int height)
        {

            if (thickness == 1)
            {
                XiaolinWuAlgoriithm.DrawLine(span, x1, y1, x2, y2, color, width, height);
            }
            else
            {
                IList<Point> normalPoints = GetLineStartingPoints(x1, y1, x2, y2, thickness);
                for (var i = 0; i < normalPoints.Count(); i++)
                {
                    var point = normalPoints[i];

                    float targetX = x2 + (point.X - x1);
                    float targetY = y2 + (point.Y - y1);

                    //if these are the outer lines, use Xiaolin to anti-aliase them
                    if (i == 0 || i == normalPoints.Count() - 1)
                    {
                        //XiaolinWuAlgoriithm.DrawLine(span, point.X, point.Y, targetX, targetY, color, width, height);
                    }
                    BresenhamLineAlgorithm.DrawLine(span, point.X, point.Y, targetX, targetY, color, width, height);
                }
            }
        }

        /// <summary>
        /// Obtains the points of the normals of a line, up to a certain distance from the original line (and starting on x1,y1)
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public static IList<Point> GetLineStartingPoints(int x1, int y1, int x2, int y2, int thickness)
        {
            Vector2 unitVector = Vector2.Normalize(new Vector2(x2 - x1, y2 - y1));
            Vector2 normalVectorRight = new Vector2(-unitVector.Y, unitVector.X);
            Vector2 normalVectorLeft = new Vector2(unitVector.Y, -unitVector.X);

            float normalSize = (thickness - 1) / 2f;

            var normalPoints = new List<Point>();

            //Obtain the points of the normals
            Point startingPoint = BresenhamLineAlgorithm.GetNonDiagonalPointsOfLine(x1, y1, normalVectorRight, normalSize).Last();

            normalPoints.Add(startingPoint);

            foreach (Point point in BresenhamLineAlgorithm.GetNonDiagonalPointsOfLine(startingPoint.X, startingPoint.Y, normalVectorLeft, thickness - 1))
            {
                normalPoints.Add(point);
            }

            return normalPoints;
        }

        /// <summary>
        /// Alpha Blends two colors. Equation from: https://en.wikipedia.org/wiki/Alpha_compositing#Alpha_blending
        /// </summary>
        /// <param name="bg"></param>
        /// <param name="fg"></param>
        /// <returns></returns>
        public static Rgba32 AlphaBlend(Rgba32 bg, Rgba32 fg)
        {
            float srcA = fg.A / 255f;
            float srcR = fg.R / 255f;
            float srcG = fg.G / 255f;
            float srcB = fg.B / 255f;
            float dstA = bg.A / 255f;
            float dstR = bg.R / 255f;
            float dstG = bg.G / 255f;
            float dstB = bg.B / 255f;

            //If the destination is already fully opaque it can't be made more transparent
            float outA = dstA == 1 ? 1 : srcA + dstA * (1 - srcA);

            //Mix the RGB, taking into consideration the alpha
            float outR = (srcR * srcA + dstR * dstA * (1 - srcA)) / outA;
            float outG = (srcG * srcA + dstG * dstA * (1 - srcA)) / outA;
            float outB = (srcB * srcA + dstB * dstA * (1 - srcA)) / outA;

            return new Rgba32(outR, outG, outB, outA);
        }

        /// <summary>
        /// Sets a pixel(x,y) inside an image with a specified color
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public static void SetPixel(Span<Rgba32> image, int x, int y, Rgba32 color, int width, int height)
        {
            if (x < 0 || y < 0 || x >= width || y >= height) return;

            int index = y * height + x;

            //If there's some transparency on the supplied color, blend with the color below
            if (color.A < 255)
            {
                Rgba32 blendedColor = AlphaBlend(image[index], color);
                image[index] = blendedColor;
            }
            //Otherwise just replace it
            else
            {
                image[index] = color;
            }
        }

        public static void SetPixel(Span<Rgba32> image, double x, double y, Rgba32 color, int width, int height)
        {
            SetPixel(image, (int)Math.Round(x), (int)Math.Round(y), color, width, height);
        }
    }
}
