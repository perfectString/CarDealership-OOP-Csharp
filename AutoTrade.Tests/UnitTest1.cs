using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;

namespace AutoTrade.Tests
{
    [TestFixture]
    public class DealerShopTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ConstructorWorksAndCapacityIsRight()
        {
            DealerShop shop = new(3);

            Assert.AreEqual(3, shop.Capacity);
            Assert.IsNotNull(shop.Vehicles);
            Assert.AreEqual(0, shop.Vehicles.Count);
        }

        [TestCase(0), TestCase(-20)]
        public void ConstructorThrowsExceptionIfCapacityIsInvalid(int invalid)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => { new DealerShop(invalid); });
            Assert.AreEqual(ex.Message, "Capacity must be a positive value.");
        }

        [Test]
        public void AddVehicleWorksIfItsNotFull()
        {
            DealerShop shop = new(2);
            Vehicle vehicle = new("Mazda", "c5", 1999);
            string actual = shop.AddVehicle(vehicle);

            Assert.AreEqual(shop.Vehicles.Count, 1);
            Assert.AreEqual("Added 1999 Mazda c5", actual);
        }

        [Test]
        public void AddVehicleDoesntWorksIfIsFull()
        {
            DealerShop shop = new(1);
            Vehicle vehicle = new("Mazda", "c5", 1999);
            string actual = shop.AddVehicle(vehicle);

            Assert.AreEqual(shop.Vehicles.Count, 1);
            Assert.Throws<InvalidOperationException>(() => {
                Vehicle invalid = new("Audi", "a5", 2009); ;
                shop.AddVehicle(invalid);
            });

        }

        [Test]
        public void SellReturnsTrueIfFound()
        {
            DealerShop shop = new(1);
            Vehicle vehicle = new("Mazda", "c5", 1999);
            shop.AddVehicle(vehicle);

            bool actual = shop.SellVehicle(vehicle);

            Assert.True(actual);
           
           

        }

        [Test]
        public void SellReturnsFalseIfNotFound()
        {
            DealerShop shop = new(1);
            Vehicle vehicle = new("Mazda", "c5", 1999);

            bool actual = shop.SellVehicle(vehicle);

            Assert.False(actual);

        }
        [Test]
        public void InventoryReportShouldWork()
        {
            DealerShop shop = new(2);
            Vehicle vehicle = new("Mazda", "c5", 1999);
            shop.AddVehicle(vehicle);

            string actualReport = shop.InventoryReport();

            StringBuilder expectedReport = new StringBuilder();
            expectedReport.AppendLine("Inventory Report");
            expectedReport.AppendLine($"Capacity: {2}");
            expectedReport.AppendLine($"Vehicles: {1}");
            expectedReport.AppendLine($"1999 Mazda c5");

            Assert.AreEqual( expectedReport.ToString().TrimEnd(), actualReport );
        }
    }
}

