namespace HospitalDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Patient
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool HasInsurance { get; set; }

        public List<Diagnose> Diagnoses { get; set; } = new List<Diagnose>();

        public List<Visitation> Visitations { get; set; } = new List<Visitation>();

        public List<PatientMedicament> Medicaments { get; set; } = new List<PatientMedicament>();
    }
}