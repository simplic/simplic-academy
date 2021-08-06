using System.Collections.Generic;

namespace VehicleManager.DI
{
    public interface IVehicleService
    {
        void Save(Vehicle vehicle);

        IList<Vehicle> GetAll();
    }
}
