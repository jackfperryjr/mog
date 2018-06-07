﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Mvc.Models;
using System;

namespace Mvc.Migrations
{
    [DbContext(typeof(CharacterContext))]
    [Migration("20180607154309_UpdatedGameTable")]
    partial class UpdatedGameTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Mvc.Models.Characters", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Age")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<int>("GameId");

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

                    b.Property<string>("Race")
                        .IsRequired();

                    b.Property<string>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("Mvc.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Picture");

                    b.Property<string>("Platform");

                    b.Property<string>("ReleaseDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Mvc.Models.Monster", b =>
                {
                    b.Property<int>("MonsterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("GameId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Picture");

                    b.Property<string>("Strength");

                    b.Property<string>("Weakness");

                    b.HasKey("MonsterId");

                    b.HasIndex("GameId");

                    b.ToTable("Monsters");
                });

            modelBuilder.Entity("Mvc.Models.Characters", b =>
                {
                    b.HasOne("Mvc.Models.Game", "Game")
                        .WithMany("Characters")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mvc.Models.Monster", b =>
                {
                    b.HasOne("Mvc.Models.Game")
                        .WithMany("Monsters")
                        .HasForeignKey("GameId");
                });
#pragma warning restore 612, 618
        }
    }
}