namespace ProductShop.App.Models
{
    using System.Collections.Generic;

    public class ShortUserProductsModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<ShortProductModel> Products { get; set; }
    }
}