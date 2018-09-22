namespace PhotoShare.Services.Implementations
{
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly PhotoShareDbContext context;

        public DatabaseInitializerService(PhotoShareDbContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}