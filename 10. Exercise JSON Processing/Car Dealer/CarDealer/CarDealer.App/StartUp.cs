namespace CarDealer.App
{
    using AutoMapper;
    using Data;
    using Infrastructure;
    using Infrastructure.Mapping;

    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new CarDealerProfile()));

            var db = new CarDealerDbContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var deserializer = new Deserializer(db);

            deserializer.ImportEntities();

            var serializer = new Serializer(db);

            serializer.ExportOrderedCustomers();
            serializer.ExportCarsFromMakeToyota();
            serializer.ExportLocalSuppliers();
            serializer.ExportCarsWithTheirListOfParts();
            serializer.ExportTotalSalesByCustomer();
            serializer.ExportSalesWithAppliedDiscount();
        }
    }
}