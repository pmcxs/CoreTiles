using Xunit;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CoreTiles.Hexagon.Test
{
    public class HexagonTests
    {

        /// <summary>
        /// 
        ///                   +++++++++               +++++++++
        ///                  + U:1,V:-1+             + U:3,V:-2+
        ///                 +           +           +           +
        ///                +             +         +             +
        ///       +++++++++               +++++++++               +++++++++
        ///      + U:0,V:0 +             + U:2,V:-1+             + U:4,V:-2
        ///     +     x-----------X--------------x--------x     +           +
        ///    +      |B     +    |C   +         |   +    |    +             +
        ///   +       |       ++++|++++          |    ++++|++++               +
        ///    +      |      + U:1|V:0 +         |   + U:3|V:-1+             +
        ///     +     |-----------x==============x  +     |     +           +
        ///      +    |    +       1     +         +      |      +         +
        ///       ++++|++++               +++++++++       |       +++++++++
        ///      + U:0|V:1 +             + U:2,v:0 +     A|      + U:4,v:-1+
        ///     +     x-----------------------------------x     +           +
        ///    +             +         +             +         +             +
        ///   +               +++++++++               +++++++++               +
        /// </summary>
        [Fact]
        public void CheckHexagonLocation()
        {
            IList<HexagonCoordinate> resultsA = HexagonUtil.GetHexagonsInsideBoudingBox(new Point(0, -20), new Point(450, 100), 100).ToList();

            Assert.Equal(8, resultsA.Count);
            Assert.Equal(resultsA[0], new HexagonCoordinate(0, 0));
            Assert.Equal(resultsA[1], new HexagonCoordinate(0, 1));
            Assert.Equal(resultsA[2], new HexagonCoordinate(1, -1));
            Assert.Equal(resultsA[3], new HexagonCoordinate(1, 0));
            Assert.Equal(resultsA[4], new HexagonCoordinate(2, -1));
            Assert.Equal(resultsA[5], new HexagonCoordinate(2, 0));
            Assert.Equal(resultsA[6], new HexagonCoordinate(3, -2));
            Assert.Equal(resultsA[7], new HexagonCoordinate(3, -1));

            IList<HexagonCoordinate> resultsB = HexagonUtil.GetHexagonsInsideBoudingBox(new Point(0, -20), new Point(320, 20), 100).ToList();

            Assert.Equal(4, resultsB.Count);
            Assert.Equal(resultsB[0], new HexagonCoordinate(0, 0));
            Assert.Equal(resultsB[1], new HexagonCoordinate(1, -1));
            Assert.Equal(resultsB[2], new HexagonCoordinate(1, 0));
            Assert.Equal(resultsB[3], new HexagonCoordinate(2, -1));

            IList<HexagonCoordinate> resultsC = HexagonUtil.GetHexagonsInsideBoudingBox(new Point(150, -20), new Point(320, 20), 100).ToList();

            Assert.Equal(3, resultsC.Count);
            Assert.Equal(resultsC[0], new HexagonCoordinate(1, -1));
            Assert.Equal(resultsC[1], new HexagonCoordinate(1, 0));
            Assert.Equal(resultsC[2], new HexagonCoordinate(2, -1));

        }

        /// <summary>
        ///                      50
        ///                    |---|
        ///                    *********
        ///                   * U:1,V:-1*
        ///              50  *           *
        ///            |---|*  (150,-87)  *
        ///        *********       +       *
        ///       * U:0,V:0 *             *
        ///      *           *           *
        ///     *    (0,0)    *         *
        ///    *       +       *********
        ///     *             * U:1,V:0 *
        ///      *           *           *
        ///       *         *  (150,87)   *
        ///        *********       +       *
        ///                 *             *
        ///                  *           *
        ///                   *         *
        ///                    *********
        /// 
        ///  Edge size: 100
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, -1, 150, -87)]
        [InlineData(1, 0, 150, 87)]
        public void GetCenterPointXYOfHexagon(int u, int v, double x, double y)
        {
            PointF center = HexagonUtil.GetCenterPixelOfHexagonCoordinate(new HexagonCoordinate(u, v), 100);

            Assert.Equal(x, center.X);
            Assert.Equal(y, center.Y);

        }

    }
}