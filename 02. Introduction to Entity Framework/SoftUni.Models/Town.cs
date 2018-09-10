namespace SoftUni.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public int TownId { get; set; }

        public string Name { get; set; }

        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}