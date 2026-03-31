using System;
using System.Globalization;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                double a1 = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                double a2 = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                double a3 = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                double a4 = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                double a5 = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

                bool outOfRange =
                    a1 < 0 || a1 > 100000 ||
                    a2 < 0 || a2 > 100000 ||
                    a3 < 0 || a3 > 100000 ||
                    a4 < 0 || a4 > 100000 ||
                    a5 < 0 || a5 > 100000;

                if (outOfRange || a5 == 0 || (a2 - a3) < 0)
                {
                    Console.WriteLine("ERROR");
                }
                else
                {
                    double s = a1 * Math.Sqrt(a2 - a3) * a4 / a5;
                    Console.WriteLine("{0:0.000}", s);
                }
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }
    }
}
