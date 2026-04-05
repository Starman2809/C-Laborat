using System;
using System.IO;

namespace LR10
{
    class Matrix
    {
        public static int[,] ReadMatrixFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            int rows = lines.Length;
            int cols = lines[0].Split(' ').Length;

            int[,] matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string[] numbers = lines[i].Split(' ');

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = int.Parse(numbers[j]);
                }
            }

            return matrix;
        }

        public static int FindMax(int[,] matrix)
        {
            int max = matrix[0, 0];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                    }
                }
            }

            return max;
        }
    }
}