﻿// <auto-generated />
using System;
using ComicSys.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ComicSys.Migrations
{
    [DbContext(typeof(ComicSystemContext))]
    partial class ComicSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ComicSys.Models.ComicBook", b =>
                {
                    b.Property<int>("ComicBookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ComicBookID"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ComicBookID");

                    b.ToTable("ComicBooks");
                });

            modelBuilder.Entity("ComicSys.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ComicSys.Models.Rental", b =>
                {
                    b.Property<int>("RentalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RentalID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentalDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RentalID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("ComicSys.Models.RentalDetail", b =>
                {
                    b.Property<int>("RentalDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RentalDetailID"));

                    b.Property<int>("ComicBookID")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RentalID")
                        .HasColumnType("int");

                    b.HasKey("RentalDetailID");

                    b.HasIndex("ComicBookID");

                    b.HasIndex("RentalID");

                    b.ToTable("RentalDetails");
                });

            modelBuilder.Entity("ComicSys.Models.Rental", b =>
                {
                    b.HasOne("ComicSys.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ComicSys.Models.RentalDetail", b =>
                {
                    b.HasOne("ComicSys.Models.ComicBook", "ComicBook")
                        .WithMany()
                        .HasForeignKey("ComicBookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComicSys.Models.Rental", "Rental")
                        .WithMany("RentalDetails")
                        .HasForeignKey("RentalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComicBook");

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("ComicSys.Models.Rental", b =>
                {
                    b.Navigation("RentalDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
