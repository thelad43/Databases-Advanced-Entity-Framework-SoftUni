namespace CarDealer.App.Models
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ShortCarModel
    {
        [XmlElement(ElementName = "make")]
        public string Make { get; set; }

        [XmlElement(ElementName = "model")]
        public string Model { get; set; }

        [XmlElement(ElementName = "travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}