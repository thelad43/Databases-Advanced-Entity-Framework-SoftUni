namespace ProductShop.App.Infrastructure
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
        private const string UsersPathJson = "users.json";
        private const string CategoriesPathJson = "categories.json";
        private const string ProductsPathJson = "products.json";
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
            var users = JsonConvert.DeserializeObject<User[]>(File.ReadAllText(UsersPathJson));

            this.db.AddRange(users);

            this.db.SaveChanges();
        }

        private void ImportCategories()
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(File.ReadAllText(CategoriesPathJson));

            this.db.AddRange(categories);

            this.db.SaveChanges();
        }

        private void ImportProducts()
        {
            var products = JsonConvert.DeserializeObject<Product[]>(File.ReadAllText(ProductsPathJson));

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