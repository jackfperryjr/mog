﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mog.Data;

namespace Mog.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mog.Models.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Age")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<string>("Height");

                    b.Property<string>("Job")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Origin")
                        .IsRequired();

                    b.Property<string>("Picture");

                    b.Property<string>("Picture2");

                    b.Property<string>("Picture3");

                    b.Property<string>("Picture4");

                    b.Property<string>("Picture5");

                    b.Property<string>("Race")
                        .IsRequired();

                    b.Property<string>("Response1");

                    b.Property<string>("Response10");

                    b.Property<string>("Response2");

                    b.Property<string>("Response3");

                    b.Property<string>("Response4");

                    b.Property<string>("Response5");

                    b.Property<string>("Response6");

                    b.Property<string>("Response7");

                    b.Property<string>("Response8");

                    b.Property<string>("Response9");

                    b.Property<string>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Mog.Models.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Picture");

                    b.Property<string>("Platform");

                    b.Property<string>("ReleaseDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Mog.Models.Monster", b =>
                {
                    b.Property<Guid>("MonsterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddedBy");

                    b.Property<int>("Attack");

                    b.Property<int>("Defense");

                    b.Property<string>("Description");

                    b.Property<string>("ElementalAffinity");

                    b.Property<string>("ElementalWeakness");

                    b.Property<string>("Game");

                    b.Property<int>("HitPoints");

                    b.Property<string>("JapaneseName");

                    b.Property<int>("ManaPoints");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Picture");

                    b.HasKey("MonsterId");

                    b.ToTable("Monsters");
                });
#pragma warning restore 612, 618
        }
    }
}
