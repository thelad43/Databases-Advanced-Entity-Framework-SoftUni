namespace ProductShop.App.Models
{
    using System.Xml.Serialization;

    [XmlType("product")]
    public class ShortProductModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}