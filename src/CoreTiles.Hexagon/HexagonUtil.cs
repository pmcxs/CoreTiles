using System;
using System.Collections.Generic;
using System.Drawing;

namespace CoreTiles.Hexagon
{
    public class HexagonUtil
    {
        public static IEnumerable<PointF> GetHexagonPixels(int edgeSize, PointF center)
        {
            for (var i = 0; i < 6; i++)
            {
                float angle_deg = 60f * i;
                float angle_rad = (float) Math.PI / 180f * angle_deg;
                yield return new PointF(center.X + edgeSize * (float) Math.Cos(angle_rad),
                                        center.Y + edgeSize * (float) Math.Sin(angle_rad));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="hexagonDefinition"></param>
        /// <returns></returns>
        public static PointF GetCenterPixelOfHexagonCoordinate(HexagonCoordinate coordinate, int edgeSize)
        {
            float x = edgeSize * 3f / 2f * coordinate.U;
            float y = edgeSize * (float) Math.Sqrt(3.0) * (coordinate.V + coordinate.U / 2f);
            return new PointF(x, y);
        }

        public static HexagonCoordinate GetHexagonCoordinateOfPixel(Point point, int edgeSize)
        {
            double u = point.X * 2.0 / 3.0 / edgeSize;
            double v = (-point.X / 3.0 + Math.Sqrt(3.0) / 3.0 * point.Y) / edgeSize;
            return new HexagonCoordinate(u, v);
        }

        public static IEnumerable<HexagonCoordinate> GetHexagonsInsideBoudingBox(Point topLeftCorner, Point bottomRightCorner, int edgeSize)
        {

            var topLeftUV = GetHexagonCoordinateOfPixel(topLeftCorner, edgeSize);

            var bottomLeftUV = GetHexagonCoordinateOfPixel(new Point(topLeftCorner.X, bottomRightCorner.Y), edgeSize);

            var bottomRightUV = GetHexagonCoordinateOfPixel(bottomRightCorner, edgeSize);

            var rowsEven = bottomLeftUV.V - topLeftUV.V + 1;

            var topLeftUVCenter = GetCenterPixelOfHexagonCoordinate(topLeftUV, edgeSize);

            int minX2 = (int) Math.Round(topLeftUVCenter.X + (edgeSize * 1.5));

            var topLeftUV2 = GetHexagonCoordinateOfPixel(new Point(minX2, topLeftCorner.Y), edgeSize);
            var bottomLeftUV2 = GetHexagonCoordinateOfPixel(new Point(minX2, bottomRightCorner.Y), edgeSize);
            var rowsOdd = bottomLeftUV2.V - topLeftUV2.V + 1;

            var columns = bottomRightUV.U - topLeftUV.U + 1;
            
            for (var i = 0; i < columns; i++)
            {
                var rows = (i % 2) == 0 ? rowsEven : rowsOdd;

                for (var j = 0; j < rows; j++)
                {
                    int offset = topLeftUV.V == topLeftUV2.V ? Convert.ToInt32(Math.Floor((double)i / 2)) : Convert.ToInt32(Math.Ceiling((double)i / 2));

                    int u = topLeftUV.U + i;
                    int v = topLeftUV.V + j - offset;
                    yield return new HexagonCoordinate(u, v);
                }
            }
            



        }
    }

}
