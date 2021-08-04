using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager.DI.Json
{
    public class VehicleRepositoryJson : IVehicleRepository
    {
        private int GetNextId()
        {
            return Directory.GetFiles(Environment.CurrentDirectory, "*.json").Count() + 1;
        }

        public void Save(Vehicle vehicle)
        {
            if (vehicle.Id == 0)
                vehicle.Id = GetNextId();

            var json = JsonConvert.SerializeObject(vehicle);
            File.WriteAllText($"{vehicle.Id}.json", json);
        }

        public IList<Vehicle> GetAll()
        {
            var result = new List<Vehicle>();

            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, "*.json"))
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var vehicle = JsonConvert.DeserializeObject<Car>(json);

                    result.Add(vehicle);
                }
                catch (Exception)
                {
                    /* swallow */
                }
            }

            return result;
        }
    }
}
