﻿// <auto-generated />
using System;
using Bookleus.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookleus.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230209024859_AddedBookReservationTable")]
    partial class AddedBookReservationTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Bookleus.Domain.Entities.Book", b =>
                {
                    b.Property<Guid>("SKU")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("SKU");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Bookleus.Domain.Entities.CustomerBookReservation", b =>
                {
                    b.Property<Guid>("CustomerBookReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookSKU")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerBookReservationId");

                    b.HasIndex("BookSKU");

                    b.ToTable("BookReservations");
                });

            modelBuilder.Entity("Bookleus.Domain.Entities.CustomerBookReservation", b =>
                {
                    b.HasOne("Bookleus.Domain.Entities.Book", "Book")
                        .WithMany("BookReservations")
                        .HasForeignKey("BookSKU")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Bookleus.Domain.Entities.Book", b =>
                {
                    b.Navigation("BookReservations");
                });
#pragma warning restore 612, 618
        }
    }
}
