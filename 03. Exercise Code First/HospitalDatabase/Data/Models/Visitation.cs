﻿namespace HospitalDatabase.Data.Models
{
    using System;

    public class Visitation
    {
        public int Id { get; set; }

        public string Comments { get; set; }

        public DateTime Date { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public int? DoctorId { get; set; }

        public Doctor Doctor { get; set; }
    }
}