using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using VehicleManager.DI;
using VehicleManager.DI.Data.DB;
using VehicleManager.DI.Json;
using VehicleManager.DI.Service;

namespace VehicleManager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var container = new UnityContainer();
            container.RegisterType<IVehicleRepository, VehicleRepositoryJson>("json");
            container.RegisterType<IVehicleRepository, VehicleRepository>("sql");
            container.RegisterType<IVehicleService, VehicleService>();

            DataContext = new VehicleManagerViewModel(container);
        }
    }
}
