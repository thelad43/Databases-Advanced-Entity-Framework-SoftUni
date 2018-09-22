namespace BusTicketsSystem.App
{
    using AutoMapper;
    using Core;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Implementations;
    using System;
    using static Data.Configurations.BusTicketsSystemDbConfiguration;

    public class StartUp
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();

            serviceProvider.GetService<IDatabaseInitializerService>().InitializeDatabase();

            serviceProvider.GetService<IDatabaseSeederService>().SeedData();

            var commandParser = new CommandParser(serviceProvider);

            var engine = new Engine(commandParser);

            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<BusTicketsSystemDbContext>(options =>
                options.UseSqlServer(ConnectionString));

            services.AddSingleton<IDatabaseInitializerService, DatabaseInitializerService>();
            services.AddSingleton<IDatabaseSeederService, DatabaseSeederService>();
            services.AddTransient<IBusStationService, BusStationService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ITripService, TripService>();
            services.AddTransient<IBankAccountService, BankAccountService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ICompanyService, CompanyService>();

            services.AddAutoMapper();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}