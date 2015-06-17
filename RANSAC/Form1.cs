using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RANSAC.Geometry;
using Point = RANSAC.Geometry.Point;
using GeometryUtils = RANSAC.Geometry.Utils;

namespace RANSAC
{
    public partial class Form1 : Form
    {
        private HashSet<Point> points;
        private List<CircleModel> models;

        public Form1()
        {
            this.models = new List<CircleModel>();
            InitializeComponent();
            points = new HashSet<Point>();
            //this.pictureBox1.BackgroundImage = new Bitmap("C:\\Users\\Nuno\\Desktop\\image1.jpg");
        }

        private static void DrawPoint(Graphics g, Brush b, Point p)
        {
            g.FillRectangle(b, (float)p.X, (float)p.Y, 5, 5);
        }

        private static void DrawHull(Graphics g, Pen p, List<Point> points)
        {
            int size = points.Count;

            g.DrawLine(p, (float)points[size - 1].X, (float)points[size - 1].Y, (float)points[0].X, (float)points[0].Y);

            for (int i = 0; i < size - 1; i++)
            {
                g.DrawLine(p, (float)points[i].X, (float)points[i].Y, (float)points[i + 1].X, (float)points[i + 1].Y);
            }
        }
        
        private static void DrawTriangle(Graphics g, Pen p, Triangle t)
        {
            g.DrawLine(p, (float)t.P1.X, (float)t.P1.Y, (float)t.P2.X, (float)t.P2.Y);
            g.DrawLine(p, (float)t.P2.X, (float)t.P2.Y, (float)t.P3.X, (float)t.P3.Y);
            g.DrawLine(p, (float)t.P3.X, (float)t.P3.Y, (float)t.P1.X, (float)t.P1.Y);
        }

        private static void DrawDelaunay(Graphics g, Pen p, IList<Triangle> l)
        {
            foreach (Triangle t in l)
            {
                DrawTriangle(g, p, t);
            }
        }

        private static void DrawCircle(Graphics g, Pen p, Circle c)
        {
            g.DrawEllipse(p, (float)(c.Center.X - c.Radius), (float)(c.Center.Y - c.Radius), (float)(2 * c.Radius), (float)(2 * c.Radius));
        }

        private static void DrawVoronoi(Graphics g, Pen p, Voronoi v)
        {
            foreach (Line l in v.Edges)
            {
                g.DrawLine(p, (float)l.P1.X, (float)l.P1.Y, (float)l.P2.X, (float)l.P2.Y);
            }
        }

        private static void DrawAnnulus(Graphics g, Brush b, Annulus a)
        {
            GraphicsPath p1 = new GraphicsPath();
            GraphicsPath p2 = new GraphicsPath();

            p1.AddEllipse((float)(a.Center.X - a.InnerRadius), (float)(a.Center.Y - a.InnerRadius), (float)(2 * a.InnerRadius), (float)(2 * a.InnerRadius));
            p2.AddEllipse((float)(a.Center.X - a.OuterRadius), (float)(a.Center.Y - a.OuterRadius), (float)(2 * a.OuterRadius), (float)(2 * a.OuterRadius));

            Region r = new Region(p2);

            r.Exclude(p1);

            g.FillRegion(b, r);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                Point p = new Point(e.X, e.Y);
                points.Add(p);

                DrawPoint(g, new SolidBrush(Color.Black), p);

                Console.WriteLine(e.X + " " + e.Y);

                if (points.Count >= 3)
                {
                    btnDetect.Enabled = true;
                }
            }
        }

        private void btnAnnulus_Click(object sender, EventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.Clear(Color.White);

                foreach (Point p in points)
                {
                    DrawPoint(g, new SolidBrush(Color.Red), p);
                }

                foreach (CircleModel model in this.models)
                {
                    Annulus a = Annulus.SmallestWidthAnnulus(model.Inliers, 5000);

                    Console.WriteLine("### ANNULUS ###");
                    Console.WriteLine("Center: " + a.Center);
                    Console.WriteLine("Inner Radius: " + a.InnerRadius);
                    Console.WriteLine("Outer Radius: " + a.OuterRadius);
                    Console.WriteLine("Width: " + a.Width);
                    Console.WriteLine("Total Inliers: " + model.TotalInliers + "\n");
                    DrawAnnulus(g, new SolidBrush(Color.FromArgb(150, 90, 130, 255)), a);

                    foreach (Point p in model.Inliers)
                    {
                        DrawPoint(g, new SolidBrush(Color.Green), p);
                    }
                }
            }
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            if (points.Count < 3)
                return;

            this.models.Clear();

            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.Clear(Color.White);
                CircleModelFactory cmf = new CircleModelFactory();

                HashSet<Point> outliers = new HashSet<Point>();

                foreach (Point p in this.points)
                {
                    outliers.Add(p);
                    DrawPoint(g, new SolidBrush(Color.Red), p);
                }

                for (;;)
                {
                    CircleModel circleModel = (CircleModel)RANSAC.Compute(outliers.ToArray(), cmf, 3, Int32.Parse(textBox2.Text), Int32.Parse(textBox1.Text), Int32.Parse(textBox3.Text));

                    if (circleModel == null) break;

                    this.models.Add(circleModel);

                    DrawCircle(g, new Pen(Color.DarkRed), circleModel.circle);

                    List<Point> pts = new List<Point>();

                    foreach (Point p in circleModel.Inliers)
                    {
                        DrawPoint(g, new SolidBrush(Color.Green), p);
                        outliers.Remove(p);
                    }
                }
                
