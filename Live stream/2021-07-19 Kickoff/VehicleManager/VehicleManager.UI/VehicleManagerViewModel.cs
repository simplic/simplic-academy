using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;
using VehicleManager.DB;
using VehicleManager.DI;

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

        private string repositoryName = "sql";

        private IUnityContainer container;
        private IVehicleRepository vehicleRepository;

        public VehicleManagerViewModel(IUnityContainer container)
        {
            this.container = container;

            foreach (var repo in container.ResolveAll<IVehicleRepository>())
            {
                var name = container.Registrations.FirstOrDefault(x => x.MappedToType == repo.GetType());

                Repositories.Add(name.Name);
            }

            vehicleRepository = container.Resolve<IVehicleRepository>("sql");
            var dbVehicles = vehicleRepository.GetAll();

            foreach (var dbVehicle in dbVehicles)
            {
                vehicles.Add(new VehicleViewModel(dbVehicle));
            }

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

            SaveToDB = new RelayCommand(() =>
            {
                if (SelectedVehicle != null)
                {
                    // Save current vehicle to database
                    vehicleRepository.Save(SelectedVehicle.Model);
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

        public ICommand SaveToDB
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

        public ObservableCollection<string> Repositories { get; set; } = new ObservableCollection<string>();
        
        public string RepositoryName 
        {
            get => repositoryName;
            set 
            {
                repositoryName = value;

                vehicles.Clear();

                vehicleRepository = container.Resolve<IVehicleRepository>(repositoryName);
                var dbVehicles = vehicleRepository.GetAll();

                foreach (var dbVehicle in dbVehicles)
                {
                    vehicles.Add(new VehicleViewModel(dbVehicle));
                }
            }
        }
    }
}
