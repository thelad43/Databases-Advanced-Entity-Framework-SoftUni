namespace CarDealer.App.Models
{
    using System;
    using System.Collections.Generic;

    public class CustomerSalesModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public List<ShortSaleModel> Sales { get; set; }
    }
}