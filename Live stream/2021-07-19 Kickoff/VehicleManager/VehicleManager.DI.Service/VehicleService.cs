using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace VehicleManager.DI.Service
{
    public class VehicleService : IVehicleService
    {
        private IVehicleRepository repository;

        public VehicleService([Dependency("sql")] IVehicleRepository repository)
        {
            this.repository = repository;
        }

        public IList<Vehicle> GetAll()
        {
            return repository.GetAll();
        }

        public void Save(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.Manufacturer))
            {
                throw new Exception("Manufacturer must not be null or whitespace");
            }

            repository.Save(vehicle);
        }
    }
}
