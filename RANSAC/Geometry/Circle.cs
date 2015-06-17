using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANSAC.Geometry
{
    class Circle
    {
        public Point Center { get; set; }
        public double Radius { get; set; }

        public double Area
        {
            get
            {
                return Math.PI * Math.Pow(this.Radius, 2);
            }
        }

        public Circle(Point center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public Circle(Triangle t)
            : this(t.P1, t.P2, t.P3)
        {

        }

        public Circle(Point p1, Point p2, Point p3)
        {
            if (Point.Collinear(p1, p2, p3))
            {
                if (p1 != p2 && p1 != p3 && p2 != p3)
                {
                    throw new ImpossibleCircleException("The points are different and collinear.");
                }
                else
                {
                    Point pa = p1;
                    Point pb = (p1 == p2 ? p3 : p2);

                    this.Center = (new Line(pa, pb)).MiddlePoint();
                    this.Radius = Point.Distance(this.Center, pa);
                }
            }
            else
            {
                if (p2.X == p1.X)
                {
                    Point temp = p3;
                    p3 = p2;
                    p2 = temp;
                }
                else
                {
                    if (p2.X == p3.X)
                    {
                        Point temp = p1;
                        p1 = p2;
                        p2 = temp;
                    }
                }

                Line l1 = new Line(p1, p2);
                Line l2 = new Line(p2, p3);

                double m1 = l1.Slope;
                double m2 = l2.Slope;

                double x = (m1 * m2 * (p1.Y - p3.Y) + m2 * (p1.X + p2.X) - m1 * (p2.X + p3.X)) / (2 * (m2 - m1));

                Line l = m1 != 0 ? l1 : l2;

                Point p = Line.GetYFromPoint(l.MiddlePoint(), -1 / l.Slope, x);

                this.Center = p;
                this.Radius = Point.Distance(p1, this.Center);
            }
        }

        public double Distance(Point p)
        {
            var d = Point.Distance(p, this.Center) - this.Radius;
            return (d > 0 ? d : 0);
        }

        public double DistanceToBorder(Point p)
        {
            return Math.Abs(Point.Distance(p, this.Center) - this.Radius);
        }

        public bool IsOnBorder(Point p)
        {
            return this.DistanceToBorder(p) == 0;
        }

        public bool Contains(Point p)
        {
            return Point.Distance(p, this.Center) <= this.Radius;
        }
    }

    class ImpossibleCircleException : ApplicationException
    {
        public ImpossibleCircleException(string message)
            : base(message)
        {

        }
    }
}
