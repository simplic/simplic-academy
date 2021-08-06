using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager
{
    public abstract class Vehicle
    {
        public virtual void Fill()
        {
            System.Console.Write("Manufacturer: ");
            Manufacturer = System.Console.ReadLine();

            System.Console.Write("Color_Edit: ");
            Color = System.Console.ReadLine();

            System.Console.Write("Fuel type: ");
            FuelType = System.Console.ReadLine();
        }

        /// <summary>
        /// Gets or sets the unique vehicle id. The id will be generated automatically
        /// </summary>
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Color { get; set; }
        public string FuelType { get; set; }
    }
}
