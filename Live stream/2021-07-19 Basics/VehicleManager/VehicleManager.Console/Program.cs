using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the simplic vehicle manager!");
            System.Console.WriteLine("You have the following options. Enter `new-car` / `edit-car` / `del-car`");
            System.Console.WriteLine("Enter `exit` to leave the application");

            var vehicleNumber = 10;
            var command = System.Console.ReadLine();

            var vehicles = new List<Vehicle>();

            while (command != "exit")
            {
                if (command == "new-car" || command == "new-tu")
                {
                    Vehicle vehicle = null;

                    if (command.Contains("car"))
                        vehicle = new Car();
                    else
                        vehicle = new TractorUnit();
                    
                    vehicle.Id = vehicleNumber;

                    // Increment
                    vehicleNumber++;

                    vehicle.Fill();

                    vehicles.Add(vehicle);

                    System.Console.WriteLine($"You vehicle was created successfully {vehicle.Id}");
                }
                else if (command == "edit-car")
                {
                    ExecutionAction action = (v) =>
                    {
                        v.Fill();
                    };

                    SearchAndExecute(vehicles, action);
                }
                else if (command == "del-car")
                {
                    ExecutionAction action = (v) =>
                    {
                        vehicles.Remove(v);
                    };

                    SearchAndExecute(vehicles, action);
                }
                else if (command == "print-car")
                {
                    ExecutionAction action = (v) =>
                    {
                        System.Console.WriteLine($"Yay, we are printing something: {v.Manufacturer}");
                    };

                    SearchAndExecute(vehicles, action);
                }
                else
                {
                    System.Console.WriteLine($"Invalid command {command}. Please use `new-car` / `edit-car` / `del-car`");
                }

                System.Console.WriteLine("You vehicle list:");
                foreach (var vehicle in vehicles)
                {
                    System.Console.WriteLine($" > {vehicle.Id} / {vehicle.Manufacturer} / {vehicle.Color} / {vehicle.FuelType}");
                }

                command = System.Console.ReadLine();
            }
        }

        private delegate void ExecutionAction(Vehicle vehicle);

        private static void SearchAndExecute(IList<Vehicle> vehicles, ExecutionAction action)
        {
            System.Console.Write("Please enter vehicle id:");
            var vehicleInputId = System.Console.ReadLine();

            if (int.TryParse(vehicleInputId, out int vehicleId))
            {
                // Linq
                Vehicle existingVehicle = vehicles.FirstOrDefault(x => x.Id == vehicleId);

                if (existingVehicle == null)
                {
                    System.Console.WriteLine($"Invalid input, no vehicle found with id {vehicleInputId}");
                }
                else
                {
                    action(existingVehicle);
                }
            }
        }
    }
}
