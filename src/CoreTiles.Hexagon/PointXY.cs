﻿using System;

namespace CoreTiles.Hexagon
{
    public delegate (double, double) CoordinateToPointXY(double latitude, double longitude);

    [Serializable]
    public class PointXY
    {
        public PointXY()
        {
        }

        public PointXY(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        private bool Equals(PointXY other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return X.GetHashCode() * 397 ^ Y.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format("X:" + X + "  Y:" + Y);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PointXY)obj);
        }
    }
}