using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Lab21
{
    public partial class MainWindow : Window
    {
        private CarCollection carCollection;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            carCollection = new CarCollection();

            try
            {
                string filePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "cars.txt");

                var cars = CarDataReader.ReadFromFile(filePath);

                foreach (var car in cars)
                    carCollection.Add(car);

                lstCars.ItemsSource = carCollection.Cars;

                txtTotalCost.Text = carCollection.TotalCost.ToString("N2") + " р.";
                txtMinMileage.Text = carCollection.MinMileageInfo;
                txtCount.Text = carCollection.Count.ToString();

                if (carCollection.Count > 0)
                    lstCars.SelectedIndex = 0;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCars.SelectedItem is Car car)
            {
                txtModel.Text = car.Model;
                txtManufacturer.Text = car.Manufacturer;
                txtCost.Text = car.Cost.ToString("N2");
                txtMileage.Text = car.Mileage.ToString("N0");
            }
            else
            {
                txtModel.Text = "";
                txtManufacturer.Text = "";
                txtCost.Text = "";
                txtMileage.Text = "";
            }
        }
    }
}
