using System;
using System.IO;
using System.Collections;
using CoodirationTransformation.Module;

namespace CoodirationTransformation.Util
{
	class FileIO 
	{
		// 从文件中获取矩阵，传入文件的路径path
		public static void readMatrixFromFile(String path)
		{
			Queue lineQueue = new Queue();
			StreamReader file = new StreamReader(path);
			string line;
			while((line = file.ReadLine()) != null)
			{
				lineQueue.Enqueue(line);
			}

			Matrix matrix = new Matrix((uint) lineQueue.Count, (uint) lineQueue.Count);
			int counter = 0;
			foreach (var item in lineQueue)
			{
				string notSplitString = (string) item;
				string[] splitString = notSplitString.Split(",");
				counter++;
				for (int i = 0; i< lineQueue.Count; i++)
				{
					matrix.matrixValue[counter, i] = Convert.ToDouble(splitString[i]);
				}//Index was outside the bounds of the array
			}

			matrix.printMatrixToConsole();
		}

		// 把矩阵写入矩阵，传入矩阵matrix和路径path
		public static void writeMatrixToFile(Matrix matrix, String path)
		{
			
		}
	}
}