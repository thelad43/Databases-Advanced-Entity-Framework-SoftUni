namespace HospitalDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Specialty { get; set; }

        public List<Visitation> Visitations { get; set; } = new List<Visitation>();
    }
}