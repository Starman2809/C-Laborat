using System.ComponentModel;

namespace Lab21
{
    public class Car : INotifyPropertyChanged
    {
        private string model;
        private string manufacturer;
        private double cost;
        private double mileage;

        public Car(string model, string manufacturer, double cost, double mileage)
        {
            this.model = model;
            this.manufacturer = manufacturer;
            this.cost = cost;
            this.mileage = mileage;
        }

        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set
            {
                manufacturer = value;
                OnPropertyChanged("Manufacturer");
            }
        }

        public double Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged("Cost");
            }
        }

        public double Mileage
        {
            get { return mileage; }
            set
            {
                mileage = value;
                OnPropertyChanged("Mileage");
            }
        }

        public override string ToString()
        {
            return string.Format("{0} : {1} ({2:N2} р., {3:N0} км)",
                Model, Manufacturer, Cost, Mileage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
