using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RANSAC.Geometry;

namespace RANSAC
{
    class RANSAC
    {
        public static Model Compute(IList<Point> data,
                             ModelFactory modelFac,
                             int minPoints,
                             int iterations, 
                             double threshold,
                             int minInliers)
        {
            Random random = new Random();
            Model bestModel = null;
            int bestInliers = -1;

            if (data.Count < minPoints)
                return null;

            for (int n = 0; n < iterations; n++)
            {
                HashSet<Point> points = new HashSet<Point>();

                for (int i = 0; i < minPoints;)
                {   
                    Point p = data[random.Next(data.Count)];

                    if (points.Contains(p))
                        continue;

                    points.Add(p);
                    i++;
                }

                Model m = modelFac.CreateModel();

                try
                {
                    m.Fit(points.ToArray());
                }
                catch
                {
                    continue;
                }

                m.SetInliers(data, threshold);
                int inliers = m.TotalInliers;

                if (inliers >= minInliers && inliers > bestInliers)
                {
                    bestModel = m;
                    bestInliers = inliers;
                }
            }

            return bestModel;
        }
    }
}
