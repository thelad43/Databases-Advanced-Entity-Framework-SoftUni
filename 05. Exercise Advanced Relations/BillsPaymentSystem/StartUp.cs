namespace BillsPaymentSystem.App
{
    using Data;
    using Data.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Implementations;
    using System;

    public class StartUp
    {
        private static IServiceProvider serviceProvider;

        public static void Main()
        {
            serviceProvider = ConfigureServices();

            serviceProvider.GetService<IDbSeeder>().SeedData();

            var users = serviceProvider.GetService<IUserService>();

            const int id = 15;
            var user = users.ByIdWithPaymentMethods(id);

            Console.WriteLine($"User: {user.Name}");
            Console.WriteLine("Bank Accounts:");

            foreach (var bankAccount in user.BankAccounts)
            {
                Console.WriteLine($"--ID: {bankAccount.Id}");
                Console.WriteLine($"-- - Balance: {bankAccount.Balance}");
                Console.WriteLine($"-- - Bank: {bankAccount.BankName}");
                Console.WriteLine($"---SWIFT: {bankAccount.SwiftCode}");
            }

            Console.WriteLine("Credit Cards:");

            foreach (var creditCard in user.CreditCards)
            {
                Console.WriteLine($"--ID: {creditCard.Id}");
                Console.WriteLine($"-- - Limit: {creditCard.Limit:F2}");
                Console.WriteLine($"-- - Limit Left:: {creditCard.LimitLeft:F2}");
                Console.WriteLine($"-- - Money Owed: {creditCard.MoneyOwed}");
                Console.WriteLine($"-- - Expiration Date: {creditCard.ExpirationDate.ToShortDateString()}");
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<BillsPaymentDbContext>(options =>
                options.UseSqlServer(BillsPaymentDbConfiguration.ConnectionString));

            services.AddTransient<IUserService, UserService>();

            services.AddSingleton<IDbSeeder, DbSeeder>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}