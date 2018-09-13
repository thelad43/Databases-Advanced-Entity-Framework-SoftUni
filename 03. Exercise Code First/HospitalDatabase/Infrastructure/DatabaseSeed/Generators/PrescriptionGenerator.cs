namespace HospitalDatabase.Infrastructure.DatabaseSeed.Generators
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PrescriptionGenerator
    {
        public static void InitialPrescriptionSeed(HospitalDbContext db)
        {
            var random = new Random();

            var allMedicamentIds = db.Medicaments.Select(d => d.Id).ToArray();

            var allPatientIds = db.Patients.Select(p => p.Id).ToArray();

            foreach (int patientId in allPatientIds)
            {
                var patientMedicamentsCount = random.Next(1, 4);

                var medicamentIds = new int[patientMedicamentsCount];

                for (int id = 0; id < patientMedicamentsCount; id++)
                {
                    var index = -1;

                    while (!allMedicamentIds.Contains(index) || medicamentIds.Contains(index))
                    {
                        index = random.Next(allMedicamentIds.Max());
                    }

                    medicamentIds[id] = index;
                }

                var medicaments = new List<PatientMedicament>();

                foreach (int medicamentId in medicamentIds)
                {
                    var medicament = new PatientMedicament()
                    {
                        PatientId = patientId,
                        MedicamentId = medicamentId
                    };

                    medicaments.Add(medicament);
                }

                db.Patients.Find(patientId).Medicaments = medicaments;
            }

            db.SaveChanges();
        }

        public static void NewPrescription(int patientId, int medicamentId, HospitalDbContext context)
        {
            var medicaments = new PatientMedicament()
            {
                PatientId = patientId,
                MedicamentId = medicamentId
            };

            context.Patients.Find(patientId).Medicaments.Add(medicaments);
            context.SaveChanges();
        }

        public static void NewPrescription(Patient patient, Medicament medicament, HospitalDbContext context)
        {
            var patientMedicament = new PatientMedicament()
            {
                Patient = patient,
                Medicament = medicament
            };

            patient.Medicaments.Add(patientMedicament);
            context.SaveChanges();
        }
    }
}