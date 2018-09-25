namespace ProductShop.App.Models
{
    using System.Xml.Serialization;

    [XmlType("user")]
    public class UserModel
    {
        [XmlAttribute("firstName")]
        public string FirstName { get; set; }

        [XmlAttribute("lastName")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }
    }
}