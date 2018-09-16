﻿// <auto-generated />
namespace BillsPaymentSystem.Data.Migrations
{
    using System;
    using BillsPaymentSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    [DbContext(typeof(BillsPaymentDbContext))]
    [Migration("20180803153954_InitialTables")]
    partial class InitialTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BillsPaymentSystem.Data.Models.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("SwiftCode")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("BillsPaymentSystem.Data.Models.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<decimal>("Limit");

                    b.Property<decimal>("MoneyOwed");

                    b.HasKey("Id");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("BillsPaymentSystem.Data.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankAccountId");

                    b.Property<int?>("CreditCardId");

                    b.Property<int>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId")
                        .IsUnique()
                        .HasFilter("[BankAccountId] IS NOT NULL");

                    b.HasIndex("CreditCardId")
                        .IsUnique()
                        .HasFilter("[CreditCardId] IS NOT NULL");

                    b.HasIndex("UserId", "BankAccountId", "CreditCardId")
                        .IsUnique()
                        .HasFilter("[BankAccountId] IS NOT NULL AND [CreditCardId] IS NOT NULL");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("BillsPaymentSystem.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BillsPaymentSystem.Data.Models.PaymentMethod", b =>
                {
                    b.HasOne("BillsPaymentSystem.Data.Models.BankAccount", "BankAccount")
                        .WithOne("PaymentMethod")
                        .HasForeignKey("BillsPaymentSystem.Data.Models.PaymentMethod", "BankAccountId");

                    b.HasOne("BillsPaymentSystem.Data.Models.CreditCard", "CreditCard")
                        .WithOne("PaymentMethod")
                        .HasForeignKey("BillsPaymentSystem.Data.Models.PaymentMethod", "CreditCardId");

                    b.HasOne("BillsPaymentSystem.Data.Models.User", "User")
                        .WithMany("PaymentMethods")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
