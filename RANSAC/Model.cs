using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RANSAC.Geometry;

namespace RANSAC
{
    abstract class Model
    {
        public HashSet<Point> Points { get; protected set; }
        public List<Point> Inliers { get; protected set; }
        public int TotalInliers
        {
            get
            {
                return Inliers.Count;
            }
        }

        public abstract void Fit(IList<Point> points);
        public abstract double Error(Point p);
        public abstract void SetInliers(IList<Point> points, double threshold);
    }

    class CircleModel : Model
    {
        public Circle circle { get; private set; }

        public CircleModel()
        {
            this.Points = new HashSet<Point>();
            this.Inliers = new List<Point>();
            circle = null;
        }

        public CircleModel(Point p1, Point p2, Point p3)
        {
            this.Points = new HashSet<Point>();
            this.Points.Add(p1);
            this.Points.Add(p2);
            this.Points.Add(p3);
            this.circle = new Circle(p1, p2, p3);
        }

        public CircleModel(Point[] points)
        {
            this.Fit(points);
        }

        public override void Fit(IList<Point> points)
        {
            if (points.Count < 3)
                return; // This should be an exception

            foreach (Point p in points)
                this.Points.Add(p);

            try
            {
                this.circle = new Circle(points[0], points[1], points[2]);
            }
            catch
            {
                throw new ImpossibleModelException("Can't create a model that fits the given data!");
            }
        }

        public override void SetInliers(IList<Point> points, double threshold)
        {
            foreach (Point p in points)
            {
                if (this.Error(p) <= threshold)
                {
                    this.Inliers.Add(p);
                }
            }
        }

        public void Print()
        {
            foreach (Point p in this.Points)
                Console.WriteLine(p);

            Console.WriteLine();
            Console.WriteLine(this.circle.Center);
            Console.WriteLine(this.circle.Radius);
        }

        public override double Error(Point p)
        {
            return this.circle.DistanceToBorder(p);
        }

    }

    interface ModelFactory
    {
        Model CreateModel();
    }

    class CircleModelFactory : ModelFactory
    {
        public Model CreateModel()
        {
            return new CircleModel();
        }
    }

    class ImpossibleModelException : ApplicationException
    {
        public ImpossibleModelException(string msg) : base(msg)
        {

        }
    }
}
