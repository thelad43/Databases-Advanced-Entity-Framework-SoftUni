namespace ProductShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.DataConstants;

    public class User
    {
        public int Id { get; set; }

        [MinLength(MinLengthName)]
        public string FirstName { get; set; }

        [MinLength(MinLengthName)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public List<Product> SoldProducts { get; set; } = new List<Product>();

        public List<Product> BoughtProducts { get; set; } = new List<Product>();
    }
}