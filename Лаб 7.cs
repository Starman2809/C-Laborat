using System;
using System.Globalization;
using System.IO;

namespace Lab7
{

    internal sealed class NegativeXGraph
    {
        private double _a;
        private double _b;

     
        public NegativeXGraph()
        {
            _a = 0.0;
            _b = 1.0;
        }

        public NegativeXGraph(double a, double b)
        {
            _a = a;
            _b = b;
        }

       
        public static NegativeXGraph CreateFromFile()
        {
            string lineA = Console.ReadLine();
            string lineB = Console.ReadLine();
            double a = Convert.ToDouble(lineA, CultureInfo.InvariantCulture);
            double b = Convert.ToDouble(lineB, CultureInfo.InvariantCulture);
            return new NegativeXGraph(a, b);
        }

        public void Input()
        {
            Console.Write("Введите a: ");
            _a = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.Write("Введите b: ");
            _b = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
        }

        public void Output()
        {
            Console.WriteLine("a = " + _a.ToString("0.###", CultureInfo.InvariantCulture));
            Console.WriteLine("b = " + _b.ToString("0.###", CultureInfo.InvariantCulture));
        }

        /// <summary>∫_a^b (-x) dx = (a² - b²) / 2</summary>
        public double Integral()
        {
            return Integral(_a, _b);
        }

        public static double Integral(double a, double b)
        {
            return (a * a - b * b) / 2.0;
        }

        /// <summary>Длина отрезка от (a, y(a)) до (b, y(b)) для y = -x: |b - a|·√2.</summary>
        public double SegmentLength()
        {
            return SegmentLength(_a, _b);
        }

        public static double SegmentLength(double a, double b)
        {
            return Math.Sqrt(2.0) * Math.Abs(b - a);
        }

        public void Info()
        {
            Console.WriteLine("--- График y = -x ---");
            Output();
            Console.WriteLine(
                "Интеграл от a до b: " + Integral().ToString("0.###", CultureInfo.InvariantCulture));
            Console.WriteLine(
                "Длина отрезка графика: " + SegmentLength().ToString("0.###", CultureInfo.InvariantCulture));
        }

        public void Info(ConsoleColor foreground, ConsoleColor background)
        {
            ConsoleColor oldFg = Console.ForegroundColor;
            ConsoleColor oldBg = Console.BackgroundColor;
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            try
            {
                Info();
            }
            finally
            {
                Console.ForegroundColor = oldFg;
                Console.BackgroundColor = oldBg;
            }
        }

        public void Info(string title)
        {
            Console.WriteLine(title);
            Info();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
#if !DEBUG
            TextWriter saveOut = Console.Out;
            TextReader saveIn = Console.In;
            StreamWriter newOut = null;
            StreamReader newIn = null;

            try
            {
                newOut = new StreamWriter("graph_output.txt");
                newIn = new StreamReader("graph_input.txt");
                Console.SetOut(newOut);
                Console.SetIn(newIn);

                NegativeXGraph g1 = NegativeXGraph.CreateFromFile();
                NegativeXGraph g2 = new NegativeXGraph(5.6, 5.11);

                g1.Info();
                g2.Info();
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
#else
            Console.WriteLine("DEBUG: ввод границ a и b с клавиатуры.");
            NegativeXGraph g = new NegativeXGraph();
            g.Input();
            g.Info(ConsoleColor.Yellow, ConsoleColor.Blue);
            Console.WriteLine();
            g.Info("Перегрузка Info(string):");
            Console.ReadKey();
#endif
        }
    }
}
