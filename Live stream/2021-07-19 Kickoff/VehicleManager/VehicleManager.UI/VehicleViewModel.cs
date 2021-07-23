using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager.UI
{
    public class VehicleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Vehicle vehicle;

        public VehicleViewModel()
        {
            vehicle = new Car();
        }

        public int Id { get; set; }
        public string Manufacturer 
        {
            get => vehicle.Manufacturer;
            set
            {
                vehicle.Manufacturer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Manufacturer)));
            }
        }

        public string Color 
        {
            get => vehicle.Color;
            set
            {
                vehicle.Color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
            }
        }

        public string FuelType 
        {
            get => vehicle.FuelType;
            set
            {
                vehicle.FuelType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FuelType)));
            }
        }
    }
}
