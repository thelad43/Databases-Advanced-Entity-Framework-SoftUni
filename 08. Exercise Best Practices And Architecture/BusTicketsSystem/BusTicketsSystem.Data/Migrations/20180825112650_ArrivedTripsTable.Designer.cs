﻿// <auto-generated />
namespace BusTicketsSystem.Data.Migrations
{
    using System;
    using BusTicketsSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    [DbContext(typeof(BusTicketsSystemDbContext))]
    [Migration("20180825112650_ArrivedTripsTable")]
    partial class ArrivedTripsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusTicketsSystem.Models.ArrivedTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActualArrivalTime");

                    b.Property<int?>("DestinationBusStationId");

                    b.Property<int?>("OriginBusStationId");

                    b.Property<int>("PassengersCount");

                    b.HasKey("Id");

                    b.HasIndex("DestinationBusStationId");

                    b.HasIndex("OriginBusStationId");

                    b.ToTable("ArrivedTrips");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .IsRequired();

                    b.Property<decimal>("Balance");

                    b.Property<int>("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TownId");

                    b.HasKey("Id");

                    b.HasIndex("TownId");

                    b.ToTable("BusStations");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Nationality");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankAccountId");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("HomeTownId");

                    b.Property<bool?>("IsMale");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("HomeTownId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("CustomerId");

                    b.Property<double>("Grade");

                    b.Property<DateTime>("PublishDate");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId");

                    b.Property<decimal>("Price");

                    b.Property<int>("Seat");

                    b.Property<int>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TripId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArrivalTime");

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("DepartureTime");

                    b.Property<int>("DestinationStationId");

                    b.Property<int>("OriginBusStationId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DestinationStationId");

                    b.HasIndex("OriginBusStationId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.ArrivedTrip", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.BusStation", "DestinationBusStation")
                        .WithMany()
                        .HasForeignKey("DestinationBusStationId");

                    b.HasOne("BusTicketsSystem.Models.BusStation", "OriginBusStation")
                        .WithMany()
                        .HasForeignKey("OriginBusStationId");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BankAccount", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithOne("BankAccount")
                        .HasForeignKey("BusTicketsSystem.Models.BankAccount", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusStation", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Town", "Town")
                        .WithMany("BusStations")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Customer", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Town", "HomeTown")
                        .WithMany("Customers")
                        .HasForeignKey("HomeTownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Review", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Company", "Company")
                        .WithMany("Reviews")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Ticket", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketsSystem.Models.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Trip", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Company", "Company")
                        .WithMany("Trips")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketsSystem.Models.BusStation", "DestinationBusStation")
                        .WithMany("ArrivingTrips")
                        .HasForeignKey("DestinationStationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketsSystem.Models.BusStation", "OriginBusStation")
                        .WithMany("DeparturesTrips")
                        .HasForeignKey("OriginBusStationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
