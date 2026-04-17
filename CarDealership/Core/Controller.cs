using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Core.Contracts;
using CarDealership.Models;
using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;

namespace CarDealership.Core
{
    public class Controller : IController
    {
        private readonly Dealership dealership;
        private readonly string[] customerTypes = new[]
        {
            nameof(IndividualClient) ,
            nameof(LegalEntityCustomer)
        };
        private readonly string[] carTypes = new[]
        {
            nameof(SaloonCar) ,
            nameof(SUV) ,
            nameof(Truck)

        };
        private readonly string[] individualClientCarTypes = new[]
        {

            nameof(SaloonCar) ,
            nameof(SUV) 
        };
        private readonly string[] legalClientCarTypes = new[]
        {
            nameof(SUV) ,
            nameof(Truck)
        };

        public Controller()
        {
            dealership = new Dealership();
        }
        public string AddCustomer(string customerTypeName, string customerName)
        {
            if (!customerTypes.Contains(customerTypeName))
            {
                return string.Format(OutputMessages.InvalidType, customerTypeName);
            }

            if (dealership.Customers.Exists(customerName))
            {
                return string.Format(OutputMessages.CustomerAlreadyAdded, customerName);
            }

            ICustomer customer;
            if (customerTypeName == nameof(IndividualClient))
            {
                customer = new IndividualClient(customerName);
                dealership.Customers.Add(customer);
            }
            else if (customerTypeName == nameof(LegalEntityCustomer))
            {
                customer = new LegalEntityCustomer(customerName);
                dealership.Customers.Add(customer);
            }


            return string.Format(OutputMessages.CustomerAddedSuccessfully, customerName);
        }

        public string AddVehicle(string vehicleTypeName, string model, double price)
        {
            if (!carTypes.Contains(vehicleTypeName))
            {
                return string.Format(OutputMessages.InvalidType, vehicleTypeName);
            }

            if (dealership.Vehicles.Exists(model))
            {
                return string.Format(OutputMessages.VehicleAlreadyAdded, model);
            }

            double finalPrice = 0;
            IVehicle vehicle;
            if (vehicleTypeName == nameof(SUV))
            {
                vehicle = new SUV(model, price);
                dealership.Vehicles.Add(vehicle);
                finalPrice = vehicle.Price;
            }
            else if (vehicleTypeName == nameof(Truck))
            {
                vehicle = new Truck(model, price);
                dealership.Vehicles.Add(vehicle);
                finalPrice = vehicle.Price;
            }
            else if (vehicleTypeName == nameof(SaloonCar))
            {
                vehicle = new SaloonCar(model, price);
                dealership.Vehicles.Add(vehicle);
                finalPrice = vehicle.Price;

            }

            return string.Format(OutputMessages.VehicleAddedSuccessfully, vehicleTypeName, model, $"{finalPrice:F2}");
        }

        public string CustomerReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Customer Report:");

            foreach (ICustomer customer in dealership.Customers.Models.OrderBy(c => c.Name))
            {
                sb.AppendLine(customer.ToString());
                sb.AppendLine("-Models:");

                if (customer.Purchases.Count == 0)
                {
                    sb.AppendLine("--none");
                }
                else
                {
                    foreach (var car in customer.Purchases.OrderBy(p => p))
                    {
                        sb.AppendLine($"--{car}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string PurchaseVehicle(string vehicleTypeName, string customerName, double budget)
        {
            if (!dealership.Customers.Exists(customerName))
            {
                return string.Format(OutputMessages.CustomerNotFound, customerName);
            }

            if (!dealership.Vehicles.Models.Any(v => v.GetType().Name == vehicleTypeName))
            {
                return string.Format(OutputMessages.VehicleTypeNotFound, vehicleTypeName);
            }

            ICustomer customer = dealership.Customers.Get(customerName);
            if (customer is LegalEntityCustomer)
            {
                if (!legalClientCarTypes.Contains(vehicleTypeName))
                {
                    return string.Format(OutputMessages.CustomerNotEligibleToPurchaseVehicle, customerName, vehicleTypeName);
                }
            }
            else if (customer is IndividualClient) //checkni teq nz
            {
                if (!individualClientCarTypes.Contains(vehicleTypeName))
                {
                    return string.Format(OutputMessages.CustomerNotEligibleToPurchaseVehicle, customerName, vehicleTypeName);
                }
            }

            List<IVehicle> vehiclesFilteredPrice = dealership.Vehicles.Models.Where(v => v.GetType().Name == vehicleTypeName && v.Price <= budget).ToList();

            if (!vehiclesFilteredPrice.Any())
            {
                return string.Format(OutputMessages.BudgetIsNotEnough, customerName, vehicleTypeName);
            }
            IVehicle vehicle = vehiclesFilteredPrice.MaxBy(v => v.Price);
            customer.BuyVehicle(vehicle.Model);
            vehicle.SellVehicle(customerName);

            return string.Format(OutputMessages.VehiclePurchasedSuccessfully, customerName, vehicle.Model);
        }

        public string SalesReport(string vehicleTypeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{vehicleTypeName} Sales Report:");

            List<IVehicle> vehicles = dealership.Vehicles.Models
                .Where(v => v.GetType().Name==vehicleTypeName)
                .ToList();

            foreach (var car in vehicles.OrderBy(v => v.Model))
            {
                sb.AppendLine($"--{car.ToString()}");
            }
            sb.AppendLine($"-Total Purchases: {vehicles.Select(v=> v.SalesCount).Sum()}");

            return sb.ToString().TrimEnd();
        }
    }
}
