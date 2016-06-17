using System;
using System.Collections.Generic;
using System.Numerics;
using ImageProcessorCore;

namespace CoreTiles.Drawing
{
    public static class BresenhamLineAlgorithm
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
        public static void DrawLine(Image image, int x1, int y1, int x2, int y2, Color color, int thickness)
        {
            DrawLine(image, x1, y1, x2, y2, color);

            if (thickness > 1)
            {
                foreach (Point point in GetNormals(x1, y1, x2, y2, thickness))
                {
                    DrawLine(image, point.X, point.Y, x2 + (point.X - x1), y2 + (point.Y - y1), color);
                }
            }
        }

        /// <summary>
        /// Obtains the points of both normals of a line, up to a certain distance from the original line 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        private static IEnumerable<Point> GetNormals(int x1, int y1, int x2, int y2, int thickness)
        {
            Vector2 unitVector = Vector2.Normalize(new Vector2(x2 - x1, y2 - y1));
            Vector2 normalVectorRight = new Vector2(-unitVector.Y, unitVector.X);
            Vector2 normalVectorLeft = new Vector2(unitVector.Y, -unitVector.X);

            //Extend in both directions (normals of the main vector)
            int rightExtraThickness = (int)Math.Ceiling((thickness - 1) / 2.0);
            int leftExtraThickess = (int)Math.Floor((thickness - 1) / 2.0);

            //Obtain the points of the normals
            foreach (Point point in GetNonDiagonalPointsOfLine(x1, y1, (int)(x1 + normalVectorLeft.X * thickness), (int)(y1 + normalVectorLeft.Y * thickness), rightExtraThickness))
            {
                yield return point;
            }

            foreach (Point point in GetNonDiagonalPointsOfLine(x1, y1, (int)(x1 + normalVectorRight.X * thickness), (int)(y1 + normalVectorRight.Y * thickness), leftExtraThickess))
            {
                yield return point;
            }
        }

        /// <summary>
        /// Draws a line between two points using Bresenham's algorithm with a thickness of 1 pixel
        /// </summary>
        /// <remarks>Algorithm from http://tech-algorithm.com/articles/drawing-line-using-bresenham-algorithm/</remarks>
        /// <param name="image"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        private static void DrawLine(Image image, int x1, int y1, int x2, int y2, Color color)
        {
            int w = x2 - x1;
            int h = y2 - y1;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                image.SetPixel(x1, y1, color);

                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x1 += dx1;
                    y1 += dy1;
                }
                else
                {
                    x1 += dx2;
                    y1 += dy2;
                }
            }
        }

        /// <summary>
        /// Using a variant of Bresenham algorithm obtains the various points of a line (without the first one) without any diagonal movement
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        private static IEnumerable<Point> GetNonDiagonalPointsOfLine(int x0, int y0, int x1, int y1, int maxDistance)
        {
            int xDist = Math.Abs(x1 - x0);
            int yDist = -Math.Abs(y1 - y0);
            int xStep = (x0 < x1 ? +1 : -1);
            int yStep = (y0 < y1 ? +1 : -1);
            int error = xDist + yDist;

            int i = 0;

            while ((x0 != x1 || y0 != y1) && (i < maxDistance))
            {
                int e2 = error << 1;

                if (e2 - yDist > xDist - e2)
                {
                    // horizontal step
                    error += yDist;
                    x0 += xStep;
                }
                else
                {
                    // vertical step
                    error += xDist;
                    y0 += yStep;
                }

                yield return new Point(x0, y0);
                i++;
            }
        }
    }
}
