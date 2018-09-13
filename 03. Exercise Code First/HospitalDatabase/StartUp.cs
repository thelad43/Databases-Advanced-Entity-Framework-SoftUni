namespace HospitalDatabase
{
    using Data;
    using Infrastructure.DatabaseSeed;

    public class StartUp
    {
        public static void Main()
        {
            var db = new HospitalDbContext();
            DatabaseInitializer.InitialSeed(db);
        }
    }
}