namespace CarDealer.App.Infrastructure
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string CarsPathXml = "cars.xml";
        private const string CustomersPathXml = "customers.xml";
        private const string PartsPathXml = "parts.xml";
        private const string SuppliersPathXml = "suppliers.xml";
        private const int MinPartsCount = 10;
        private const int MaxPartsCount = 21;
        private const int MinSalesCount = 100;
        private const int MaxSalesCount = 501;

        private readonly CarDealerDbContext db;
        private readonly Random random;

        public Deserializer(CarDealerDbContext db)
        {
            this.db = db;
            this.random = new Random();
        }

        public void ImportEntities()
        {
            this.ImportSuppliers();
            this.ImportParts();
            this.ImportCars();
            this.ImportCustomers();
            this.ImportSales();
        }

        private void ImportSuppliers()
        {
            var serializer = new XmlSerializer(typeof(ShortSupplierModel[]), new XmlRootAttribute("suppliers"));

            var deserializedSuppliers = (ShortSupplierModel[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(SuppliersPathXml)));

            var suppliers = deserializedSuppliers.AsQueryable().ProjectTo<Supplier>().ToArray();

            this.db.AddRange(suppliers);

            this.db.SaveChanges();
        }

        private void ImportParts()
        {
            var serializer = new XmlSerializer(typeof(LongPartModel[]), new XmlRootAttribute("parts"));

            var deserializedParts = (LongPartModel[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(PartsPathXml)));

            var parts = deserializedParts.AsQueryable().ProjectTo<Part>().ToArray();

            var suppliers = this.db.Suppliers.ToArray();

            foreach (var part in parts)
            {
                var supplier = suppliers[this.random.Next(suppliers.Length)];
                part.Supplier = supplier;
                part.SupplierId = supplier.Id;
            }

            this.db.AddRange(parts);

            this.db.SaveChanges();
        }

        private void ImportCars()
        {
            var serializer = new XmlSerializer(typeof(ShortCarModel[]), new XmlRootAttribute("cars"));

            var deserializedCars = (ShortCarModel[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(CarsPathXml)));

            var cars = deserializedCars.AsQueryable().ProjectTo<Car>().ToArray();

            var parts = this.db.Parts.ToArray();

            foreach (var car in cars)
            {
                var containedParts = new HashSet<Part>();

                for (var i = 0; i < this.random.Next(MinPartsCount, MaxPartsCount); i++)
                {
                    var part = parts[this.random.Next(parts.Length)];

                    if (containedParts.Contains(part))
                    {
                        while (containedParts.Contains(part))
                        {
                            part = parts[this.random.Next(parts.Length)];
                        }
                    }

                    containedParts.Add(part);

                    var partCar = new PartCar
                    {
                        Car = car,
                        CarId = car.Id,
                        Part = part,
                        PartId = part.Id
                    };

                    this.db.Add(partCar);

                    car.Parts.Add(partCar);
                }
            }

            this.db.AddRange(cars);

            this.db.SaveChanges();
        }

        private void ImportCustomers()
        {
            var serializer = new XmlSerializer(typeof(CustomerModel[]), new XmlRootAttribute("customers"));

            var deserializedCustomers = (CustomerModel[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(CustomersPathXml)));

            var customers = deserializedCustomers.AsQueryable().ProjectTo<Customer>().ToArray();

            this.db.AddRange(customers);

            this.db.SaveChanges();
        }

        private void ImportSales()
        {
            var cars = this.db.Cars.ToArray();
            var customers = this.db.Customers.ToArray();
            var discounts = new[] { 0D, 0.05D, 0.1D, 0.15D, 0.2D, 0.3D, 0.4D, 0.5D };

            var sales = new Sale[this.random.Next(MinSalesCount, MaxSalesCount)];

            for (var i = 0; i < sales.Length; i++)
            {
                sales[i] = new Sale
                {
                    Car = cars[this.random.Next(cars.Length)],
                    Customer = customers[this.random.Next(customers.Length)],
                    Discount = discounts[this.random.Next(discounts.Length)]
                };
            }

            this.db.AddRange(sales);
            this.db.SaveChanges();
        }
    }
}