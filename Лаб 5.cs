using System;
using System.Globalization;
using System.IO;

namespace Lab5
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TextWriter saveOut = Console.Out;
            TextReader saveIn = Console.In;
            StreamWriter newOut = null;
            StreamReader newIn = null;

            try
            {
                newOut = new StreamWriter("output.txt");
                newIn = new StreamReader("input.txt");
                Console.SetOut(newOut);
                Console.SetIn(newIn);

                int m = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
                int n = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
                int x = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
                int y = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);

                if (m <= 0 || n <= 0)
                {
                    Console.WriteLine("ERROR");
                    return;
                }

                int[,] matrix = new int[m, n];
                int seed = unchecked(((m * 73856093) ^ (n * 19349663) ^ (x * 83492791) ^ (y * 297121507)) & 0x7fffffff);
                Random random = new Random(seed);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = random.Next(-1000, 1001);
                    }
                }

                Console.WriteLine("*** Source matrix ***");
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(matrix[i, j]);
                        if (j < n - 1)
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("*** Min elements for columns ***");
                for (int j = 0; j < n; j++)
                {
                    int min = matrix[0, j];
                    for (int i = 1; i < m; i++)
                    {
                        if (matrix[i, j] < min)
                        {
                            min = matrix[i, j];
                        }
                    }
                    Console.Write(min);
                    if (j < n - 1)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();

                long evenSum = 0;
                int evenCount = 0;
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (matrix[i, j] % 2 == 0)
                        {
                            evenSum += matrix[i, j];
                            evenCount++;
                        }
                    }
                }

                double sp = evenCount > 0 ? (double)evenSum / evenCount : 0.0;
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "sp = {0:0.000}", sp));

                Console.WriteLine("*** Modified matrix ***");
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int value;
                        if (matrix[i, j] % 2 != 0)
                        {
                            value = y;
                        }
                        else if (matrix[i, j] < sp)
                        {
                            value = x;
                        }
                        else
                        {
                            value = matrix[i, j];
                        }

                        Console.Write(value);
                        if (j < n - 1)
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
            finally
            {
                Console.SetOut(saveOut);
                Console.SetIn(saveIn);
                if (newOut != null)
                {
                    newOut.Close();
                }
                if (newIn != null)
                {
                    newIn.Close();
                }
            }
        }
    }
}
