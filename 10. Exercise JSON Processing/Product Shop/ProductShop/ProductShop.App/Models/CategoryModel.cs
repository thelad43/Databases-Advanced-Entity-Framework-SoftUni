namespace ProductShop.App.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }

        public int Products { get; set; }

        public decimal AveragePrice { get; set; }

        public decimal TotalPriceSum { get; set; }
    }
}