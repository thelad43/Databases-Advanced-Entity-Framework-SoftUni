namespace CarDealer.App.Infrastructure
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class Serializer
    {
        private const string CarsFileName = "cars.xml";
        private const string FerrariCarsFileName = "ferrari-cars.xml";
        private const string LocalSuppliersFileName = "local-suppliers.xml";
        private const string CarsAndPartsFileName = "cars-and-parts.xml";
        private const string CustomersTotalSalesFileName = "customers-total-sales.xml";
        private const string SalesDiscountsFileName = "sales-discounts.xml";
        private const string Ferrari = "Ferrari";

        private const int Km = 2000000;

        private readonly CarDealerDbContext db;

        public Serializer(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void ExportCarsWithDistance()
        {
            var cars = this.db
                .Cars
                .Where(c => c.TravelledDistance >= Km)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .ProjectTo<ShortCarModel>()
                .ToList();

            var fileStream = CreateFileIfDoesNotExist(CarsFileName);

            var serializer = new XmlSerializer(typeof(ShortCarModel));

            foreach (var car in cars)
            {
                serializer.Serialize(fileStream, car);
            }
        }

        public void ExportCarsFromMakeFerrari()
        {
            var cars = this.db
                  .Cars
                  .Where(c => c.Make == Ferrari)
                  .OrderBy(c => c.Model)
                  .ThenByDescending(c => c.TravelledDistance)
                  .ProjectTo<CarIdModel>()
                  .ToList();

            var fileStream = CreateFileIfDoesNotExist(FerrariCarsFileName);

            var serializer = new XmlSerializer(typeof(CarIdModel));

            foreach (var car in cars)
            {
                serializer.Serialize(fileStream, car);
            }
        }

        public void ExportLocalSuppliers()
        {
            var suppliers = this.db
                  .Suppliers
                  .Where(s => !s.IsImporter)
                  .ProjectTo<LongSupplierModel>()
                  .ToList();

            var fileStream = CreateFileIfDoesNotExist(LocalSuppliersFileName);

            var serializer = new XmlSerializer(typeof(LongSupplierModel));

            foreach (var supplier in suppliers)
            {
                serializer.Serialize(fileStream, supplier);
            }
        }

        public void ExportCarswithTheirListOfParts()
        {
            var cars = this.db
                 .Cars
                 .ProjectTo<CarPartsModel>()
                 .ToList();

            var fileStream = CreateFileIfDoesNotExist(CarsAndPartsFileName);

            var serializer = new XmlSerializer(typeof(CarPartsModel));

            foreach (var car in cars)
            {
                serializer.Serialize(fileStream, car);
            }
        }

        public void ExportTotalSalesByCustomer()
        {
            var customers = this.db
                .Customers
                .Include(c => c.Sales)
                .ThenInclude(c => c.Car)
                .ThenInclude(c => c.Parts)
                .ThenInclude(pc => pc.Part)
                .ToList();

            var customerModels = customers
                .Select(c => new CustomerCarsModel
                {
                    Name = c.Name,
                    Cars = c.Sales.Count,
                    MoneySpent = c.Sales
                        .Sum(s => s.Car.Parts
                            .Sum(pc => pc.Part.Price) * (1M - (decimal)s.Discount - (c.IsYoungDriver ? 0.05M : 0M)))
                })
                .OrderByDescending(c => c.MoneySpent)
                .ThenByDescending(c => c.Cars)
                .ToList();

            var fileStream = CreateFileIfDoesNotExist(CustomersTotalSalesFileName);

            var serializer = new XmlSerializer(typeof(CustomerCarsModel));

            foreach (var customer in customerModels)
            {
                serializer.Serialize(fileStream, customer);
            }
        }

        public void ExportSaleswithAppliedDiscount()
        {
            var sales = this.db
                .Sales
                .Include(s => s.Customer)
                .Include(s => s.Car)
                .ThenInclude(c => c.Parts)
                .ThenInclude(pc => pc.Part)
                .ToList();

            var saleModels = new SaleModel[sales.Count];

            for (var i = 0; i < sales.Count; i++)
            {
                var sale = sales[i];

                var saleModel = Mapper.Map<SaleModel>(sale);

                saleModel.Price = sale.Car.Parts.Sum(pc => pc.Part.Price);

                saleModel.PriceWithDiscount = sale
                    .Car
                    .Parts
                    .Sum(pc => pc.Part.Price) * (1M - (decimal)sale.Discount - (sale.Customer.IsYoungDriver ? 0.05M : 0M));

                saleModels[i] = saleModel;
            }

            var fileStream = CreateFileIfDoesNotExist(SalesDiscountsFileName);

            var serializer = new XmlSerializer(typeof(SaleModel));

            foreach (var sale in saleModels)
            {
                serializer.Serialize(fileStream, sale);
            }
        }

        private FileStream CreateFileIfDoesNotExist(string fileName)
        {
            var directory = Directory.GetCurrentDirectory();
            var path = directory + "/" + fileName;

            var exists = File.Exists(path);

            if (exists)
            {
                return new FileStream(path, FileMode.Truncate);
            }

            return File.Create(fileName);
        }
    }
}