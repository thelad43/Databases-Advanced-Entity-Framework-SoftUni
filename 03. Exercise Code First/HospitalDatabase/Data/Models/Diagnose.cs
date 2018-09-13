namespace HospitalDatabase.Data.Models
{
    public class Diagnose
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comments { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}