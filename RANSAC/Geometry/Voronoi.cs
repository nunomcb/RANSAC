using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANSAC.Geometry
{
    abstract class Voronoi
    {
        public List<Point> Vertices { get; protected set; }
        public List<Line> Edges { get; protected set; }
        public double Border { get; protected set; }

        public abstract void Compute(IList<Point> points);
    }

    class FurthestVoronoi : Voronoi
    {
        public FurthestVoronoi(IList<Point> points, double border)
        {
            this.Vertices = new List<Point>();
            this.Edges = new List<Line>();
            this.Border = border;
            this.Compute(points);
        }

        public override void Compute(IList<Point> data)
        {
            List<Point> hull = new List<Point>();
            List<Circle> circles = new List<Circle>();
            List<Point> bisectors = new List<Point>();

            double bestRad = 0;
            int bestRadIndex = 0;
            int n = data.Count;

            foreach (Point p in data)
            {
                hull.Add(new Point(p));
            }

            for (int i = 0; i < hull.Count; i++)
            {
                circles.Add(new Circle(hull[i], hull[(i + 1) % hull.Count], hull[(i + 2) % hull.Count]));

                int p1 = i;
                int p2 = (i + 1) % hull.Count;

                Line l = new Line(hull[p1], hull[p2]);
                Point m = l.MiddlePoint();
                Point m2 = new Point(m);

                m2.X++;

                if (Triangle.SignedArea(hull[p1], hull[p2], m2) > 0) 
                {
                    bisectors.Add(Line.GetYFromPoint(m, -1 / l.Slope, this.Border));
                }  
                else
                {
                    bisectors.Add(Line.GetYFromPoint(m, -1 / l.Slope, -this.Border));
                }
                    
            }

            while (n > 2)
            {
                bestRad = -1;

                for (int i = 0; i < circles.Count; i++)
                {
                    if (circles[i].Radius > bestRad)
                    {
                        bestRad = circles[i].Radius;
                        bestRadIndex = i;
                    }
                }

                int p1 = bestRadIndex;
                int p2 = (p1 + 1) % hull.Count;
                int p3 = (p1 + 2) % hull.Count;
                Point center = circles[p1].Center;

                this.Vertices.Add(center);
                
                Line l1 = new Line(bisectors[p1], center);
                Line l2 = new Line(bisectors[p2], center);

                this.Edges.Add(l1);
                this.Edges.Add(l2);

                bisectors[p1] = new Point(center);

                hull.RemoveAt(p2);
                circles.RemoveAt(p2);
                bisectors.RemoveAt(p2);

                if (p1 > p2)
                    p1--;

                if (p3 > p2)
                    p3--;

                if (n == 3)
                {
                    this.Edges.Add(new Line(bisectors[p3], bisectors[p1]));
                }

                p1 = (p2 - 1 + hull.Count) % hull.Count;
                p2 = (p2 - 2 + hull.Count) % hull.Count;

                circles[p1] = new Circle(hull[p1], hull[(p1 + 1) % hull.Count], hull[(p1 + 2) % hull.Count]);
                circles[p2] = new Circle(hull[p2], hull[(p2 + 1) % hull.Count], hull[(p2 + 2) % hull.Count]);

                n--;
            }

            
        }
    }

    class ClosestVoronoi : Voronoi
    {
        public ClosestVoronoi(IList<Point> points, double border)
        {
            this.Vertices = new List<Point>();
            this.Edges = new List<Line>();
            this.Border = border;
            this.Compute(points);
        }
        public override void Compute(IList<Point> points)
        {
            IList<Triangle> triangles = Triangle.DelaunayTriangulation(points);
            var dictionary = new Dictionary<Line, List<Tuple<Point, bool>>>();

            foreach (Triangle t in triangles)
            {
                var combs = new Point[3, 3];
                var c = new Circle(t);

                Point pa = t.P1;
                Point pb = t.P2;
                Point pc = t.P3;
                Point pt;

                if (pa.CompareTo(pb) > 0) { pt = pa; pa = pb; pb = pt; }
                if (pb.CompareTo(pc) > 0) { pt = pb; pb = pc; pc = pt; }
                if (pa.CompareTo(pb) > 0) { pt = pa; pa = pb; pb = pt; }

                combs[0, 0] = combs[1, 0] = combs[2, 2] = pa;
                combs[0, 1] = combs[1, 2] = combs[2, 0] = pb;
                combs[0, 2] = combs[1, 1] = combs[2, 1] = pc;

                for (int i = 0; i < 3; i++)
                {
                    Line l = new Line(combs[i, 0], combs[i, 1]);

                    if (!dictionary.ContainsKey(l))
                    {
                        dictionary.Add(l, new List<Tuple<Point, bool>>());
                    }

                    bool b = ((Triangle.SignedArea(combs[i, 0], combs[i, 1], combs[i, 2]) > 0) && l.Slope > 0) || ((Triangle.SignedArea(combs[i, 0], combs[i, 1], combs[i, 2]) < 0) && l.Slope < 0);
                    var tuple = new Tuple<Point, bool>(c.Center, b);
                    dictionary[l].Add(tuple);
                    this.Vertices.Add(c.Center);
                }
            }

            foreach (Line line in dictionary.Keys)
            {
                List<Tuple<Point, bool>> l = dictionary[line];

                if (l.Count == 1)
                    if (l[0].Item2)
                    {
                        this.Edges.Add(new Line(l[0].Item1, Line.GetYFromPoint(l[0].Item1, -1 / line.Slope, this.Border)));
                    }
                    else
                    {
                        this.Edges.Add(new Line(l[0].Item1, Line.GetYFromPoint(l[0].Item1, -1 / line.Slope, -this.Border)));
                    }
                else
                    for (int i = 0; i < l.Count; i++)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            this.Edges.Add(new Line(l[i].Item1, l[j].Item1));
                        }
                    }
            }
        }
    }
}
