﻿// <auto-generated />
using DotNetApiChallenge.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNetApiChallenge.Migrations
{
    [DbContext(typeof(ApiChallengeDataContext))]
    [Migration("20241211014535_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("DotNetApiChallenge.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("DotNetApiChallenge.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ColorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lastname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Zipcode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("DotNetApiChallenge.Models.Person", b =>
                {
                    b.HasOne("DotNetApiChallenge.Models.Color", "Color")
                        .WithMany("Persons")
                        .HasForeignKey("ColorId");

                    b.Navigation("Color");
                });

            modelBuilder.Entity("DotNetApiChallenge.Models.Color", b =>
                {
                    b.Navigation("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
