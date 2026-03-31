using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_One
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА 1. РАЗРАБОТКА КОНСОЛЬНОГО ПРИЛОЖЕНИЯ");
            Console.WriteLine("ФИО студента: Щетинин К.Н.");
            Console.WriteLine("Группа и шифр специальности: ИДПО_ИСИТ-З-У-24/1");
            Console.WriteLine("Дата рождения: 28.09.2004");
            Console.WriteLine("Населенный пункт постоянного места жительства: Буденновск");
            Console.WriteLine("Любимый предмет в школе: перемена");
            Console.WriteLine("Увлечения, хобби, интересы: Сижу дома");
            VariantTask.Run();

            Console.ReadKey();
        }
    }
}
