﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mog.Api.Infrastructure;

namespace Mog.Api.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mog.Api.Core.Models.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JapaneseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Mog.Api.Core.Models.DatingProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("DatingProfile");
                });

            modelBuilder.Entity("Mog.Api.Core.Models.DatingResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DatingProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Response")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DatingProfileId");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("Mog.Api.Core.Models.Feed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Addition")
                        .HasColumnType("int");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CharacterName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Deletion")
                        .HasColumnType("int");

                    b.Property<int>("Dislike")
                        .HasColumnType("int");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<int>("Love")
                        .HasColumnType("int");

                    b.Property<int>("PhotoUpdate")
                        .HasColumnType("int");

                    b.Property<int>("StatAddition")
                        .HasColumnType("int");

                    b.Property<int>("StatDeletion")
                        .HasColumnType("int");

                    b.Property<int>("StatUpdate")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("Update")
                        .HasColumnType("int");

                    b.Property<string>("UserFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Feed");
                });

            modelBuilder.Entity("Mog.Api.Core.Models.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Platform")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Mog.Api.Core.Models.Monster", b =>
                {
                    b.Property<Guid>("MonsterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ElementalAffinity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ElementalWeakness")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Game")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HitPoints")
                        .HasColumnType("int");

                    b.Property<string>("JapaneseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManaPoints")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MonsterId");

                    b.ToTable("Monsters");
                });

            modelBuilder.Entity("Mog.Api.Core.Models.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CollectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Primary")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Mog.Api.Core.Models.Stat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<string>("Class")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CollectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("HitPoints")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("Magic")
                        .HasColumnType("int");

                    b.Property<int>("MagicDefense")
                        .HasColumnType("int");

                    b.Property<int>("ManaPoints")
                        .HasColumnType("int");

                    b.Property<string>("Platform")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Spirit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("Mog.Api.Core.Models.DatingProfile", b =>
                {
                    b.HasOne("Mog.Api.Core.Models.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mog.Api.Core.Models.DatingResponse", b =>
                {
                    b.HasOne("Mog.Api.Core.Models.DatingProfile", "DatingProfile")
                        .WithMany("Responses")
                        .HasForeignKey("DatingProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mog.Api.Core.Models.Picture", b =>
                {
                    b.HasOne("Mog.Api.Core.Models.Character", "Character")
                        .WithMany("Pictures")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mog.Api.Core.Models.Stat", b =>
                {
                    b.HasOne("Mog.Api.Core.Models.Character", "Character")
                        .WithMany("Stats")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
