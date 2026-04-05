using System;
using System.Windows;

namespace Lab18
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int N = int.Parse(TextN.Text);
                int K = int.Parse(TextK.Text);
                double p = double.Parse(TextP.Text);
                double y = double.Parse(TextY.Text);

                if (N <= 0 || K <= 0)
                {
                    MessageBox.Show("N и K должны быть положительными целыми числами.",
                        "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double Z = 0;
                for (int i = 1; i <= N; i++)
                {
                    for (int j = 1; j <= K; j++)
                    {
                        Z += Math.Pow(p, i) * Math.Pow(y, j) / (i * j);
                    }
                }

                ResultPanel.Visibility = Visibility.Visible;
                ResultText.Text = "Z = " + Z.ToString("F6");
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте правильность введённых данных.\n" +
                    "N и K — целые числа, p и y — вещественные.",
                    "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Результат слишком велик для вычисления.\n" +
                    "Попробуйте уменьшить значения параметров.",
                    "Переполнение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
