namespace Employees.App
{
    using AutoMapper;
    using Core;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Implementations;
    using System;

    using static Data.Infrastructure.Configurations.EmployeesDbConfiguration;

    public class StartUp
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();
            var comandParser = new CommandParser(serviceProvider);

            var engine = new Engine(comandParser);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<EmployeesDbContext>(options =>
                options.UseSqlServer(ConnectionString));

            services.AddTransient<IEmployeeService, EmployeeService>();

            services.AddAutoMapper();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}