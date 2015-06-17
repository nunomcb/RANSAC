using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANSAC.Geometry
{
    class Line : IEquatable<Line>
    {
        public Point P1 { get; set; }

        public Point P2 { get; set; }

        public double Slope
        {
            get
            {
                double d = P2.X - P1.X;

                if (d == 0)
                    throw new LineIsVerticalException("The line passing through points " + P1.ToString() + " and " + P2.ToString() + " is vertical.");

                return (P2.Y - P1.Y) / (P2.X - P1.X);
            }
        }

        public double Intercept
        {
            get
            {
                try
                {
                    return P1.Y - P1.X * this.Slope;
                }
                catch (LineIsVerticalException e)
                {
                    throw e;
                }
            }
        }

        public Line(Point p1, Point p2)
        {
            this.P1 = p1;
            this.P2 = p2;
        }

        public static Point GetYFromPoint(Point p, double slope, double x)
        {
            double y = (x - p.X) * slope + p.Y;

            return new Point(x, y);
        }

        public static Point GetXFromPoint(Point p, double slope, double y)
        {
            if (slope == 0)
                return new Point(y, y);

            double x = (y - p.Y) / slope + p.X;

            return new Point(x, y);
        }

        public static Point Intersection(Line l1, Line l2)
        {
            double x1 = l1.P1.X,
                   y1 = l1.P1.Y,
                   x2 = l1.P2.X,
                   y2 = l1.P2.Y,
                   x3 = l2.P1.X,
                   y3 = l2.P1.Y,
                   x4 = l2.P2.X,
                   y4 = l2.P2.Y;

            double denom = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (denom == 0)
                // Parallel... I'll take care of this exception later
                throw new Exception();

            double num1 = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            double num2 = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);

            return new Point(num1 / denom, num2 / denom);
        }

        public bool InSegment(Point p)
        {
            bool b = ((p.X >= this.P1.X && p.X <= this.P2.X) || (p.X <= this.P1.X && p.X >= this.P2.X));

            return b;
        }

        public double FindY(double x)
        {
            try
            {
                return x * this.Slope + this.Intercept;
            }
            catch (LineIsVerticalException e)
            {
                throw e;
            }

        }

        public double FindX(double y)
        {
            return (y - this.Intercept) / this.Slope;
        }

        public Point MiddlePoint()
        {
            return new Point((this.P1.X + this.P2.X) / 2, (this.P1.Y + this.P2.Y) / 2);
        }

        public override int GetHashCode()
        {
            unchecked // integer overflows are accepted here
            {
                int hashCode = 0;
                Point a, b;

                if (this.P1.X > this.P2.X)
                {
                    a = this.P1;
                    b = this.P2;
                }
                else
                {
                    if (this.P1.X == this.P2.X)
                    {
                        if (this.P1.Y > this.P2.Y)
                        {
                            a = this.P1;
                            b = this.P2;
                        }
                        else
                        {
                            a = this.P2;
                            b = this.P1;
                        }
                    }
                    else
                    {
                        a = this.P2;
                        b = this.P1;
                    }
                }

                hashCode = (hashCode * 397) ^ a.GetHashCode();
                hashCode = (hashCode * 397) ^ b.GetHashCode();
                return hashCode;
            }
        }

        public bool Equals(Line l)
        {
            return (this.P1.Equals(l.P1) && this.P2.Equals(l.P2)) || (this.P1.Equals(l.P2) && this.P2.Equals(l.P1));
        }
    }

    class LineIsVerticalException : ApplicationException
    {
        public LineIsVerticalException(string message)
            : base(message)
        {

        }
    }
}
