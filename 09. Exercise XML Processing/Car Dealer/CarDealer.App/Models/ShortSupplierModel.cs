namespace CarDealer.App.Models
{
    using System.Xml.Serialization;

    [XmlType("supplier")]
    public class ShortSupplierModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("is-importer")]
        public bool IsImporter { get; set; }
    }
}