using Dapper;
using Sap.Data.SQLAnywhere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager.DB
{
    public static class VehicleDBManager
    {
        private static SAConnection GetConnection()
        {
            var connection = new SAConnection("server=academy;dbn=simplic;uid=dba;pwd=sql;links=tcpip");
            connection.Open();

            return connection;
        }

        private static int GetNextVehicleId()
        {
            using (var connection = GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT GET_IDENTITY('Vehicle')";

                var value = (ulong)command.ExecuteScalar();

                return (int)value;
            }
        }

        public static void Save(Vehicle vehicle)
        {
            using (var connection = GetConnection())
            {
                var command = connection.CreateCommand();

                command.Parameters.Add("manufacturer", vehicle.Manufacturer);
                command.Parameters.Add("color", vehicle.Color);
                command.Parameters.Add("fuelType", vehicle.FuelType);

                if (vehicle.Id == 0)
                {
                    vehicle.Id = GetNextVehicleId();
                    command.Parameters.Add("id", vehicle.Id);

                    command.CommandText = $"INSERT INTO Vehicle (Id, Manufacturer, Color, FuelType) VALUES (:id, :manufacturer, :color, :fuelType);";
                }
                else
                {
                    command.Parameters.Add("id", vehicle.Id);
                    command.CommandText = "UPDATE Vehicle SET Manufacturer = :manufacturer, Color = :color, FuelType = :fuelType WHERE Id = :id";
                }

                command.ExecuteNonQuery();
            }
        }

        public static IList<Vehicle> GetAllUsingDapper()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Car>("SELECT * FROM Vehicle").OfType<Vehicle>().ToList();
            }
        }

        public static IList<Vehicle> GetAll()
        {
            var vehicles = new List<Vehicle>();

            var connection = GetConnection();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Vehicle";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var vehicle = new Car();

                // Fill vehicles
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);

                    switch (columnName)
                    {
                        case "Id":
                            vehicle.Id = reader.GetInt32(i);
                            break;
                        case "Manufacturer":
                            vehicle.Manufacturer = reader.GetString(i);
                            break;
                        case "Color":
                            vehicle.Color = reader.GetString(i);
                            break;
                        case "FuelType":
                            vehicle.FuelType = reader.GetString(i);
                            break;
                    }
                }

                vehicles.Add(vehicle);
            }

            connection.Close();

            return vehicles;
        }
    }
}
