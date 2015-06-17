using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANSAC.Geometry
{
    class Triangle
    {
        public Point P1, P2, P3;
        public Point[] Vertices
        {
            get
            {
                return new Point[] { P1, P2, P3 };
            }
        }

        public static List<Triangle> DelaunayTriangulation(IList<Point> points)
        {
            if (points.Count == 3)
            {
                return new List<Triangle> { new Triangle(points[0], points[1], points[2]) };
            }
            var triangles = new List<Triangle>();

            Triangle superTriangle = ComputeSuperTriangle(points);

            triangles.Add(superTriangle);

            points.Add(superTriangle.P1);
            points.Add(superTriangle.P2);
            points.Add(superTriangle.P3);

            foreach (Point p in points)
            {
                var edges = new List<Line>();

                for (int i = triangles.Count - 1; i >= 0; i--)
                {
                    Triangle t = triangles[i];
                    var c = new Circle(t);

                    if (c.Contains(p))
                    {
                        edges.Add(new Line(t.P1, t.P2));
                        edges.Add(new Line(t.P2, t.P3));
                        edges.Add(new Line(t.P3, t.P1));
                        triangles.RemoveAt(i);
                    }
                }

                for (int i = edges.Count - 2; i >= 0; i--)
                {
                    for (int j = edges.Count - 1; j > i; j--)
                    {
                        if (edges[i].Equals(edges[j]))
                        {
                            edges.RemoveAt(j);
                            edges.RemoveAt(i);
                            break;
                        }
                    }
                }

                foreach (Line l in edges)
                {
                    triangles.Add(new Triangle(l.P1, l.P2, p));
                }
            }

            for (int i = triangles.Count - 1; i >= 0; i--)
            {
                Triangle t = triangles[i];

                foreach (Point p in superTriangle.Vertices)
                {
                    if (t.P1.Equals(p) || t.P2.Equals(p) || t.P3.Equals(p))
                    {
                        triangles.RemoveAt(i);
                        break;
                    }
                }
            }

            points.RemoveAt(points.Count - 1);
            points.RemoveAt(points.Count - 1);
            points.RemoveAt(points.Count - 1);

            return triangles;
        }

        public static Triangle ComputeSuperTriangle(IList<Point> points)
        {
            double xmin = points[0].X;
            double xmax = xmin;
            double ymin = points[0].Y;
            double ymax = ymin;

            foreach (Point p in points)
            {
                if (p.X < xmin) xmin = p.X;
                if (p.X > xmax) xmax = p.X;
                if (p.Y < ymin) ymin = p.Y;
                if (p.Y > ymax) ymax = p.Y;
            }

            double dx = xmax - xmin;
            double dy = ymax - ymin;
            double dmax = (dx > dy) ? dx : dy;

            double xmid = (xmax + xmin) * 0.5;
            double ymid = (ymax + ymin) * 0.5;

            Point p1 = new Point((xmid - 2 * dmax), (ymid - dmax));
            Point p2 = new Point(xmid, (ymid + 2 * dmax));
            Point p3 = new Point((xmid + 2 * dmax), (ymid - dmax));

            return new Triangle(p1, p2, p3);
        }

        public Triangle(Point p1, Point p2, Point p3)
        {
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;
        }

        public static double SignedArea(Point p1, Point p2, Point p3)
        {
            return ((p1.X - p3.X) * (p2.Y - p1.Y) - (p1.X - p2.X) * (p3.Y - p1.Y)) / 2;
        }
    }
}
