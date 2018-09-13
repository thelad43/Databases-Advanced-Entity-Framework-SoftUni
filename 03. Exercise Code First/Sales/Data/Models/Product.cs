﻿namespace Sales.Data.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public List<Sale> Sales { get; set; } = new List<Sale>();
    }
}