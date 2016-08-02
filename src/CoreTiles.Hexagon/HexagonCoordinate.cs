using System;

namespace CoreTiles.Hexagon
{
    public class HexagonCoordinate
    {

        public HexagonCoordinate(int u, int v)
        {
            U = u;
            V = v;
        }

        public HexagonCoordinate(double u, double v)
        {
            var x = u;
            var z = v;
            var y = -x - z;

            var rx = Convert.ToInt32(Math.Round(x));
            var ry = Convert.ToInt32(Math.Round(y));
            var rz = Convert.ToInt32(Math.Round(z));

            int x_diff = Convert.ToInt32(Math.Abs(rx - x));
            int y_diff = Convert.ToInt32(Math.Abs(ry - y));
            int z_diff = Convert.ToInt32(Math.Abs(rz - z));

            if (x_diff > y_diff && x_diff > z_diff)
                rx = -ry - rz;
            else if (y_diff > z_diff)
                ry = -rx - rz;
            else
                rz = -rx - ry;

            U = rx;
            V = rz;
        }




        public int U { get; set; }

        public int V { get; set; }


        public override bool Equals(object obj)
        {
            HexagonCoordinate coordinate = obj as HexagonCoordinate;
            if (coordinate == null)
            {
                return false;
            }

            return U == coordinate.U && V == coordinate.V;
        }
    }
}