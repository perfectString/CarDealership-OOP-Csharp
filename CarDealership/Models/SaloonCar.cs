using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public class SaloonCar : Vehicle
    {
        private const double priceFactor = 1.10;
        public SaloonCar(string model, double price) : base(model, price * priceFactor) // * 1.10 v bazov konstr shte raboti na izpita
        {
        }
    }
}