                btnAnnulus.Enabled = true;
                btnFPVD.Enabled = true;
                btnCPVD.Enabled = true;
                btnDelaunay.Enabled = true;
                btnHull.Enabled = true;
            }
        }

        private void btnClearSet_Click(object sender, EventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                this.points.Clear();
                this.models.Clear();

                g.Clear(Color.White);
                btnDetect.Enabled = false;
                btnAnnulus.Enabled = false;
                btnHull.Enabled = false;
                btnFPVD.Enabled = false;
                btnCPVD.Enabled = false;
                btnDelaunay.Enabled = false;
            }
        }

        private void btnHull_Click(object sender, EventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                foreach (CircleModel model in this.models)
                    DrawHull(g, new Pen(Color.DarkViolet), GeometryUtils.ConvexHull(model.Inliers));
            }
        }

        private void btnDelaunay_Click(object sender, EventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                foreach (CircleModel model in this.models)
                    DrawDelaunay(g, new Pen(Color.Yellow), Triangle.DelaunayTriangulation(model.Inliers));
            }
        }

        private void btnFPVD_Click(object sender, EventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                foreach (CircleModel model in this.models)
                {
                    Voronoi v = new FurthestVoronoi(GeometryUtils.ConvexHull(model.Inliers), 5000);
                    DrawVoronoi(g, new Pen(Color.Crimson), v);
                }
            }
        }

        private void btnCPVD_Click(object sender, EventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                foreach (CircleModel model in this.models)
                {
                    Voronoi v = new ClosestVoronoi(model.Inliers, 5000);
                    DrawVoronoi(g, new Pen(Color.DarkTurquoise), v);
                }
            }
        }

        private void btnClearLines_Click(object sender, EventArgs e)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.Clear(Color.White);

                foreach (Point p in this.points)
                {
                    DrawPoint(g, new SolidBrush(Color.Black), p);
                }

                btnAnnulus.Enabled = false;
                btnHull.Enabled = false;
                btnFPVD.Enabled = false;
                btnCPVD.Enabled = false;
                btnDelaunay.Enabled = false;
            }
        }

        private void btnLoad1_Click(object sender, EventArgs e)
        {
            this.models.Clear();
            this.points = new HashSet<Point>() {new Point(158, 507), new Point(142, 393), new Point(100, 317), new Point(92, 215), new Point(98, 197), new Point(151, 261), new Point(143, 244), new Point(170, 255), new Point(209, 272), new Point(198, 257), new Point(214, 243), new Point(223, 223), new Point(214, 199), new Point(234, 201), new Point(264, 196), new Point(159, 175), new Point(158, 148), new Point(143, 144), new Point(141, 93), new Point(166, 73), new Point(136, 32), new Point(179, 29), new Point(207, 39), new Point(233, 47), new Point(257, 61), new Point(267, 43), new Point(271, 89), new Point(292, 81), new Point(234, 106), new Point(214, 106), new Point(221, 321), new Point(235, 313), new Point(247, 296), new Point(265, 341), new Point(283, 326), new Point(307, 329), new Point(320, 317), new Point(340, 286), new Point(327, 266), new Point(322, 206), new Point(337, 194), new Point(348, 163), new Point(320, 161), new Point(370, 142), new Point(350, 129), new Point(389, 108), new Point(355, 341), new Point(384, 381), new Point(421, 423), new Point(441, 414), new Point(391, 307), new Point(416, 301), new Point(391, 283), new Point(490, 238), new Point(460, 225), new Point(474, 174), new Point(453, 145), new Point(467, 104), new Point(543, 255), new Point(618, 242), new Point(611, 129)};
            btnClearLines_Click(sender, e);
            btnDetect.Enabled = true;
        }

        private void btnLoad2_Click(object sender, EventArgs e)
        {
            this.models.Clear();
            this.points = new HashSet<Point>() { new Point(164, 442), new Point(244, 433), new Point(249, 413), new Point(287, 395), new Point(303, 407), new Point(308, 390), new Point(342, 396), new Point(295, 424), new Point(349, 449), new Point(366, 435), new Point(449, 306), new Point(317, 343), new Point(481, 279), new Point(455, 251), new Point(501, 241), new Point(505, 215), new Point(388, 244), new Point(147, 278), new Point(142, 225), new Point(135, 186), new Point(126, 110), new Point(194, 84), new Point(206, 116), new Point(213, 132), new Point(202, 125), new Point(184, 122), new Point(169, 107), new Point(154, 134), new Point(162, 151), new Point(188, 176), new Point(200, 179), new Point(215, 159), new Point(214, 201), new Point(216, 234), new Point(270, 103), new Point(348, 89), new Point(326, 97), new Point(351, 98), new Point(388, 94), new Point(373, 111), new Point(368, 154), new Point(360, 148), new Point(356, 133), new Point(336, 137), new Point(331, 142), new Point(320, 146), new Point(305, 140), new Point(308, 153), new Point(281, 158), new Point(273, 162), new Point(274, 187), new Point(285, 218), new Point(302, 221), new Point(311, 234), new Point(316, 198), new Point(329, 187), new Point(344, 233), new Point(399, 178), new Point(414, 177), new Point(475, 126), new Point(490, 103), new Point(456, 100), new Point(520, 106), new Point(559, 95), new Point(562, 84), new Point(564, 76), new Point(543, 87), new Point(549, 77), new Point(544, 72), new Point(530, 82), new Point(517, 68) };
            btnClearLines_Click(sender, e);
            btnDetect.Enabled = true;
        }
    }
}
