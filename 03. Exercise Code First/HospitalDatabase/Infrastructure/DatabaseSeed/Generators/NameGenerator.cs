namespace HospitalDatabase.Infrastructure.DatabaseSeed.Generators
{
    using System;

    public class NameGenerator
    {
        private static readonly string[] firstNames =
            { "Petur", "Ivan", "Georgi", "Alexander", "Stefan", "Vladimir", "Svetoslav", "Kaloyan", "Mihail", "Stamat" };

        private static readonly string[] lastNames =
            { "Ivanov", "Georgiev", "Stefanov", "Alexandrov", "Petrov", "Stamatkov", };

        public static string FirstName() => GenerateName(firstNames);

        public static string LastName() => GenerateName(lastNames);

        private static string GenerateName(string[] names)
        {
            var random = new Random();

            var index = random.Next(0, names.Length);

            var name = names[index];

            return name;
        }
    }
}