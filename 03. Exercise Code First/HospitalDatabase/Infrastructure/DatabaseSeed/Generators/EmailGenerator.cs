namespace HospitalDatabase.Infrastructure.DatabaseSeed.Generators
{
    using System;

    public class EmailGenerator
    {
        private static Random rnd = new Random();

        private static string[] domains =
            { "mail.bg", "abv.bg", "gmail.com", "hotmail.com", "softuni.bg", "students.softuni.bg" };

        public static string NewEmail(string name)
        {
            var domain = domains[rnd.Next(domains.Length)];
            var number = rnd.Next(1, 2000);

            return $"{name.ToLower()}{number}@{domain}";
        }
    }
}