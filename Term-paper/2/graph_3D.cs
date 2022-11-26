using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
namespace _2
{
	class graph_3D
	{
		public void drawAxis(int[,] matrEkr, System.Drawing.Graphics gr)
		{
			System.Drawing.Pen pendr = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 1);
			gr.DrawLine(pendr, matrEkr[0, 0], matrEkr[0, 1], matrEkr[1, 0], matrEkr[1, 1]);
			gr.DrawLine(pendr, matrEkr[2, 0], matrEkr[2, 1], matrEkr[3, 0], matrEkr[3, 1]);
			gr.DrawLine(pendr, matrEkr[4, 0], matrEkr[4, 1], matrEkr[5, 0], matrEkr[5, 1]);
		}
		public void writeAxisLeter(int[,] matrEkr, System.Drawing.Graphics gr)
		{
			System.Drawing.Font tf = new System.Drawing.Font("Arial", 8);
			gr.DrawString("X", tf, System.Drawing.Brushes.Red, matrEkr[1, 0], matrEkr[1, 1]);
			gr.DrawString("Y", tf, System.Drawing.Brushes.Red, matrEkr[3, 0], matrEkr[3, 1]);
			gr.DrawString("Z", tf, System.Drawing.Brushes.Red, matrEkr[5, 0], matrEkr[5, 1]);
		}
		public void DrawSurface(int[,] surfCord, System.Drawing.Graphics gr)
		{
			System.Drawing.Pen pendr = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.DarkOrange), 1);
			gr.DrawLine(pendr, surfCord[0, 0], surfCord[0, 1], surfCord[1, 0], surfCord[1, 1]);
			gr.DrawLine(pendr, surfCord[1, 0], surfCord[1, 1], surfCord[2, 0], surfCord[2, 1]);
			gr.DrawLine(pendr, surfCord[2, 0], surfCord[2, 1], surfCord[3, 0], surfCord[3, 1]);
			gr.DrawLine(pendr, surfCord[3, 0], surfCord[3, 1], surfCord[0, 0], surfCord[0, 1]);
		}
		public void fillColor(int[,] planeCord, System.Drawing.Graphics gr)
		{
			System.Drawing.Point[] pts = { new System.Drawing.Point(planeCord[0, 0], planeCord[0, 1]), new System.Drawing.Point(planeCord[1, 0], planeCord[1, 1]), new System.Drawing.Point(planeCord[2, 0], planeCord[2, 1]), new System.Drawing.Point(planeCord[3, 0], planeCord[3, 1]) };
			GraphicsPath g_path = new GraphicsPath { };
			g_path.AddClosedCurve(pts, 0.01f);
			System.Drawing.SolidBrush transBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 100));
			gr.FillPath(transBrush, g_path);
		}
		public void drawFigure(int[,] FgCd, System.Drawing.Graphics gr)
		{
			System.Drawing.Pen figPen = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), 3);
			gr.DrawLine(figPen, FgCd[0, 0], FgCd[0, 1], FgCd[1, 0], FgCd[1, 1]);
			gr.DrawLine(figPen, FgCd[1, 0], FgCd[1, 1], FgCd[2, 0], FgCd[2, 1]);
			gr.DrawLine(figPen, FgCd[2, 0], FgCd[2, 1], FgCd[3, 0], FgCd[3, 1]);
			gr.DrawLine(figPen, FgCd[3, 0], FgCd[3, 1], FgCd[0, 0], FgCd[0, 1]);

			gr.DrawLine(figPen, FgCd[4, 0], FgCd[4, 1], FgCd[5, 0], FgCd[5, 1]);
			gr.DrawLine(figPen, FgCd[5, 0], FgCd[5, 1], FgCd[6, 0], FgCd[6, 1]);
			gr.DrawLine(figPen, FgCd[6, 0], FgCd[6, 1], FgCd[7, 0], FgCd[7, 1]);
			gr.DrawLine(figPen, FgCd[7, 0], FgCd[7, 1], FgCd[4, 0], FgCd[4, 1]);

			gr.DrawLine(figPen, FgCd[0, 0], FgCd[0, 1], FgCd[4, 0], FgCd[4, 1]);
			gr.DrawLine(figPen, FgCd[1, 0], FgCd[1, 1], FgCd[5, 0], FgCd[5, 1]);
			gr.DrawLine(figPen, FgCd[2, 0], FgCd[2, 1], FgCd[6, 0], FgCd[6, 1]);
			gr.DrawLine(figPen, FgCd[3, 0], FgCd[3, 1], FgCd[7, 0], FgCd[7, 1]);
		}
	}
}