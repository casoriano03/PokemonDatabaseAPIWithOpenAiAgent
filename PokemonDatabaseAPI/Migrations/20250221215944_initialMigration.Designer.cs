﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonDatabaseAPI.Data;

#nullable disable

namespace PokemonDatabaseAPI.Migrations
{
    [DbContext(typeof(PokemonDbContext))]
    [Migration("20250221215944_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PokemonDatabaseAPI.Model.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PokedexEntryNumber")
                        .HasColumnType("int");

                    b.Property<int?>("PokemonAbilityId")
                        .HasColumnType("int");

                    b.Property<string>("PokemonDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PokemonImgLink")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PokemonName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PokemonType1Id")
                        .HasColumnType("int");

                    b.Property<int?>("PokemonType2Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PokedexEntryNumber")
                        .IsUnique();

                    b.HasIndex("PokemonAbilityId");

                    b.HasIndex("PokemonName")
                        .IsUnique();

                    b.HasIndex("PokemonType1Id");

                    b.HasIndex("PokemonType2Id");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("PokemonDatabaseAPI.Model.PokemonAbility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AbilityDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AbilityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AbilityName")
                        .IsUnique();

                    b.ToTable("PokemonAbilities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AbilityDescription = "Increases the power of Grass-type moves by 50% (1.5x) when the ability-bearer's HP falls below a third of its maximum (known in-game as in a pinch).",
                            AbilityName = "Overgrow"
                        },
                        new
                        {
                            Id = 2,
                            AbilityDescription = "Increases the power of Fire-type moves by 50% (1.5x) when the ability-bearer's HP falls below a third of its maximum (known in-game as in a pinch).",
                            AbilityName = "Blaze"
                        },
                        new
                        {
                            Id = 3,
                            AbilityDescription = "Increases the power of Water-type moves by 50% (1.5x) when the ability-bearer's HP falls below a third of its maximum (known in-game as in a pinch).",
                            AbilityName = "Torrent"
                        });
                });

            modelBuilder.Entity("PokemonDatabaseAPI.Model.PokemonStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int>("SAtk")
                        .HasColumnType("int");

                    b.Property<int>("SDef")
                        .HasColumnType("int");

                    b.Property<int>("Spd")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PokemonStats");
                });

            modelBuilder.Entity("PokemonDatabaseAPI.Model.PokemonType1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TypeName")
                        .IsUnique();

                    b.ToTable("PokemonTypes1");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeName = "Normal"
                        },
                        new
                        {
                            Id = 2,
                            TypeName = "Fire"
                        },
                        new
                        {
                            Id = 3,
                            TypeName = "Water"
                        },
                        new
                        {
                            Id = 4,
                            TypeName = "Electric"
                        },
                        new
                        {
                            Id = 5,
                            TypeName = "Grass"
                        },
                        new
                        {
                            Id = 6,
                            TypeName = "Ice"
                        },
                        new
                        {
                            Id = 7,
                            TypeName = "Fighting"
                        },
                        new
                        {
                            Id = 8,
                            TypeName = "Poison"
                        },
                        new
                        {
                            Id = 9,
                            TypeName = "Ground"
                        },
                        new
                        {
                            Id = 10,
                            TypeName = "Flying"
                        },
                        new
                        {
                            Id = 11,
                            TypeName = "Psychic"
                        },
                        new
                        {
                            Id = 12,
                            TypeName = "Bug"
                        },
                        new
                        {
                            Id = 13,
                            TypeName = "Rock"
                        },
                        new
                        {
                            Id = 14,
                            TypeName = "Ghost"
                        },
                        new
                        {
                            Id = 15,
                            TypeName = "Dragon"
                        },
                        new
                        {
                            Id = 16,
                            TypeName = "Dark"
                        },
                        new
                        {
                            Id = 17,
                            TypeName = "Steel"
                        },
                        new
                        {
                            Id = 18,
                            TypeName = "Fairy"
                        });
                });

            modelBuilder.Entity("PokemonDatabaseAPI.Model.PokemonType2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TypeName")
                        .IsUnique();

                    b.ToTable("PokemonTypes2");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeName = "Normal"
                        },
                        new
                        {
                            Id = 2,
                            TypeName = "Fire"
                        },
                        new
                        {
                            Id = 3,
                            TypeName = "Water"
                        },
                        new
                        {
                            Id = 4,
                            TypeName = "Electric"
                        },
                        new
                        {
                            Id = 5,
                            TypeName = "Grass"
                        },
                        new
                        {
                            Id = 6,
                            TypeName = "Ice"
                        },
                        new
                        {
                            Id = 7,
                            TypeName = "Fighting"
                        },
                        new
                        {
                            Id = 8,
                            TypeName = "Poison"
                        },
                        new
                        {
                            Id = 9,
                            TypeName = "Ground"
                        },
                        new
                        {
                            Id = 10,
                            TypeName = "Flying"
                        },
                        new
                        {
                            Id = 11,
                            TypeName = "Psychic"
                        },
                        new
                        {
                            Id = 12,
                            TypeName = "Bug"
                        },
                        new
                        {
                            Id = 13,
                            TypeName = "Rock"
                        },
                        new
                        {
                            Id = 14,
                            TypeName = "Ghost"
                        },
                        new
                        {
                            Id = 15,
                            TypeName = "Dragon"
                        },
                        new
                        {
                            Id = 16,
                            TypeName = "Dark"
                        },
                        new
                        {
                            Id = 17,
                            TypeName = "Steel"
                        },
                        new
                        {
                            Id = 18,
                            TypeName = "Fairy"
                        },
                        new
                        {
                            Id = 19,
                            TypeName = ""
                        });
                });

            modelBuilder.Entity("PokemonDatabaseAPI.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PokemonDatabaseAPI.Model.Pokemon", b =>
                {
                    b.HasOne("PokemonDatabaseAPI.Model.PokemonAbility", "PokemonAbility")
                        .WithMany()
                        .HasForeignKey("PokemonAbilityId");

                    b.HasOne("PokemonDatabaseAPI.Model.PokemonType1", "PokemonType1")
                        .WithMany()
                        .HasForeignKey("PokemonType1Id");

                    b.HasOne("PokemonDatabaseAPI.Model.PokemonType2", "PokemonType2")
                        .WithMany()
                        .HasForeignKey("PokemonType2Id");

                    b.Navigation("PokemonAbility");

                    b.Navigation("PokemonType1");

                    b.Navigation("PokemonType2");
                });
#pragma warning restore 612, 618
        }
    }
}
