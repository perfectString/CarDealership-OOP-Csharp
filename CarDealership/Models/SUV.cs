using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public class SUV : Vehicle
    {
        private const double priceFactor = 1.20;
        public SUV(string model, double price) : base(model, price * priceFactor)
        {
        }
    }
}
