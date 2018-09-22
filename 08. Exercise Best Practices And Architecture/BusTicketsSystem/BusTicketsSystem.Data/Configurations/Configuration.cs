namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class Configuration
    {
        public void Configure<TClass, TConfig>(ModelBuilder builder)
            where TClass : class
            where TConfig : IEntityTypeConfiguration<TClass>
        {
            var configuration = Activator.CreateInstance<TConfig>();
            builder.ApplyConfiguration(configuration);
        }
    }
}