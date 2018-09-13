namespace Sales.Data.Models
{
    using System.Collections.Generic;

    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Sale> Sales { get; set; } = new List<Sale>();
    }
}