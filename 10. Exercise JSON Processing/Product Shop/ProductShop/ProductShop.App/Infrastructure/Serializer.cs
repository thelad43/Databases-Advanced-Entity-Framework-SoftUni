namespace ProductShop.App.Infrastructure
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;

    public class Serializer
    {
        private const string ProductsInRangeFileName = "products-in-range.json";
        private const string UsersSoldProductsFileName = "users-sold-products.json";
        private const string CategoriesByProductsFileName = "categories-by-products.json";
        private const string UsersAndProductsFileName = "users-and-products.json";

        private readonly ProductShopDbContext db;

        public Serializer(ProductShopDbContext db)
        {
            this.db = db;
        }

        public void ExportProductsInRange()
        {
            var products = this.db
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .ProjectTo<ProductModel>()
                .ToList();

            File.Create(ProductsInRangeFileName).Close();

            foreach (var product in products)
            {
                using (var writer = new StreamWriter(ProductsInRangeFileName, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(product));
                }
            }
        }

        public void ExportSuccessfullySoldProducts()
        {
            var users = this.db
                .Users
                .Where(u => u.SoldProducts.Any())
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<ShortUserProductsModel>()
                .ToList();

            File.Create(UsersSoldProductsFileName).Close();

            foreach (var user in users)
            {
                using (var writer = new StreamWriter(UsersSoldProductsFileName, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(user));
                }
            }
        }

        public void ExportCategoriesByProductsCount()
        {
            var categories = this.db
                .Categories
                .OrderBy(c => c.Products.Count)
                .ProjectTo<CategoryModel>()
                .ToList();

            File.Create(CategoriesByProductsFileName).Close();

            foreach (var category in categories)
            {
                using (var writer = new StreamWriter(CategoriesByProductsFileName, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(category));
                }
            }
        }

        public void ExportUsersAndProducts()
        {
            var users = this.db
                .Users
                .Where(u => u.SoldProducts.Any())
                .OrderByDescending(u => u.SoldProducts.Count)
                .ThenBy(u => u.LastName)
                .ProjectTo<UserProductsModel>()
                .ToList();

            File.Create(UsersAndProductsFileName).Close();

            foreach (var user in users)
            {
                using (var writer = new StreamWriter(UsersAndProductsFileName, true))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(user));
                }
            }
        }
    }
}