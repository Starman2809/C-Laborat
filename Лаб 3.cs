using System;
using System.Globalization;
using System.IO;

namespace Test
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

                int t = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
                int n = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
                double x = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                double y = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

                if (n <= 0 || (t < 0 || t > 2))
                {
                    Console.WriteLine("ERROR");
                    return;
                }

                int i = 1;
                int step;
                double z = 0.0;
                double znam = 1.0;
                double chisl;

                if (t == 0)
                {
                    for (i = 1; i <= n; i++)
                    {
                        step = i * 2;
                        znam *= (step - 1) * step;
                        chisl = (i % 2 == 0) ? -Math.Pow(y, step) : Math.Pow(x, step);
                        z += (step - 1) * (step + 1) * chisl / znam;
                    }
                }
                else if (t == 1)
                {
                    i = 1;
                    while (i <= n)
                    {
                        step = i * 2;
                        znam *= (step - 1) * step;
                        chisl = (i % 2 == 0) ? -Math.Pow(y, step) : Math.Pow(x, step);
                        z += (step - 1) * (step + 1) * chisl / znam;
                        i++;
                    }
                }
                else
                {
                    i = 1;
                    do
                    {
                        step = i * 2;
                        znam *= (step - 1) * step;
                        chisl = (i % 2 == 0) ? -Math.Pow(y, step) : Math.Pow(x, step);
                        z += (step - 1) * (step + 1) * chisl / znam;
                        i++;
                    } while (i <= n);
                }

                Console.WriteLine("{0:0.0000000}", z);
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
