namespace HospitalDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Medicament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PatientMedicament> Patients { get; set; } = new List<PatientMedicament>();
    }
}