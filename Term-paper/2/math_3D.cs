using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
namespace _2
{
	class math_3D
	{
		//matrixTransformation(multiple)=homotodec=CalculateDispCord
		public void multipleMatrixnxm(float[,] m1, float[,] m2, float[,] rez)
		{
			int i, j, r;
			int firstMxRow = 0;
			int firstMxCol = 0;
			int secondMxRow = 0;
			int secondMxCol = 0;
			int rezMxRow = 0;
			int rezMxCol = 0;

			firstMxRow = m1.GetLength(0);
			firstMxCol = m1.GetLength(1);
			secondMxRow = m2.GetLength(0);
			secondMxCol = m2.GetLength(1);
			rezMxRow = rez.GetLength(0);
			rezMxCol = rez.GetLength(1);

			for (i = 0; i < rezMxRow; i++)
			{
				for (j = 0; j < rezMxCol; j++)
				{
					rez[i, j] = 0;
				}
			}
			if (firstMxCol == secondMxRow)
			{
				for (i = 0; i < firstMxRow; i++)
				{
					for (j = 0; j < secondMxCol; j++)
					{
						for (r = 0; r < firstMxCol; r++)
						{
							rez[i, j] = rez[i, j] + m1[i, r] * m2[r, j];
						}
					}
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("The first matrix Columns count and the second " + "matrix Rows count must be equals", "Important");
				return;
			}
		}
		public void homogToDec(float[,] endCord, float[,] decCord)
		{
			//із однорідної системи координат переходимо в декартову систему
			int mi = 0;
			int mj = 0;
			int firstMxRow = endCord.GetLength(0);
			int firstMxCol = endCord.GetLength(1);
			for (mi = 0; mi < firstMxRow; mi++)
			{
				for (mj = 0; mj < firstMxCol; mj++)
				{
					if (endCord[mi, 3] != 1)
					{
						if (endCord[mi, 3] == 0)
						{
							endCord[mi, 3] = 0.01f;
						}
						else
						{
							decCord[mi, mj] = endCord[mi, mj] / endCord[mi, 3];
						}
					}
					else
					{
						decCord[mi, mj] = endCord[mi, mj];
					}
				}
			}
		}
		//із декартових координат в дісплейні
		public void calculateDispCord(int[,] mDisp, float[,] mDek, int W, int H, float mmw, float mmh)
		{
			int i, j;
			int mDekerk = mDek.GetLength(0);
			int mDekLayan = mDek.GetLength(1);
			for (i = 0; i < mDekerk; i++)
			{
				for (j = 0; j < mDekLayan - 1; j++)
				{
					if (j == 0)
					{
						mDisp[i, 0] = (int)(W / 2 + (mmw * mDek[i, j]));
					}
					if (j == 1)
					{
						mDisp[i, 1] = (int)(H / 2 - (mmh * mDek[i, j]));
					}
				}
			}
		}
		// заповнення матрици int
		public void fillMatrixmxn(int[,] m, System.Windows.Forms.DataGridView dgv)
		{
			int i, j;
			int arajimatricaerk = 0;
			int arajimatricalayn = 0;
			arajimatricaerk = m.GetLength(0);
			arajimatricalayn = m.GetLength(1);
			dgv.RowCount = arajimatricaerk;
			if (arajimatricaerk > dgv.RowCount)
			{
				System.Windows.Forms.MessageBox.Show("datagride Rows count must be greater " + "than matrix rows count", "Important");
				return;
			}
			for (i = 0; i < arajimatricaerk; i++)
			{
				for (j = 0; j < arajimatricalayn; j++)
				{
					dgv.Rows[i].Cells[j].Value = m[i, j];
				}
			}
		}
		// заполнение матрицы float
		public void fillMatrixmxn(float[,] m, System.Windows.Forms.DataGridView dgv)
		{
			int i, j;
			int arajimatricaerk = 0;
			int arajimatricalayn = 0;
			arajimatricaerk = m.GetLength(0);
			arajimatricalayn = m.GetLength(1);
			dgv.RowCount = arajimatricaerk;
			if (arajimatricaerk > dgv.RowCount)
			{
				System.Windows.Forms.MessageBox.Show("datagride Rows count must be greater " + "than matrix rows count", "Important");
				return;
			}
			for (i = 0; i < arajimatricaerk; i++)
			{
				for (j = 0; j < arajimatricalayn; j++)
				{
					dgv.Rows[i].Cells[j].Value = m[i, j];
				}
			}
		}
		//Calculating General Transformation Matrix
		public void creatGeneralTransfMx(Single[,] mxRY, Single[,] mxRX, Single[,] mxOrtoZ, Single[,] mxGT, System.Windows.Forms.DataGridView dgv1)
		{
			float[,] tempM = new float[4, 4];
			multipleMatrixnxm(mxRY, mxRX, tempM);
			multipleMatrixnxm(tempM, mxOrtoZ, mxGT);
			//X*Y*Z
			fillMatrixmxn(mxGT, dgv1);
		}
		//Calculating coordinats after transformation
		public void matrixAxTransformation(Single[,] matrixAxStartCord, Single[,] matrixGenTr, Single[,] matrixAxEndCord,  System.Windows.Forms.DataGridView dgv1)
		{
			multipleMatrixnxm(matrixAxStartCord, matrixGenTr, matrixAxEndCord);
			fillMatrixmxn(matrixAxEndCord, dgv1);
		}
		//Calculating оси coordinats after transformation
		public void matrixTransformationSurface(Single[,] surfaceXOY, Single[,] surfaceXOZ, Single[,] surfaceYOZ, 
			Single[,] matrixGenTr, Single[,] surfaceXOYEnd, Single[,] surfaceXOZEnd, Single[,] surfaceYOZEnd)
		{
			multipleMatrixnxm(surfaceXOY, matrixGenTr, surfaceXOYEnd);
			multipleMatrixnxm(surfaceXOZ, matrixGenTr, surfaceXOZEnd);
			multipleMatrixnxm(surfaceYOZ, matrixGenTr, surfaceYOZEnd);
		}
		//Calculating figure coordinates after transformation
		public void matrixFgTransformation(Single[,] matrixTransf, Single[,] mxScaleFig, Single[,] matrixGenTr,
			Single[,] matrixFgGenTr, Single[,] matrixFgStartCord, Single[,] matrixFgEndCord)
		{
			float[,] tempM = new float[4, 4];
			multipleMatrixnxm(matrixTransf, mxScaleFig, tempM);
			multipleMatrixnxm(tempM, matrixGenTr, matrixFgGenTr);
			multipleMatrixnxm(matrixFgStartCord, matrixFgGenTr, matrixFgEndCord);
		}
	}
}