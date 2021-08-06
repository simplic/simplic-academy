using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager.DI
{
    public interface IVehicleRepository
    {
        void Save(Vehicle vehicle);
        
        IList<Vehicle> GetAll();
    }
}
