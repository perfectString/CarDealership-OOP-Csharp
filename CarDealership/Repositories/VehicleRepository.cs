using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;

namespace CarDealership.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private readonly List<IVehicle> vehicles;
        public VehicleRepository()
        {
            vehicles = new List<IVehicle>();
        }
        public IReadOnlyCollection<IVehicle> Models => vehicles.AsReadOnly();

        public void Add(IVehicle model)
        {
            vehicles.Add(model);
        }

        public bool Exists(string text) => vehicles.Any(v => v.Model == text);

        public IVehicle Get(string text)
        {
            return vehicles.FirstOrDefault(v => v.Model == text);
        }

        public bool Remove(string text)
        {
            var car = Get(text);

            if (car != null)
            {
                vehicles.Remove(car);
                return true;
            }
            return false;

        }
    }
}
