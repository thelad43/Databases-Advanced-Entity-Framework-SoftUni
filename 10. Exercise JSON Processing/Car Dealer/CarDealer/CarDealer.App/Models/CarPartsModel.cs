namespace CarDealer.App.Models
{
    using System.Collections.Generic;

    public class CarPartsModel : ShortCarModel
    {
        public List<PartModel> Parts { get; set; }
    }
}