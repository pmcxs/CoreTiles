using System;
using System.Collections.Generic;
using System.Drawing;

namespace CoreTiles.Hexagon
{
    public class HexagonUtil
    {
        public static List<Point> GetPoints(HexagonDefinition hexagonDefinition, Point center)
        {
            return new List<Point>
            {
                new Point((int) (center.X - hexagonDefinition.Diameter/2.0), center.Y),
                new Point((int) (center.X - hexagonDefinition.EdgeSize/2.0), (int) (center.Y - hexagonDefinition.Height/2.0)),
                new Point((int) (center.X + hexagonDefinition.EdgeSize/2.0), (int) (center.Y - hexagonDefinition.Height/2.0)),
                new Point((int) (center.X + hexagonDefinition.Diameter/2.0), center.Y),
                new Point((int) (center.X + hexagonDefinition.EdgeSize/2.0), (int) (center.Y + hexagonDefinition.Height/2.0)),
                new Point((int) (center.X - hexagonDefinition.EdgeSize/2.0), (int) (center.Y + hexagonDefinition.Height/2.0)),
                new Point((int) (center.X - hexagonDefinition.Diameter/2.0), center.Y)

            };
        }

        public static List<Point> GetPoints(int edgeSize, Point center)
        {
            return GetPoints(new HexagonDefinition(edgeSize), center);
        }

    }
}
