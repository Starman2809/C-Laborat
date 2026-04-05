using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Lab21
{
    public class CarCollection : INotifyPropertyChanged
    {
        private ObservableCollection<Car> cars;

        public CarCollection()
        {
            cars = new ObservableCollection<Car>();
        }

        public ObservableCollection<Car> Cars
        {
            get { return cars; }
        }

        public void Add(Car car)
        {
            cars.Add(car);
            OnPropertyChanged("TotalCost");
            OnPropertyChanged("MinMileageCar");
            OnPropertyChanged("MinMileageInfo");
            OnPropertyChanged("Count");
        }

        public double TotalCost
        {
            get { return cars.Sum(c => c.Cost); }
        }

        public Car MinMileageCar
        {
            get
            {
                if (cars.Count == 0) return null;
                return cars.OrderBy(c => c.Mileage).First();
            }
        }

        public string MinMileageInfo
        {
            get
            {
                var car = MinMileageCar;
                if (car == null) return "—";
                return string.Format("{0} ({1}), пробег: {2:N0} км",
                    car.Model, car.Manufacturer, car.Mileage);
            }
        }

        public int Count
        {
            get { return cars.Count; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
