namespace ProductShop.App
{
    using AutoMapper;
    using Data;
    using Infrastructure;
    using ProductShop.App.Infrastructure.Mapping;

    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new ProductShopProfile()));

            var db = new ProductShopDbContext();

            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            //var deserializer = new Deserializer(db);

            //deserializer.ImportEntities();

            var serializer = new Serializer(db);

            //serializer.ExportProductsInRange();

            //serializer.ExportSuccessfullySoldProducts();

            //serializer.ExportCategoriesByProductsCount();

            serializer.ExportUsersAndProducts();
        }
    }
}