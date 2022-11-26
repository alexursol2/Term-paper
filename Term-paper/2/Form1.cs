using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace _2
{
	public partial class Form1 : Form
	{
		math_3D m3d = new math_3D();
		graph_3D g3d = new graph_3D();

		float[,] matrixAxStartCord; //Для начальних координат осей
		float[,] matrixAxEndCord; //Для координат осей после трансформаций
		float[,] matrixAxDecCord; //Однородные координаты
		int[,] matrixAxDispCord; //для экранных координат
		float[,] matrixTransf; //матрица трансформаций

		float[,] surfaceXOY;
		float[,] surfaceXOYEnd;
		float[,] surfaceXOYDec;
		int[,] surfaceXOYDisp;

		float[,] surfaceXOZ;
		float[,] surfaceXOZEnd;
		float[,] surfaceXOZDec;
		int[,] surfaceXOZDisp;

		float[,] surfaceYOZ;
		float[,] surfaceYOZEnd;
		float[,] surfaceYOZDec;
		int[,] surfaceYOZDisp;

		float[,] matrixFgGenTr;
		float[,] matrixFgStartCord;
		float[,] matrixFgEndCord;
		float[,] matrixFgDecCord;
		int[,] matrixFgDispCord;
		float[,] mxScaleFig;

		float ml, mm, mn, ms;

		float[,] matrixAxTrRotY; //Поворот по оси У
		float[,] matrixAxTrRotX; //Поворот по оси Х
		float[,] matrixOrtogZ; //Поворот по оси Z
		float[,] matrixGenTr; //оканчательная основная матрица трансформации

		int H, W;

		float cAx, mmh, mmw;

		float degSt; //единный радиан

		int rotY, rotX;//угол поворота

		Graphics graph;

		Graphics gr;
		Bitmap fromBitmap;

		Font f;

		bool iamStarted = false;

		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			W = picDraw.Width;
			H = picDraw.Height;
			cAx = 10;
			mmw = W / (2 * (cAx + 1));
			mmh = H / (2 * (cAx + 1));
			degSt = (float)Math.PI / 180;
			rotX = 0;
			rotY = 0;
			ml = 0;
			mn = 0;
			mm = 0;
			ms = 1;
			//матрица координат осей
			matrixTransf = new float[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { ml, mm, mn, ms } };
			matrixAxStartCord = new float[6, 4] { { -cAx, 0, 0, 1 }, { cAx, 0, 0, 1 }, { 0, -cAx, 0, 1 }, { 0, cAx, 0, 1 }, { 0, 0, -cAx, 1 }, { 0, 0, cAx, 1 } };
			matrixAxTrRotY = new float[4, 4] { { (float)Math.Cos(degSt * rotY), 0, (float)-Math.Sin(degSt * rotY), 0 }, { 0, 1, 0, 0 }, { (float)Math.Sin(degSt * rotY), 0, (float)Math.Cos(degSt * rotY), 0 }, { 0, 0, 0, 1 } };
			matrixAxTrRotX = new float[4, 4] { { 1, 0, 0, 0 }, { 0, (float)Math.Cos(degSt * rotX), (float)Math.Sin(degSt * rotX), 0 }, { 0, (float)-Math.Sin(degSt * rotX), (float)Math.Cos(degSt * rotX), 0 }, { 0, 0, 0, 1 } };

			matrixOrtogZ = new float[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 1 }, };

			matrixGenTr = new float[4, 4];
			matrixAxEndCord = new float[6, 4];
			matrixAxDecCord = new float[6, 4];
			matrixAxDispCord = new int[6, 4];
			//плоскости
			surfaceXOY = new float[4, 4] { {-cAx, cAx, 0, 1}, { cAx, cAx, 0, 1 }, { cAx, -cAx, 0, 1 }, { -cAx, -cAx, 0, 1 } };
			surfaceXOYEnd = new float[4, 4];
			surfaceXOYDec = new float[4, 4];
			surfaceXOYDisp = new int[4, 4];

			surfaceXOZ = new float[4, 4] { { -cAx, 0, -cAx, 1 }, { cAx, 0, -cAx, 1 }, { cAx, 0, cAx, 1 }, { -cAx, 0, cAx, 1 } };
			surfaceXOZEnd = new float[4, 4];
			surfaceXOZDec = new float[4, 4];
			surfaceXOZDisp = new int[4, 4];

			surfaceYOZ = new float[4, 4] { { 0,-cAx, -cAx, 1 }, { 0, -cAx, cAx, 1 }, { 0, cAx, cAx, 1 }, {0, cAx, -cAx, 1 } };
			surfaceYOZEnd = new float[4, 4];
			surfaceYOZDec = new float[4, 4];
			surfaceYOZDisp = new int[4, 4];
			
			matrixFgGenTr = new float[4, 4];
			matrixFgStartCord = new float[8, 4] { { 0, 0, 0, 1 }, { 0, 4, 0, 1 }, { 4, 4, 0, 1 }, { 4, 0, 0, 1 }, { 0, 0, 4, 1 }, { 0, 4, 4, 1 }, { 4, 4, 4, 1 }, { 4, 0, 4, 1 } };
			matrixFgEndCord = new float[8, 4];
			matrixFgDecCord = new float[8, 4];
			matrixFgDispCord = new int[8, 4];
			mxScaleFig = new float[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, ms } };

			dgvAxisStart.RowCount = 6;
			dgvAxisFin.RowCount = 6;
			dgvAxisDec.RowCount = 6;
			dgvAxisEkr.RowCount = 6;
			dgvTransf.RowCount = 4;
			//заполнение матриц
			m3d.fillMatrixmxn(matrixAxStartCord, dgvAxisStart);
			m3d.fillMatrixmxn(matrixTransf, dgvTransf);
			//график
			f = new Font("Arial", 10);
			graph = picDraw.CreateGraphics();
			fromBitmap = new Bitmap(W, H);
			gr = Graphics.FromImage(fromBitmap);
			picDraw.Image = fromBitmap;

			startGeneral();

			iamStarted = true;
		}

		private void startGeneral()
		{
			mathGeneral();
			graphGeneral();
		}
		//Calculating projection from 3D world to 2D canvas
		private void mathGeneral()
		{
			m3d.creatGeneralTransfMx(matrixAxTrRotY, matrixAxTrRotX, matrixOrtogZ, matrixGenTr, dgvAxisEkr);
			m3d.matrixAxTransformation(matrixAxStartCord, matrixGenTr, matrixAxEndCord, dgvAxisFin);
			m3d.homogToDec(matrixAxEndCord, matrixAxDecCord);
			m3d.fillMatrixmxn(matrixAxDecCord, dgvAxisDec);
			m3d.calculateDispCord(matrixAxDispCord, matrixAxDecCord, W, H,mmw, mmh);
			m3d.fillMatrixmxn(matrixAxDispCord, dgvAxisEkr);

			m3d.matrixFgTransformation(matrixTransf, mxScaleFig, matrixGenTr, matrixFgGenTr, matrixFgStartCord, matrixFgEndCord);
			m3d.homogToDec(matrixFgEndCord, matrixFgDecCord);
			m3d.calculateDispCord(matrixFgDispCord, matrixFgDecCord, W, H, mmw, mmh);

			if (chbSurface.Checked)
			{
				m3d.matrixTransformationSurface(surfaceXOY, surfaceXOZ, surfaceYOZ, matrixGenTr, surfaceXOYEnd, surfaceXOZEnd, surfaceYOZEnd);
				m3d.homogToDec(surfaceXOYEnd, surfaceXOYDec);
				m3d.calculateDispCord(surfaceXOYDisp, surfaceXOYDec, W, H, mmw, mmh);

				m3d.homogToDec(surfaceXOZEnd, surfaceXOZDec);
				m3d.calculateDispCord(surfaceXOZDisp, surfaceXOZDec, W, H, mmw, mmh);

				m3d.homogToDec(surfaceYOZEnd, surfaceYOZDec);
				m3d.calculateDispCord(surfaceYOZDisp, surfaceYOZDec, W, H, mmw, mmh);
			}
		}
		//Drawing 3D in 2D display
		private void graphGeneral()
		{
			g3d.drawAxis(matrixAxDispCord, gr);
			g3d.writeAxisLeter(matrixAxDispCord, gr);
			g3d.drawFigure(matrixFgDispCord, gr);

			if (chbSurface.Checked)
			{
				g3d.DrawSurface(surfaceXOYDisp, gr);
				g3d.DrawSurface(surfaceXOZDisp, gr);
				g3d.DrawSurface(surfaceYOZDisp, gr);

				if (chbFillSurface.Checked)
				{
					g3d.fillColor(surfaceXOYDisp, gr);
					g3d.fillColor(surfaceXOZDisp, gr);
					g3d.fillColor(surfaceYOZDisp, gr);
				}
			}
		}
		//чистит и рисует изменения
		private void updatePicBox()
		{
			gr.Clear(picDraw.BackColor);
			startGeneral();
			picDraw.Image = fromBitmap;
		}
		//upgrade calculation and graph after form resize
		private void globalUpdate()
		{
			W = picDraw.Width;
			H = picDraw.Height;
			mmw = W / (2 * (cAx + 1));
			mmh = H / (2 * (cAx + 1));
			graph = picDraw.CreateGraphics();
			fromBitmap = new Bitmap(W, H);
			gr = Graphics.FromImage(fromBitmap);
			picDraw.Image = fromBitmap;
			startGeneral();
		}
		private void trbRotY_ValueChanged(object sender, EventArgs e)
		{
			rotY = trbRotY.Value;
			txtRotY.Text = rotY.ToString();
			matrixAxTrRotY[0, 0] = (float)Math.Cos(degSt * rotY);
			matrixAxTrRotY[0, 1] = 0;
			matrixAxTrRotY[0, 2] = (float)-Math.Sin(degSt * rotY);
			matrixAxTrRotY[0, 3] = 0;
			matrixAxTrRotY[1, 0] = 0;
			matrixAxTrRotY[1, 1] = 1;
			matrixAxTrRotY[1, 2] = 0;
			matrixAxTrRotY[1, 3] = 0;
			matrixAxTrRotY[2, 0] = (float)Math.Sin(degSt * rotY);
			matrixAxTrRotY[2, 1] = 0;
			matrixAxTrRotY[2, 2] = (float)Math.Cos(degSt * rotY);
			matrixAxTrRotY[2, 3] = 0;
			matrixAxTrRotY[3, 0] = 0;
			matrixAxTrRotY[3, 1] = 0;
			matrixAxTrRotY[3, 2] = 0;
			matrixAxTrRotY[3, 3] = 1;
			updatePicBox();
		}

		private void trbRotX_ValueChanged_1(object sender, EventArgs e)
		{
			rotX = trbRotX.Value;
			txtRotX.Text = rotX.ToString();
			matrixAxTrRotX[0, 0] = 1;
			matrixAxTrRotX[0, 1] = 0;
			matrixAxTrRotX[0, 2] = 0;
			matrixAxTrRotX[0, 3] = 0;
			matrixAxTrRotX[1, 0] = 0;
			matrixAxTrRotX[1, 1] = (float)Math.Cos(degSt * rotX);
			matrixAxTrRotX[1, 2] = (float)Math.Sin(degSt * rotX);
			matrixAxTrRotX[1, 3] = 0;
			matrixAxTrRotX[2, 0] = 0;
			matrixAxTrRotX[2, 1] = (float)-Math.Sin(degSt * rotX);
			matrixAxTrRotX[2, 2] = (float)Math.Cos(degSt * rotX);
			matrixAxTrRotX[2, 3] = 0;
			matrixAxTrRotX[3, 0] = 0;
			matrixAxTrRotX[3, 1] = 0;
			matrixAxTrRotX[3, 2] = 0;
			matrixAxTrRotX[3, 3] = 1;
			updatePicBox();
		}
		private void trbCountAxis_ValueChanged(object sender, EventArgs e)
		{
			if (iamStarted)
			{
				cAx = trbCountAxis.Value;
				txtCountAxis.Text = cAx.ToString();
				globalUpdate();

			}
		}
		private void chbSurface_CheckedChanged(object sender, EventArgs e)
		{
			globalUpdate();
		}

		private void chbFillSurface_CheckedChanged(object sender, EventArgs e)
		{
			globalUpdate();
		}
		private void trcbL_ValueChanged(object sender, EventArgs e)
		{
			ml = trcbL.Value;
			txtL.Text = ml.ToString();
			matrixTransf[3, 0] = ml;
			updatePicBox();
		}
		private void trcbM_ValueChanged(object sender, EventArgs e)
		{
			mm = trcbM.Value;
			txtM.Text = mm.ToString();
			matrixTransf[3, 1] = mm;
			updatePicBox();
		}

		private void trcbN_ValueChanged(object sender, EventArgs e)
		{
			mn = trcbN.Value;
			txtN.Text = mn.ToString();
			matrixTransf[3, 2] = mn;
			updatePicBox();
		}

		private void trcbScale1_ValueChanged(object sender, EventArgs e)
		{
			float km = 0.1f;
			ms = km * trcbScale1.Value;
			txtS.Text = Convert.ToString(1 / ms);
			mxScaleFig[3, 3] = ms;
			if (iamStarted)
			{
				updatePicBox();
			}
		}
	}
}