using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANSAC.Geometry
{
    class Annulus
    {
        public Point Center;
        public double InnerRadius;
        public double OuterRadius;

        public double Width
        {
            get
            {
                return OuterRadius - InnerRadius;
            }
        }

        public Annulus(Point center, double ir, double or)
        {
            this.Center = center;
            this.InnerRadius = ir;
            this.OuterRadius = or;
        }

        public static Annulus calculateAnnulus(IList<Point> points, Point center)
        {
            double minDist = Int32.MaxValue;
            double maxDist = 0;

            foreach (Point p in points)
            {
                double dist = Point.Distance(p, center);

                if (dist > maxDist)
                    maxDist = dist;

                if (dist < minDist)
                    minDist = dist;
            }

            return new Annulus(center, minDist, maxDist);
        }

        public static Annulus SmallestWidthAnnulus(IList<Point> points, int limit)
        {
            var fpvd = new FurthestVoronoi(Utils.ConvexHull(points), limit);
            var cpvd = new ClosestVoronoi(points, limit);
            var intersections = new List<Point>();

            foreach (Line l1 in fpvd.Edges)
            {
                foreach (Line l2 in cpvd.Edges)
                {
                    try
                    {
                        Point p = Line.Intersection(l1, l2);

                        if (l1.InSegment(p) && l2.InSegment(p))
                            intersections.Add(p);
                    }
                    catch
                    {
                    }
                }
            }

            Annulus bestAnnulus = null;
            double bestWidth = Int32.MaxValue;

            foreach (Point p in intersections)
            {
                Annulus annulus = calculateAnnulus(points, p);
                double width = annulus.Width;

                if (width < bestWidth)
                {
                    bestWidth = width;
                    bestAnnulus = annulus;
                }
            }

            foreach (Point p in fpvd.Vertices)
            {
                Annulus annulus = calculateAnnulus(points, p);
                double width = annulus.Width;

                if (width < bestWidth)
                {
                    bestWidth = width;
                    bestAnnulus = annulus;
                }
            }

            foreach (Point p in cpvd.Vertices)
            {
                Annulus annulus = calculateAnnulus(points, p);
                double width = annulus.Width;

                if (width < bestWidth)
                {
                    bestWidth = width;
                    bestAnnulus = annulus;
                }
            }

            return bestAnnulus;
        }
    }
}
