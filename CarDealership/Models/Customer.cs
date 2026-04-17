using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;

namespace CarDealership.Models
{
    public class Customer : ICustomer
    {
        private string name;
        private readonly List<string> purcheses;

        public Customer(string name)
        {
            this.Name = name;

            purcheses = new List<string>();
        }
        public string Name
        {
            get => name;
           private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameIsRequired);
                }
                name = value;
            }
        }

        public IReadOnlyCollection<string> Purchases => purcheses.AsReadOnly();

        public void BuyVehicle(string vehicleModel)
        {
            purcheses.Add(vehicleModel);
        }

        public override string ToString()
        {
            return $"{Name} - Purchases: {Purchases.Count}";
        }
    }
}
