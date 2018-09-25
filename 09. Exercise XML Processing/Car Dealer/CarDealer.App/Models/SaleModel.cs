namespace CarDealer.App.Models
{
    public class SaleModel
    {
        public ShortCarModel Car { get; set; }

        public string CustomerName { get; set; }

        public double Discount { get; set; }

        public decimal Price { get; set; }

        public decimal PriceWithDiscount { get; set; }
    }
}