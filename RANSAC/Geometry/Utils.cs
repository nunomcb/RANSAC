using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RANSAC.Geometry
{
    class Utils
    {
        public static List<Point> ConvexHull(IList<Point> points)
        {
            Point startPoint = points[0];
            List<Point> hull = new List<Point>();

            foreach (Point p in points)
            {
                if (p.Y < startPoint.Y || (p.Y == startPoint.Y && p.X < startPoint.X))
                    startPoint = p;
            }

            Point curr = startPoint;

            for (; ; )
            {
                hull.Add(curr);

                Point maybePoint = points[(curr == points[0] ? 1 : 0)];

                foreach (Point p in points)
                {
                    double t = Triangle.SignedArea(curr, maybePoint, p);

                    if (t < 0)
                        maybePoint = p;
                }

                if (maybePoint == startPoint)
                    break;

                curr = maybePoint;
            }

            return hull;
        }
    }
}