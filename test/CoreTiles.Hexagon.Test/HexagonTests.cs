using Xunit;
using CoreTiles.Hexagon;
using System.Drawing;

namespace CoreTiles.Hexagon.Test
{
    public class HexagonTests
    {
        [Fact]
        public void ReturnValidListOfPointsForHexagon()
        {

            HexagonDefinition definition = new HexagonDefinition(10);

            var points = HexagonUtil.GetPoints(definition, new Point(0, 0));

            Assert.Equal(7, points.Count);

        }

    }
}