namespace HospitalDatabase.Data
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HospitalDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Patient>()
                .Property(p => p.FirstName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Entity<Patient>()
                .Property(p => p.LastName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Entity<Patient>()
                .Property(p => p.Address)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder
                .Entity<Patient>()
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(80);

            builder
                .Entity<Visitation>()
                .Property(v => v.Comments)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder
                .Entity<Diagnose>()
                .Property(d => d.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Entity<Diagnose>()
                .Property(d => d.Comments)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder
                .Entity<Medicament>()
                .Property(m => m.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Entity<Doctor>()
                .Property(d => d.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder
                .Entity<Doctor>()
                .Property(d => d.Specialty)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder
                .Entity<PatientMedicament>()
                .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

            builder
                .Entity<PatientMedicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(m => m.Patients)
                .HasForeignKey(p => p.MedicamentId);

            builder
                .Entity<PatientMedicament>()
                .HasOne(pm => pm.Patient)
                .WithMany(p => p.Medicaments)
                .HasForeignKey(m => m.PatientId);

            builder
                .Entity<Patient>()
                .HasMany(p => p.Diagnoses)
                .WithOne(d => d.Patient)
                .HasForeignKey(p => p.PatientId);

            builder
                .Entity<Patient>()
                .HasMany(p => p.Visitations)
                .WithOne(v => v.Patient)
                .HasForeignKey(p => p.PatientId);

            builder
                .Entity<Doctor>()
                .HasMany(d => d.Visitations)
                .WithOne(v => v.Doctor)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}