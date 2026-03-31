using System;
using System.Globalization;
using System.IO;

namespace Lab4
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

                int n = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
                if (n <= 0)
                {
                    Console.WriteLine("ERROR");
                    return;
                }

                string numbersLine = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(numbersLine))
                {
                    Console.WriteLine("ERROR");
                    return;
                }

                string[] parts = numbersLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != n)
                {
                    Console.WriteLine("ERROR");
                    return;
                }

                double[] numbers = new double[n];
                double sum = 0.0;
                for (int i = 0; i < n; i++)
                {
                    double value = Convert.ToDouble(parts[i], CultureInfo.InvariantCulture);
                    if (value < 0.0 || value > 100000.0)
                    {
                        Console.WriteLine("ERROR");
                        return;
                    }

                    numbers[i] = value;
                    sum += value;
                }

                double average = sum / n;

                bool hasBelowFifty = false;
                double product = 1.0;
                for (int i = 0; i < n; i++)
                {
                    if (numbers[i] < 50.0)
                    {
                        product *= numbers[i];
                        hasBelowFifty = true;
                    }
                }

                Console.WriteLine(hasBelowFifty ? product.ToString(CultureInfo.InvariantCulture) : "0");

                bool first = true;
                for (int i = 0; i < n; i++)
                {
                    if (numbers[i] > average)
                    {
                        if (!first)
                        {
                            Console.Write(" ");
                        }
                        Console.Write(numbers[i].ToString(CultureInfo.InvariantCulture));
                        first = false;
                    }
                }
                Console.WriteLine();
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
