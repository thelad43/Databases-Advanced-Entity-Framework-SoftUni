namespace CarDealer.App.Models
{
    using System.Collections.Generic;

    public class CarPartsModel
    {
        public ShortCarModel Car { get; set; }

        public List<ShortPartModel> Parts { get; set; }
    }
}