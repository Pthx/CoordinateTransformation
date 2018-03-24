using System;

namespace CoodirationTransformation.Module
{
	//定义矩阵类，含有行列数，矩阵值等成员，打印矩阵等方法，实现了ICloneable接口
	class Matrix: ICloneable
	{
		private uint col;
		public uint Col
		{
			get {return col;}
			set {col = value;}
		}

		private uint row;
		public uint Row 
		{
			get {return row;}
			set {row = value;}
		}

		public double[,] matrixValue;

		public Matrix(uint row, uint col)
		{
			this.matrixValue = new double[row, col];
		}

		//在控制台打印矩阵
		public void printMatrixToConsole()
		{
			Console.WriteLine("打印矩阵：\n");
			for (int i = 0; i < this.row; i++)
			{
				for (int j = 0; j < this.col; j++)
				{
					Console.WriteLine(this.matrixValue[row, col]);
				}
				Console.WriteLine("\n");
			}
		}

		// 实现矩阵的复制
		public object Clone()
		{
			return new Matrix(this.Row, this.Col);
		}
	}
}