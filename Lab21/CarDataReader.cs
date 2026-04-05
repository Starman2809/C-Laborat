using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Lab21
{
    public static class CarDataReader
    {
        public static List<Car> ReadFromFile(string filePath)
        {
            var cars = new List<Car>();

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден: " + filePath);

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed))
                    continue;

                string[] parts = trimmed.Split(';');
                if (parts.Length < 4)
                    continue;

                string model = parts[0].Trim();
                string manufacturer = parts[1].Trim();
                double cost = double.Parse(parts[2].Trim(), CultureInfo.InvariantCulture);
                double mileage = double.Parse(parts[3].Trim(), CultureInfo.InvariantCulture);

                cars.Add(new Car(model, manufacturer, cost, mileage));
            }

            return cars;
        }
    }
}
