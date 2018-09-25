namespace ProductShop.App.Infrastructure
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class Serializer
    {
        private const string ProductsInRangeFileName = "products-in-range.xml";
        private const string UsersSoldProductsFileName = "users-sold-products.xml";
        private const string CategoriesByProductsFileName = "categories-by-products.xml";
        private const string UsersAndProductsFileName = "users-and-products.xml";

        private readonly ProductShopDbContext db;

        public Serializer(ProductShopDbContext db)
        {
            this.db = db;
        }

        public void ExportProductsInRange()
        {
            var fileStream = CreateFileIfDoesNotExist(ProductsInRangeFileName);

            var products = this.db
                .Products
                .Where(p => p.Price >= 1000 && p.Price <= 2000)
                .Where(p => p.Buyer != null)
                .OrderBy(p => p.Price)
                .ProjectTo<ProductBuyerModel>()
                .ToList();

            var serializer = new XmlSerializer(typeof(ProductBuyerModel));

            foreach (var product in products)
            {
                serializer.Serialize(fileStream, product);
            }
        }

        public void ExportUsersSoldProducts()
        {
            var fileStream = CreateFileIfDoesNotExist(UsersSoldProductsFileName);

            var users = this.db
                .Users
                .Where(u => u.SoldProducts.Any())
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<UserProductsModel>()
                .ToList();

            var serializer = new XmlSerializer(typeof(UserProductsModel));

            foreach (var user in users)
            {
                serializer.Serialize(fileStream, user);
            }
        }

        public void ExportCategoriesByProductsCount()
        {
            var fileStream = CreateFileIfDoesNotExist(CategoriesByProductsFileName);

            var categories = this.db
                .Categories
                .OrderBy(c => c.Products.Count)
                .ProjectTo<CategoryInfoModel>()
                .ToList();

            var serializer = new XmlSerializer(typeof(CategoryInfoModel));

            foreach (var category in categories)
            {
                serializer.Serialize(fileStream, category);
            }
        }

        public void ExportUsersAndProducts()
        {
            var fileStream = CreateFileIfDoesNotExist(UsersAndProductsFileName);

            var users = this.db
                .Users
                .Where(u => u.SoldProducts.Any())
                .OrderByDescending(u => u.SoldProducts.Count)
                .ThenBy(u => u.LastName)
                .ProjectTo<UserProductsModel>()
                .ToList();

            var serializer = new XmlSerializer(typeof(UserProductsModel));

            foreach (var user in users)
            {
                serializer.Serialize(fileStream, user);
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