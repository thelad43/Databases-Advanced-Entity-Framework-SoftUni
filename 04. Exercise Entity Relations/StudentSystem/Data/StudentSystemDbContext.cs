namespace StudentSystem.Data
{
    using Configurations;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class StudentSystemDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<HomeworkSubmission> Homeworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(StudentSystemDbConfiguration.ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId);

            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.StudentId);

            builder
                .Entity<HomeworkSubmission>()
                .HasOne(hs => hs.Student)
                .WithMany(s => s.Homeworks)
                .HasForeignKey(h => h.StudentId);

            builder
                .Entity<HomeworkSubmission>()
                .HasOne(hs => hs.Course)
                .WithMany(c => c.Homeworks)
                .HasForeignKey(h => h.CourseId);

            builder
                .Entity<Course>()
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(c => c.CourseId);

            base.OnModelCreating(builder);
        }
    }
}