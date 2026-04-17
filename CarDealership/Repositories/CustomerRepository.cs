using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;

namespace CarDealership.Repositories
{
    public class CustomerRepository : IRepository<ICustomer> //ako ima greshka bi bila tuk
    {
        private readonly List<ICustomer> customers;
        public CustomerRepository()
        {
            customers = new List<ICustomer>();
        }
        public IReadOnlyCollection<ICustomer> Models => customers.AsReadOnly();

        public void Add(ICustomer model)
        {
            customers.Add(model);
        }

        public bool Exists(string text) => customers.Any(v => v.Name == text);

        public ICustomer Get(string text)
        {
            return customers.FirstOrDefault(v => v.Name == text);
        }

        public bool Remove(string text)
        {
            var client = Get(text);

            if (client != null)
            {
                customers.Remove(client);
                return true;
            }
            return false;

        }
    }
}
