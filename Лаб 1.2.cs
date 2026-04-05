using System;

namespace VariantLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Формула: R_x = a*b + b/t - x + f*i2
            double a = 4.5;
            double b = 10.0;
            double t = 2.0;
            double x = 3.2;
            double f = 1.8;
            double i2 = 2.0;

            double rX = a * b + b / t - x + f * i2;

            Console.WriteLine(
                "a = {0:F2}, b = {1:F2}, t = {2:F2}, x = {3:F2}, f = {4:F2}, i2 = {5:F2}, Rx = {6:F2}",
                a, b, t, x, f, i2, rX);
            Console.ReadKey();
        }
    }
}
