using System;

namespace LR10
{
    class Lab10
    {
        static void Main(string[] args)
        {
            string file1 = "matrix1.txt";
            string file2 = "matrix2.txt";

            int[,] matrix1 = Matrix.ReadMatrixFromFile(file1);
            int[,] matrix2 = Matrix.ReadMatrixFromFile(file2);

            int max1 = Matrix.FindMax(matrix1);
            int max2 = Matrix.FindMax(matrix2);

            Console.WriteLine("Максимум в первой матрице: " + max1);
            Console.WriteLine("Максимум во второй матрице: " + max2);

            System.IO.File.WriteAllText(
                "output.txt",
                "Максимум в первой матрице: " + max1 + "\n" +
                "Максимум во второй матрице: " + max2
            );

            Console.ReadKey();
        }
    }
}