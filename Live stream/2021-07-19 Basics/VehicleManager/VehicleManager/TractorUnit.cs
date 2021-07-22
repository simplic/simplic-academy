using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager
{
    public class TractorUnit : Vehicle
    {
        public override void Fill()
        {
            base.Fill();

            System.Console.Write("Twin-Tires (1 = yes, 0 = no): ");
            HasTwinTires = System.Console.ReadLine() == "1";
        }

        public bool HasTwinTires { get; set; }
    }
}
