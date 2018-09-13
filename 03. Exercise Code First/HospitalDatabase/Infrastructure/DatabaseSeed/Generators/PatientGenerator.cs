namespace HospitalDatabase.Infrastructure.DatabaseSeed.Generators
{
    using Data;
    using Data.Models;
    using System;
    using System.Linq;

    public class PatientGenerator
    {
        private static Random random = new Random();

        public static Patient NewPatient(HospitalDbContext db)
        {
            var firstName = NameGenerator.FirstName();
            var lastName = NameGenerator.LastName();

            var patient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = EmailGenerator.NewEmail(firstName + lastName),
                Address = AddressGenerator.NewAddress(),
            };

            patient.Visitations = GenerateVisitations(patient).ToList();
            patient.Diagnoses = GenerateDiagnoses(patient).ToList();

            return patient;
        }

        private static Diagnose[] GenerateDiagnoses(Patient patient)
        {
            var diagnoseNames = new string[]
            {
                "Limp Scurvy",
                "Fading Infection",
                "Cow Feet",
                "Incurable Ebola",
                "Snake Blight",
                "Spider Asthma",
                "Sinister Body",
                "Spine Diptheria",
                "Pygmy Decay",
                "King's Arthritis",
                "Desert Rash",
                "Deteriorating Salmonella",
                "Shadow Anthrax",
                "Hiccup Meningitis",
                "Fading Depression",
                "Lion Infertility",
                "Wolf Delirium",
                "Humming Measles",
                "Incurable Stomach",
                "Grave Heart",
            };

            var diagnoseCount = random.Next(1, 4);
            var diagnoses = new Diagnose[diagnoseCount];

            for (int i = 0; i < diagnoseCount; i++)
            {
                var diagnoseName = diagnoseNames[random.Next(diagnoseNames.Length)];

                var diagnose = new Diagnose()
                {
                    Name = diagnoseName,
                    Patient = patient,
                    Comments = "S0m34and0mC0mm3nt5"
                };

                diagnoses[i] = diagnose;
            }

            return diagnoses;
        }

        private static Visitation[] GenerateVisitations(Patient patient)
        {
            var visitationCount = random.Next(1, 5);

            var visitations = new Visitation[visitationCount];

            for (int i = 0; i < visitationCount; i++)
            {
                var visitationDate = RandomDay(2005);

                var visitation = new Visitation()
                {
                    Date = visitationDate,
                    Patient = patient,
                    Comments = "SomeRandomComments"
                };

                visitations[i] = visitation;
            }

            return visitations;
        }

        private static DateTime RandomDay(int startYear)
        {
            var start = new DateTime(startYear, 1, 1);

            var range = (DateTime.Today - start).Days;

            return start.AddDays(random.Next(range));
        }
    }
}