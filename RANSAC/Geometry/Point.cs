using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANSAC.Geometry
{
    class Point : IEquatable<Point>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }

        public static double Angle(Point p1, Point p2, Point p3)
        {
            return Math.Atan2(p1.Y - p2.Y, p1.X - p2.X) - Math.Atan2(p3.Y - p2.Y, p3.X - p2.X);
        }

        public static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static bool Collinear(Point p1, Point p2, Point p3)
        {
            return (p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y) == 0);
        }

        public override string ToString()
        {
            return "( " + this.X + " " + this.Y + " )";
        }

        public override int GetHashCode()
        {
            unchecked // integer overflows are accepted here
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ this.X.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Y.GetHashCode();
                return hashCode;
            }
        }

        public bool Equals(Point p)
        {
            return (this.X == p.X && this.Y == p.Y);
        }

        public int CompareTo(Point p)
        {
            if (this.X > p.X)
                return 1;
            else if (this.X == p.X)
                if (this.Y > p.Y)
                    return 1;
                else if (this.Y < p.Y)
                    return -1;
                else
                    return 0;
            else
                return -1;
        }
    }
}
