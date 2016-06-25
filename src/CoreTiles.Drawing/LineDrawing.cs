using System;
using System.Collections.Generic;
using System.Numerics;
using ImageProcessorCore;

namespace CoreTiles.Drawing
{
    public class LineDrawing : ILineDrawing
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
        public void DrawLine(Image image, int x1, int y1, int x2, int y2, Color color, int thickness)
        {
            if(thickness == 1)
            {
                XiaolinWuAlgoriithm.DrawLine(image, x1, y1, x2, y2, color);
            }
            else
            {
                BresenhamLineAlgorithm.DrawLine(image, x1, y1, x2, y2, color);
            
                var normals = GetNormals(x1, y1, x2, y2, thickness);

                foreach(IList<Point> normal in normals)
                {
                    foreach(Point point in normal)
                    {
                        if(normal.IndexOf(point) == normal.Count - 1)
                        {
                            XiaolinWuAlgoriithm.DrawLine(image, point.X, point.Y, x2 + (point.X - x1), y2 + (point.Y - y1), color);
                        }
                        else
                        {
                            BresenhamLineAlgorithm.DrawLine(image, point.X, point.Y, x2 + (point.X - x1), y2 + (point.Y - y1), color);
                        }
                    }
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
        private IEnumerable<IList<Point>> GetNormals(int x1, int y1, int x2, int y2, int thickness)
        {
            Vector2 unitVector = Vector2.Normalize(new Vector2(x2 - x1, y2 - y1));
            Vector2 normalVectorRight = new Vector2(-unitVector.Y, unitVector.X);
            Vector2 normalVectorLeft = new Vector2(unitVector.Y, -unitVector.X);

            //Extend in both directions (normals of the main vector)
            int rightExtraThickness = (int)Math.Ceiling((thickness - 1) / 2.0);
            int leftExtraThickess = (int)Math.Floor((thickness - 1) / 2.0);
        
            IList<Point> normalPointsRight = new List<Point>();
            IList<Point> normalPointsLeft = new List<Point>();

            //Obtain the points of the normals
            foreach (Point point in BresenhamLineAlgorithm.GetNonDiagonalPointsOfLine(x1, y1, (int)(x1 + normalVectorLeft.X * thickness), (int)(y1 + normalVectorLeft.Y * thickness), rightExtraThickness))
            {
                normalPointsLeft.Add(point);
            }

            yield return normalPointsLeft;

            foreach (Point point in BresenhamLineAlgorithm.GetNonDiagonalPointsOfLine(x1, y1, (int)(x1 + normalVectorRight.X * thickness), (int)(y1 + normalVectorRight.Y * thickness), leftExtraThickess))
            {
                normalPointsRight.Add(point);
            }

            yield return normalPointsRight;
        }
        
    }
}
