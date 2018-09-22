namespace PhotoShare.App
{
    using AutoMapper;
    using Core;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Implementations;
    using System;

    using static Data.Configurations.PhotoShareDbConfiguration;

    public class StartUp
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<PhotoShareDbContext>(options =>
                options.UseSqlServer(ConnectionString));

            services.AddSingleton<ISessionService, SessionService>();

            services.AddTransient<ICommandParser, CommandParser>();

            services.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<ITownService, TownService>();

            services.AddTransient<ITagService, TagService>();

            services.AddTransient<IPictureService, PictureService>();

            services.AddTransient<IAlbumService, AlbumService>();

            services.AddTransient<IAlbumTagService, AlbumTagService>();

            services.AddTransient<IAlbumRoleService, AlbumRoleService>();

            services.AddTransient<IAlbumRoleService, AlbumRoleService>();

            services.AddTransient<IPictureService, PictureService>();

            services.AddAutoMapper();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}