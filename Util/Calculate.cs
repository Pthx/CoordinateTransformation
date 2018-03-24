using System;
using CoodirationTransformation.Module;

namespace CoodirationTransformation.Util
{
	class Calculate
	{
		// 判断两个矩阵是否能相乘
		public static bool canMultiply(Matrix matrix1, Matrix matrix2)
		{
			if (matrix1.Col == matrix2.Row)
			{
				return true;
			}
			return false;
		}

		// 矩阵相乘
		public static Matrix multiply(Matrix matrix1, Matrix matrix2)
		{
			if (!canMultiply(matrix1, matrix2))
			{
				return null;
			}
			Matrix result = new Matrix(matrix2.Row, matrix1.Col);
			for (int i = 0; i < matrix1.Row; i++)
			{
				for (int j = 0; j < matrix2.Col; j++)
				{
					for (int k = 0; k < matrix1.Col; k++)
					{
						result.matrixValue[i,j] += (matrix1.matrixValue[i,k] * matrix2.matrixValue[k,j]); 
					}
				}
			}
			return result;
		}

		// 判断矩阵是否等行等列
		public static bool canPointMultiply(Matrix matrix1, Matrix matrix2)
		{
			if (matrix1.Row== matrix2.Row && matrix1.Col == matrix2.Col)
			{
				return true;
			}
			return false;
		}

		// 矩阵点乘
		public static Matrix pointMultiply(Matrix matrix1, Matrix matrix2)
		{
			if (!canPointMultiply(matrix1, matrix2))
			{
				return null;
			}
			Matrix result = (Matrix) matrix1.Clone();
			for (int i = 0; i < matrix1.Row; i++)
			{
				for (int j = 0; j < matrix1.Col; j++)
				{
					result.matrixValue[i,j] = matrix1.matrixValue[i,j] * matrix2.matrixValue[i ,j];
				}
			}
			return result;
		}

		// 矩阵数乘
		public static Matrix numberMultiply(Matrix matrix, double number)
		{
			Matrix result = (Matrix) matrix.Clone();
			for (int i = 0; i < matrix.Row; i++)
			{
				for (int j = 0; j < matrix.Col; j++)
				{
					result.matrixValue[i,j] *= number;
				}
			}
			return result;
		}

		// 判断是否方阵
		public static bool isSqual(Matrix matrix)
		{
			if (matrix.Row == matrix.Col)
			{
				return true;
			}
			return false;
		}

		// 递归求行列式的值。只有满秩矩阵才有逆矩阵，因此如果行列式的值为0（在代码中以绝对值小于1E-6做判断）

		// 矩阵求逆
		public static Matrix matrixInverse(Matrix matrix)
		{
			if (!isSqual(matrix))
			{
				return null;
			}
			Matrix result = (Matrix) matrix.Clone();

			return result;
		}


		/// <summary>
		/// 求矩阵的逆矩阵
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public static double[][] InverseMatrix(double[][] matrix)
		{
			//matrix必须为非空
			if (matrix == null || matrix.Length == 0)
			{
				return new double[][] { };
			}
			//matrix 必须为方阵
			int len = matrix.Length;
			for (int counter = 0; counter < matrix.Length; counter++)
			{
				if (matrix[counter].Length != len)
				{
					throw new Exception("matrix 必须为方阵");
				}
			}
			//计算矩阵行列式的值
			double dDeterminant = Determinant(matrix);
			if (Math.Abs(dDeterminant) <= 1E-6)
			{
				throw new Exception("矩阵不可逆");
			}
			//制作一个伴随矩阵大小的矩阵
			double[][] result = AdjointMatrix(matrix);
			//矩阵的每项除以矩阵行列式的值，即为所求
			for (int i = 0; i < matrix.Length; i++)
			{
				for (int j = 0; j < matrix.Length; j++)
				{
					result[i][j] = result[i][j] / dDeterminant;
				}
			}
			return result;
		}

		/// <summary>
		/// 递归计算行列式的值
		/// </summary>
		/// <param name="matrix">矩阵</param>
		/// <returns></returns>
		public static double Determinant(double[][] matrix)
		{
			//二阶及以下行列式直接计算
			if (matrix.Length == 0) return 0;
			else if (matrix.Length == 1) return matrix[0][0];
			else if (matrix.Length == 2)
			{
			return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
			}
			//对第一行使用“加边法”递归计算行列式的值
			double dSum = 0, dSign = 1;
			for (int i = 0; i < matrix.Length; i++)
			{
				double[][] matrixTemp = new double[matrix.Length - 1][];
				for (int count = 0; count < matrix.Length - 1; count++)
				{
					matrixTemp[count] = new double[matrix.Length - 1];
				}
				for (int j = 0; j < matrixTemp.Length; j++)
				{
					for (int k = 0; k < matrixTemp.Length; k++)
					{
						matrixTemp[j][k] = matrix[j + 1][k >= i ? k + 1 : k];
					}
				}
				dSum += (matrix[0][i] * dSign * Determinant(matrixTemp));
				dSign = dSign * -1;
			}
			return dSum;
		}

		/// <summary>
		/// 计算方阵的伴随矩阵
		/// </summary>
		/// <param name="matrix">方阵</param>
		/// <returns></returns>
		public static double[][] AdjointMatrix(double [][] matrix)
		{
			//制作一个伴随矩阵大小的矩阵
			double[][] result = new double[matrix.Length][];
			for (int i = 0; i < result.Length; i++)
			{
			result[i] = new double[matrix[i].Length];
			}
			//生成伴随矩阵
			for (int i = 0; i < result.Length; i++)
			{
			for (int j = 0; j < result.Length; j++)
			{
			//存储代数余子式的矩阵（行、列数都比原矩阵少1）
			double[][] temp = new double[result.Length - 1][];
			for (int k = 0; k < result.Length - 1; k++)
			{
				temp[k] = new double[result[k].Length - 1];
			}
			//生成代数余子式
			for (int x = 0; x < temp.Length; x++)
			{
				for (int y = 0; y < temp.Length; y++)
				{
				temp[x][y] = matrix[x < i ? x : x + 1][y < j ? y : y + 1];
				}
			}
			//Console.WriteLine("代数余子式:");
			//PrintMatrix(temp);
			result[j][i] = ((i + j) % 2 == 0 ? 1 : -1) * Determinant(temp);
			}
			}
			//Console.WriteLine("伴随矩阵：");
			//PrintMatrix(result);
			return result;
		}


	}
}