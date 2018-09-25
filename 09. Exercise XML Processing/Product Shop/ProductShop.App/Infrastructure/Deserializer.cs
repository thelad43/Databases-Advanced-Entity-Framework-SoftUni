namespace ProductShop.App.Infrastructure
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
        private const string UsersPathXml = "users.xml";
        private const string CategoriesPathXml = "categories.xml";
        private const string ProductsPathXml = "products.xml";
        private const int MinCategoriesCount = 1;
        private const int MaxCategoriesCount = 12;

        private readonly ProductShopDbContext db;

        public Deserializer(ProductShopDbContext db)
        {
            this.db = db;
        }

        public void ImportEntities()
        {
            this.ImportUsers();
            this.ImportCategories();
            this.ImportProducts();
        }

        private void ImportUsers()
        {
            var serializer = new XmlSerializer(typeof(UserModel[]), new XmlRootAttribute("users"));

            var deserializedUsers = (UserModel[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(UsersPathXml)));

            var users = deserializedUsers.AsQueryable().ProjectTo<User>().ToArray();

            this.db.AddRange(users);

            this.db.SaveChanges();
        }

        private void ImportCategories()
        {
            var serializer = new XmlSerializer(typeof(CategoryNameModel[]), new XmlRootAttribute("categories"));

            var deserializedCategories = (CategoryNameModel[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(CategoriesPathXml)));

            var categories = deserializedCategories.AsQueryable().ProjectTo<Category>().ToArray();

            this.db.AddRange(categories);

            this.db.SaveChanges();
        }

        private void ImportProducts()
        {
            var serializer = new XmlSerializer(typeof(ShortProductModel[]), new XmlRootAttribute("products"));

            var deserializedProducts = (ShortProductModel[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(ProductsPathXml)));

            var products = deserializedProducts.AsQueryable().ProjectTo<Product>().ToArray();

            var random = new Random();

            var users = this.db.Users.ToArray();

            User RandomUser() => users[random.Next(users.Length)];

            var categories = this.db.Categories.ToArray();

            Category RandomCategory() => categories[random.Next(categories.Length)];

            foreach (var product in products)
            {
                product.Seller = RandomUser();
                product.SellerId = product.Seller.Id;

                var categoriesCount = random.Next(MinCategoriesCount, MaxCategoriesCount);

                var addedCategories = new HashSet<Category>();

                for (var i = 0; i < categoriesCount; i++)
                {
                    var category = RandomCategory();

                    if (addedCategories.Contains(category))
                    {
                        continue;
                    }

                    product.Categories.Add(new CategoryProduct
                    {
                        Category = category
                    });

                    addedCategories.Add(category);
                }

                if (random.Next(3) == 0)
                {
                    product.Buyer = RandomUser();
                }
            }

            this.db.AddRange(products);

            this.db.SaveChanges();
        }
    }
}