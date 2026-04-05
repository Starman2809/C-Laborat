using System;

namespace Lab8
{
    /*
     * Иерархия по индивидуальному заданию (снизу вверх):
     *   Квадрат  ──┐
     *              ├──► Четырехугольник ──┐
     *   Ромб     ──┘                      ├──► Геометрия
     *   Треугольник ──────────────────────┘
     */

    /// <summary>Цвет фигуры (требование лабораторной — перечисление).</summary>
    internal enum ЦветФигуры
    {
        Красный,
        Синий,
        Зелёный,
        Жёлтый
    }

    /// <summary>Корень иерархии — абстрактная геометрическая фигура (не создаётся напрямую).</summary>
    internal abstract class Геометрия
    {
        public ЦветФигуры Цвет { get; }

        protected Геометрия(ЦветФигуры цвет)
        {
            Цвет = цвет;
        }

        public virtual string Наименование => "Геометрическая фигура";

        public abstract double Площадь { get; }

        public abstract double Периметр { get; }

        /// <summary>Базовое текстовое описание; переопределяется в производных классах.</summary>
        public virtual string Описание()
        {
            return string.Format(
                "Цвет: {0}, тип: {1}, площадь: {2:0.###}, периметр: {3:0.###}",
                Цвет,
                Наименование,
                Площадь,
                Периметр);
        }
    }

    /// <summary>Промежуточный класс — четырёхугольник со сторонами a, b, c, d.</summary>
    internal abstract class Четырехугольник : Геометрия
    {
        protected readonly double _a;
        protected readonly double _b;
        protected readonly double _c;
        protected readonly double _d;

        protected Четырехугольник(ЦветФигуры цвет, double a, double b, double c, double d)
            : base(цвет)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }

        public override string Наименование => "Четырехугольник";

        public override double Периметр => _a + _b + _c + _d;

        public abstract override double Площадь { get; }

        public override string Описание()
        {
            return base.Описание() + string.Format(
                " (стороны: {0:0.###}; {1:0.###}; {2:0.###}; {3:0.###})",
                _a,
                _b,
                _c,
                _d);
        }
    }

    internal sealed class Квадрат : Четырехугольник
    {
        private readonly double _сторона;

        public Квадрат(ЦветФигуры цвет, double сторона)
            : base(цвет, сторона, сторона, сторона, сторона)
        {
            _сторона = сторона;
        }

        public override string Наименование => "Квадрат";

        public override double Площадь => _сторона * _сторона;

        public override string Описание()
        {
            return base.Описание() + string.Format(", квадрат: сторона {0:0.###}", _сторона);
        }
    }

    internal sealed class Ромб : Четырехугольник
    {
        private readonly double _сторона;
        private readonly double _уголМеждуСторонамиРад;

        public Ромб(ЦветФигуры цвет, double сторона, double уголГрадусы)
            : base(цвет, сторона, сторона, сторона, сторона)
        {
            _сторона = сторона;
            _уголМеждуСторонамиРад = уголГрадусы * Math.PI / 180.0;
        }

        public override string Наименование => "Ромб";

        public override double Площадь => _сторона * _сторона * Math.Sin(_уголМеждуСторонамиРад);

        public override string Описание()
        {
            return base.Описание() + string.Format(", ромб: сторона {0:0.###}", _сторона);
        }
    }

    internal sealed class Треугольник : Геометрия
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;

        public Треугольник(ЦветФигуры цвет, double a, double b, double c)
            : base(цвет)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public override string Наименование => "Треугольник";

        public override double Периметр => _a + _b + _c;

        public override double Площадь
        {
            get
            {
                double p = Периметр / 2.0;
                return Math.Sqrt(Math.Max(0.0, p * (p - _a) * (p - _b) * (p - _c)));
            }
        }

        public override string Описание()
        {
            return base.Описание() + string.Format(
                " (стороны: {0:0.###}; {1:0.###}; {2:0.###})",
                _a,
                _b,
                _c);
        }
    }

    internal sealed class Program
    {
        private static void Main()
        {
            Геометрия[] фигуры = new Геометрия[]
            {
                new Ромб(ЦветФигуры.Красный, 4, 45),
                new Треугольник(ЦветФигуры.Синий, 3, 4, 5),
                new Квадрат(ЦветФигуры.Зелёный, 4.5),
                new Ромб(ЦветФигуры.Жёлтый, 5, 60),
                new Треугольник(ЦветФигуры.Красный, 7, 8, 9),
                new Квадрат(ЦветФигуры.Синий, 1)
            };

            Console.WriteLine("Лабораторная работа 8. Иерархия классов «Геометрия».");
            Console.WriteLine("Полиморфизм: массив типа базового класса, фактические типы — разные производные.");
            Console.WriteLine();

            for (int i = 0; i < фигуры.Length; i++)
            {
                Геометрия g = фигуры[i];
                Console.WriteLine(">>>>>>>>>>> " + g.GetType().Name);
                Console.WriteLine("Наименование (свойство): " + g.Наименование);
                Console.WriteLine("Площадь: " + g.Площадь.ToString("0.###"));
                Console.WriteLine("Периметр: " + g.Периметр.ToString("0.###"));
                Console.WriteLine("Описание (переопределённый метод): " + g.Описание());
                Console.WriteLine();
            }

            Console.WriteLine("Нажмите любую клавишу…");
            Console.ReadKey(true);
        }
    }
}
