﻿// <auto-generated />
namespace HospitalDatabase.Data.Migrations
{
    using System;
    using HospitalDatabase.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;

    [DbContext(typeof(HospitalDbContext))]
    partial class HospitalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HospitalDatabase.Data.Models.Diagnose", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<int>("PatientId");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Diagnoses");
                });

            modelBuilder.Entity("HospitalDatabase.Data.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("HospitalDatabase.Data.Models.Medicament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("HospitalDatabase.Data.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<bool>("HasInsurance");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HospitalDatabase.Data.Models.PatientMedicament", b =>
                {
                    b.Property<int>("PatientId");

                    b.Property<int>("MedicamentId");

                    b.HasKey("PatientId", "MedicamentId");

                    b.HasIndex("MedicamentId");

                    b.ToTable("PatientMedicament");
                });

            modelBuilder.Entity("HospitalDatabase.Data.Models.Visitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("DoctorId");

                    b.Property<int>("PatientId");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Visitations");
                });

            modelBuilder.Entity("HospitalDatabase.Data.Models.Diagnose", b =>
                {
                    b.HasOne("HospitalDatabase.Data.Models.Patient", "Patient")
                        .WithMany("Diagnoses")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HospitalDatabase.Data.Models.PatientMedicament", b =>
                {
                    b.HasOne("HospitalDatabase.Data.Models.Medicament", "Medicament")
                        .WithMany("Patients")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HospitalDatabase.Data.Models.Patient", "Patient")
                        .WithMany("Medicaments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HospitalDatabase.Data.Models.Visitation", b =>
                {
                    b.HasOne("HospitalDatabase.Data.Models.Doctor", "Doctor")
                        .WithMany("Visitations")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HospitalDatabase.Data.Models.Patient", "Patient")
                        .WithMany("Visitations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
