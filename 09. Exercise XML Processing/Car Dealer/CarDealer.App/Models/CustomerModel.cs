namespace CarDealer.App.Models
{
    using System;
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class CustomerModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "birth-date")]
        public DateTime BirthDate { get; set; }

        [XmlElement(ElementName = "is-young-driver")]
        public bool IsYoungDriver { get; set; }
    }
}