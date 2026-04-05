using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace LR11
{
    class ProductRecord
    {
        public string Supplier { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OverdueQuantity { get; set; }
    }

    class ProductProcessor
    {
        private readonly List<ProductRecord> _records = new List<ProductRecord>();

        public void ReadData(string fileName)
        {
            _records.Clear();

            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                string line;
                bool firstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.Length == 0)
                        continue;

                    if (firstLine && line.ToLower().Contains("поставщик"))
                    {
                        firstLine = false;
                        continue;
                    }

                    firstLine = false;

                    string[] parts = line.Split(new char[] { ';' });

                    if (parts.Length < 6)
                        continue;

                    ProductRecord record = new ProductRecord
                    {
                        Supplier = parts[0].Trim(),
                        Category = parts[1].Trim(),
                        Product = parts[2].Trim(),
                        Price = ParseDecimal(parts[3].Trim()),
                        Quantity = ParseInt(parts[4].Trim()),
                        OverdueQuantity = ParseInt(parts[5].Trim())
                    };

                    _records.Add(record);
                }
            }
        }

        public string BuildReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Лабораторная работа 11");
            sb.AppendLine("Обработка данных в файлах");
            sb.AppendLine();

            sb.AppendLine("1. Самый дорогой продукт для каждого поставщика:");
            var expensiveBySupplier = _records
                .GroupBy(r => r.Supplier)
                .OrderBy(g => g.Key);

            foreach (var group in expensiveBySupplier)
            {
                ProductRecord maxProduct = group
                    .OrderByDescending(r => r.Price)
                    .ThenBy(r => r.Product)
                    .First();

                sb.AppendLine($"{group.Key}: {maxProduct.Product} — {maxProduct.Price:F2}");
            }

            sb.AppendLine();

            sb.AppendLine("2. Товар с минимальным количеством в каждой категории:");
            var minByCategory = _records
                .GroupBy(r => r.Category)
                .OrderBy(g => g.Key);

            foreach (var group in minByCategory)
            {
                ProductRecord minProduct = group
                    .OrderBy(r => r.Quantity)
                    .ThenBy(r => r.Product)
                    .First();

                sb.AppendLine($"{group.Key}: {minProduct.Product} — {minProduct.Quantity}");
            }

            sb.AppendLine();

            decimal overdueValue = _records.Sum(r => r.Price * r.OverdueQuantity);
            sb.AppendLine("3. Объем просрочки в валюте:");
            sb.AppendLine(overdueValue.ToString("F2"));

            sb.AppendLine();

            sb.AppendLine("4. Средняя цена у каждого поставщика:");
            var avgBySupplier = _records
                .GroupBy(r => r.Supplier)
                .OrderBy(g => g.Key);

            foreach (var group in avgBySupplier)
            {
                decimal avgPrice = group.Average(r => r.Price);
                sb.AppendLine($"{group.Key}: {avgPrice:F2}");
            }

            return sb.ToString();
        }

        private decimal ParseDecimal(string value)
        {
            decimal result;

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("ru-RU"), out result))
                return result;

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            return 0m;
        }

        private int ParseInt(string value)
        {
            int result;

            if (int.TryParse(value, out result))
                return result;

            return 0;
        }
    }
}