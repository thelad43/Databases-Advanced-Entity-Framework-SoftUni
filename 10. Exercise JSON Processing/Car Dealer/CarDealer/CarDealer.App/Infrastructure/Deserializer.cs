namespace CarDealer.App.Infrastructure
{
    using Data;
    using Data.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Deserializer
    {
        private const string CarsPath = "cars.json";
        private const string CustomersPath = "customers.json";
        private const string PartsPath = "parts.json";
        private const string SuppliersPath = "suppliers.json";
        private const int MinPartsCount = 10;
        private const int MaxPartsCount = 21;
        private const int MinSalesCount = 100;
        private const int MaxSalesCount = 501;

        private readonly Random random;
        private readonly CarDealerDbContext db;

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
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(File.ReadAllText(SuppliersPath));

            this.db.AddRange(suppliers);

            this.db.SaveChanges();
        }

        private void ImportParts()
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(File.ReadAllText(PartsPath));

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
            var cars = JsonConvert.DeserializeObject<Car[]>(File.ReadAllText(CarsPath));

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
            var customers = JsonConvert.DeserializeObject<Customer[]>(File.ReadAllText(CustomersPath));

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