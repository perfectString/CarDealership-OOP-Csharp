using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Contracts;
using CarDealership.Repositories;
using CarDealership.Repositories.Contracts;

namespace CarDealership.Models
{
    public class Dealership : IDealership
    {
        private readonly IRepository<ICustomer> customerRepository;
        private readonly IRepository<IVehicle> vehicleRepository;

        public Dealership()
        {
            vehicleRepository = new VehicleRepository();
            customerRepository = new CustomerRepository();
        }
        public IRepository<IVehicle> Vehicles => vehicleRepository;

        public IRepository<ICustomer> Customers => customerRepository;
    }
}
