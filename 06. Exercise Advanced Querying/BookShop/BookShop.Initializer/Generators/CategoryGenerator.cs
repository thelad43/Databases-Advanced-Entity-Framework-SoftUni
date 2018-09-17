namespace BookShop.Initializer.Generators
{
    using Models;

    public class CategoryGenerator
    {
        public static Category[] CreateCategories()
        {
            var categoryNames = new string[]
            {
                "Science Fiction",
                "Drama",
                "Action",
                "Adventure",
                "Romance",
                "Mystery",
                "Horror",
                "Health",
                "Travel",
                "Children's",
                "Science",
                "History",
                "Poetry",
                "Comics",
                "Art",
                "Cookbooks",
                "Journals",
                "Biographies",
                "Fantasy",
            };

            var categoryCount = categoryNames.Length;

            var categories = new Category[categoryCount];

            for (int i = 0; i < categoryCount; i++)
            {
                var category = new Category()
                {
                    Name = categoryNames[i],
                };

                categories[i] = category;
            }

            return categories;
        }
    }
}