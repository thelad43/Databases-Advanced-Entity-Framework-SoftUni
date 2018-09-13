namespace HospitalDatabase.Infrastructure.DatabaseSeed
{
    using Data;
    using Generators;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class DatabaseInitializer
    {
        private static readonly Random random = new Random();

        public static void ResetDatabase()
        {
            using (var db = new HospitalDbContext())
            {
                db.Database.EnsureDeleted();

                db.Database.Migrate();

                InitialSeed(db);
            }
        }

        public static void InitialSeed(HospitalDbContext context)
        {
            SeedMedicaments(context);

            SeedPatients(context, 200);

            SeedPrescriptions(context);
        }

        private static void SeedMedicaments(HospitalDbContext context)
        {
            MedicamentGenerator.InitialMedicamentSeed(context);
        }

        private static void SeedPatients(HospitalDbContext context, int count)
        {
            for (int i = 0; i < count; i++)
            {
                context.Patients.Add(PatientGenerator.NewPatient(context));
            }

            context.SaveChanges();
        }

        private static void SeedPrescriptions(HospitalDbContext context)
        {
            PrescriptionGenerator.InitialPrescriptionSeed(context);
        }
    }
}