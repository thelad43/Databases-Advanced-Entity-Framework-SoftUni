namespace CarDealer.App.Infrastructure
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;

    public class Serializer
    {
        private const string OrderedCustomersFileName = "ordered-customers.json";
        private const string ToyotaCarsFileName = "toyota-cars.json";
        private const string LocalSuppliersFileName = "local-suppliers.json";
        private const string CarsAndPartsFileName = "cars-and-parts.json";
        private const string CustomersTotalSales = "customers-total-sales.json";
        private const string SalesDiscounts = "sales-discounts.json";
        private const string Toyota = "Toyota";

        private readonly CarDealerDbContext db;

        public Serializer(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void ExportOrderedCustomers()
        {
            var customers = this.db
                .Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => !c.IsYoungDriver)
                .ProjectTo<CustomerSalesModel>()
                .ToList();

            File.Create(OrderedCustomersFileName).Close();

            foreach (var customer in customers)
            {
                using (var writer = new StreamWriter(OrderedCustomersFileName, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(customer));
                }
            }
        }

        public void ExportCarsFromMakeToyota()
        {
            var cars = this.db
               .Cars
               .Where(c => c.Make == Toyota)
               .OrderBy(c => c.Model)
               .ThenByDescending(c => c.TravelledDistance)
               .ProjectTo<CarModel>()
               .ToList();

            File.Create(ToyotaCarsFileName).Close();

            foreach (var car in cars)
            {
                using (var writer = new StreamWriter(ToyotaCarsFileName, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(car));
                }
            }
        }

        public void ExportLocalSuppliers()
        {
            var suppliers = this.db
                .Suppliers
                .Where(s => !s.IsImporter)
                .ProjectTo<SupplierModel>()
                .ToList();

            File.Create(LocalSuppliersFileName).Close();

            foreach (var supplier in suppliers)
            {
                using (var writer = new StreamWriter(LocalSuppliersFileName, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(supplier));
                }
            }
        }

        public void ExportCarsWithTheirListOfParts()
        {
            var cars = this.db
                .Cars
                .ProjectTo<CarPartsModel>()
                .ToList();

            File.Create(CarsAndPartsFileName).Close();

            foreach (var car in cars)
            {
                using (var writer = new StreamWriter(CarsAndPartsFileName, true))
                {
                    var carAsObj = new
                    {
                        car.Make,
                        car.Model,
                        car.TravelledDistance,
                        car.Parts
                    };

                    writer.WriteLine(JsonConvert.SerializeObject(carAsObj));
                }
            }
        }

        public void ExportTotalSalesByCustomer()
        {
            var customers = this.db
                .Customers
                .Where(c => c.Sales.Any())
                .Include(c => c.Sales)
                .ThenInclude(c => c.Car)
                .ThenInclude(c => c.Parts)
                .ThenInclude(pc => pc.Part)
                .ToArray()
                .Select(c => new ShortCustomerSalesModel
                {
                    Name = c.Name,
                    Cars = c.Sales.Count,
                    MoneySpent = c
                        .Sales
                        .Sum(s => s
                            .Car
                            .Parts
                            .Sum(pc => pc.Part.Price) * (1M - (decimal)s.Discount - (c.IsYoungDriver ? 0.05M : 0M)))
                })
                .OrderByDescending(c => c.MoneySpent)
                .ThenByDescending(c => c.Cars)
                .ToList();

            File.Create(CustomersTotalSales).Close();

            foreach (var customer in customers)
            {
                using (var writer = new StreamWriter(CustomersTotalSales, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(customer));
                }
            }
        }

        public void ExportSalesWithAppliedDiscount()
        {
            var sales = this.db
                .Sales
                .Include(s => s.Customer)
                .Include(s => s.Car)
                .ThenInclude(c => c.Parts)
                .ThenInclude(pc => pc.Part)
                .ToArray()
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = (decimal)s.Discount + (s.Customer.IsYoungDriver ? 0.05M : 0M),
                    price = s.Car.Parts.Sum(pc => pc.Part.Price),
                    priceWithDiscount = s
                        .Car
                        .Parts
                        .Sum(pc => pc.Part.Price) * (1M - (decimal)s.Discount - (s.Customer.IsYoungDriver ? 0.05M : 0M))
                })
               .ToList();

            File.Create(SalesDiscounts).Close();

            foreach (var sale in sales)
            {
                using (var writer = new StreamWriter(SalesDiscounts, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(sale));
                }
            }
        }
    }
}