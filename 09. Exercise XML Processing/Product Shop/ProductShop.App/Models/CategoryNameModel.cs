namespace ProductShop.App.Models
{
    using System.Xml.Serialization;

    [XmlType("category")]
    public class CategoryNameModel
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}