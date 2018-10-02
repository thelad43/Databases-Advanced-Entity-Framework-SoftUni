namespace ProductShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.DataConstants;

    public class Product
    {
        public int Id { get; set; }

        [MinLength(MinLengthName)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int? BuyerId { get; set; }

        public User Buyer { get; set; }

        public int SellerId { get; set; }

        public User Seller { get; set; }

        public List<CategoryProduct> Categories { get; set; } = new List<CategoryProduct>();
    }
}