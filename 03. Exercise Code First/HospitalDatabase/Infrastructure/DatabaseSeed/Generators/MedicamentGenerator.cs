namespace HospitalDatabase.Infrastructure.DatabaseSeed.Generators
{
    using Data;
    using Data.Models;

    public class MedicamentGenerator
    {
        public static void InitialMedicamentSeed(HospitalDbContext db)
        {
            var medicamentNames = new string[]
            {
                "Biseptol",
                "Ciclobenzaprina",
                "Curam",
                "Diclofenaco",
                "Disflatyl",
                "Duvadilan",
                "Efedrin",
                "Flanax",
                "Fluimucil",
                "Navidoxine",
                "Nistatin",
                "Olfen",
                "Pentrexyl",
                "Primolut Nor",
                "Primperan",
                "Propoven",
                "Reglin",
                "Terramicina Oftalmica",
                "Ultran",
                "Viartril-S",
            };

            for (int i = 0; i < medicamentNames.Length; i++)
            {
                db.Medicaments.Add(new Medicament() { Name = medicamentNames[i] });
            }

            db.SaveChanges();
        }

        public static void Generate(string medicamentName, HospitalDbContext context)
        {
            context.Medicaments.Add(new Medicament() { Name = medicamentName });
        }
    }
}