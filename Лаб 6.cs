using System;
using System.Globalization;

namespace Lab6
{
    internal sealed class Sphere
    {
        private double _radius;
        private string _material;

        public Sphere()
        {
            _radius = 1.0;
            _material = "Unknown";
        }

        public void Input()
        {
            Console.Write("Введите радиус: ");
            string radiusLine = Console.ReadLine();
            double radius = Convert.ToDouble(radiusLine, CultureInfo.InvariantCulture);
            if (radius <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be positive.");
            }

            Console.Write("Введите материал: ");
            string material = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(material))
            {
                material = "Unknown";
            }

            _radius = radius;
            _material = material.Trim();
        }

        public void Output()
        {
            Console.WriteLine("Радиус: " + _radius.ToString("0.###", CultureInfo.InvariantCulture));
            Console.WriteLine("Материал: " + _material);
        }

        public double GetDiameter()
        {
            return 2.0 * _radius;
        }

        public double GetSurfaceArea()
        {
            return 4.0 * Math.PI * _radius * _radius;
        }

        public double GetVolume()
        {
            return (4.0 / 3.0) * Math.PI * _radius * _radius * _radius;
        }

        public void PrintInfo()
        {
            Output();
            Console.WriteLine("Диаметр: " + GetDiameter().ToString("0.###", CultureInfo.InvariantCulture));
            Console.WriteLine("Площадь поверхности: " + GetSurfaceArea().ToString("0.###", CultureInfo.InvariantCulture));
            Console.WriteLine("Объем: " + GetVolume().ToString("0.###", CultureInfo.InvariantCulture));
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("Build configuration: DEBUG");
            Console.WriteLine("DEBUG: data entry from keyboard.");
#else
            Console.WriteLine("Build configuration: RELEASE");
            Console.WriteLine("RELEASE: object initialized with default values.");
#endif

            Sphere sphere = new Sphere();

#if DEBUG
            sphere.Input();
#endif

            sphere.PrintInfo();
            Console.ReadKey();
        }
    }
}
