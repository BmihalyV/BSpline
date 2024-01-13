using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace __AlapRajzolo
{
    public partial class Form1 : Form
    {
        Graphics g;
        Random rnd = new Random();
        List<PointF> BSplinePoints = new List<PointF>();
        Color colorLines = Color.Black;
        Color colorRectangle = Color.Brown;
        Color colorBspline = Color.DarkBlue;
        int x, j, k, l;

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                BSplinePoints.Add(new PointF(rnd.Next(canvas.Width), rnd.Next(canvas.Height)));
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            for (int i = 0; i < BSplinePoints.Count - 3; i++)
            {
                x = 0;
                j = 1;
                k = 2;
                l = 3;
                DrawBSpline(colorBspline, 
                    BSplinePoints[x], BSplinePoints[x], BSplinePoints[x], BSplinePoints[j]);
                DrawBSpline(colorBspline, 
                    BSplinePoints[x], BSplinePoints[x], BSplinePoints[j], BSplinePoints[k]);

                while (l != BSplinePoints.Count)
                {
                    DrawBSpline(colorBspline, BSplinePoints[x], BSplinePoints[j], BSplinePoints[k], BSplinePoints[l]);
                    x++;
                    j++;
                    k++;
                    l++;
                }

                x = BSplinePoints.Count - 3;
                j = BSplinePoints.Count - 2;
                l = BSplinePoints.Count - 1;
                DrawBSpline(colorBspline, BSplinePoints[x], BSplinePoints[j], BSplinePoints[l], BSplinePoints[l]);
                DrawBSpline(colorBspline, BSplinePoints[j], BSplinePoints[l], BSplinePoints[l], BSplinePoints[l]);
            }
            for (int i = 0; i < BSplinePoints.Count; i++)
                g.FillRectangle(new SolidBrush(colorRectangle), BSplinePoints[i].X - 5, BSplinePoints[i].Y - 5, 10, 10);
            for (int i = 0; i < BSplinePoints.Count - 1; i++)
                g.DrawLine(new Pen(colorLines), BSplinePoints[i], BSplinePoints[i + 1]);
        }

        private double N0(double t) { return 1.0 / 6.0 * (1 - t) * (1 - t) * (1 - t); }
        private double N1(double t) { return 0.5 * t * t * t - t * t + 2.0 / 3.0; }
        private double N2(double t) { return -0.5 * t * t * t + 0.5 * t * t + 0.5 * t + 1.0 / 6.0; }
        private double N3(double t) { return 1.0 / 6.0 * t * t * t; }

        private void DrawBSpline(Color c0, PointF p0, PointF p1, PointF p2, PointF p3)
        {
            double a = 0;
            double t = a;
            double h = 1.0 / 500.0;
            PointF d0, d1;

            d0 = new PointF((float)(N0(t) * p0.X + N1(t) * p1.X + N2(t) * p2.X + N3(t) * p3.X),
                            (float)(N0(t) * p0.Y + N1(t) * p1.Y + N2(t) * p2.Y + N3(t) * p3.Y));
            while (t < 1)
            {
                t += h;
                d1 = new PointF((float)(N0(t) * p0.X + N1(t) * p1.X + N2(t) * p2.X + N3(t) * p3.X),
                                (float)(N0(t) * p0.Y + N1(t) * p1.Y + N2(t) * p2.Y + N3(t) * p3.Y));

                g.DrawLine(new Pen(c0, 3f), d0, d1);
                d0 = d1;
            }
        }
    }
}
