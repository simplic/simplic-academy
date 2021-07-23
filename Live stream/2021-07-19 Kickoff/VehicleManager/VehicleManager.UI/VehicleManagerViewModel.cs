using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VehicleManager.UI
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action execute;

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }

    public class VehicleManagerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string companyName;
        private ObservableCollection<VehicleViewModel> vehicles = new ObservableCollection<VehicleViewModel>();
        private VehicleViewModel selectedVehicle;

        public VehicleManagerViewModel()
        {
            AddCarCommand = new RelayCommand(() => 
            {
                this.Vehicles.Add(new VehicleViewModel
                {
                    Manufacturer = "VW",
                    Color = "Red",
                    FuelType = "Diesel"
                });
            });

            RemoveCarCommand = new RelayCommand(() => 
            {
                if (SelectedVehicle != null)
                {
                    var result = MessageBox.Show($"Are you sure you want to delete {SelectedVehicle.Manufacturer}", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                        Vehicles.Remove(SelectedVehicle);
                }
                else
                {
                    MessageBox.Show("No vehicle selected", "Wrong selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CompanyName)));

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(WindowTitle)));
            }
        }

        public ICommand AddCarCommand
        {
            get;
            set;
        }

        public ICommand RemoveCarCommand
        {
            get;
            set;
        }

        public VehicleViewModel SelectedVehicle
        {
            get => selectedVehicle;
            set 
            {
                selectedVehicle = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedVehicle)));
            }
        }

        public string WindowTitle
        {
            get => $"Vehicle manager {CompanyName}";
        }

        public ObservableCollection<VehicleViewModel> Vehicles
        {
            get => vehicles;
        }
    }
}
