namespace BookShop.Services.Models
{
    using BookShop.Models;

    public class TitleEditionTypePriceModel : TitlePriceModel
    {
        public EditionType EditionType { get; set; }
    }
}