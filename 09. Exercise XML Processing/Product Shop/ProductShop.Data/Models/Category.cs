namespace ProductShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.DataConstants;

    [Serializable()]
    public class Category
    {
        public int Id { get; set; }

        [MinLength(MinLengthName)]
        [MaxLength(MaxLengthCategoryName)]
        public string Name { get; set; }

        public List<CategoryProduct> Products { get; set; } = new List<CategoryProduct>();
    }
}