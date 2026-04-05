using System;
using System.IO;
using System.Text;

namespace LR11
{
    class Lab11
    {
        static void Main(string[] args)
        {
            string inputFile = "input.txt";
            string outputFile = "output.txt";

            ProductProcessor processor = new ProductProcessor();
            processor.ReadData(inputFile);

            string report = processor.BuildReport();

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(report);

            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                writer.Write(report);
            }

            Console.ReadKey();
        }
    }
}