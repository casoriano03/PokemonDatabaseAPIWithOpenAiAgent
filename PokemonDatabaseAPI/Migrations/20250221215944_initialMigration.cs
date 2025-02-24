﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PokemonDatabaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbilityName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AbilityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hp = table.Column<int>(type: "int", nullable: false),
                    Atk = table.Column<int>(type: "int", nullable: false),
                    Def = table.Column<int>(type: "int", nullable: false),
                    Spd = table.Column<int>(type: "int", nullable: false),
                    SAtk = table.Column<int>(type: "int", nullable: false),
                    SDef = table.Column<int>(type: "int", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypes1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypes2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokedexEntryNumber = table.Column<int>(type: "int", nullable: false),
                    PokemonName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PokemonDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PokemonImgLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PokemonAbilityId = table.Column<int>(type: "int", nullable: true),
                    PokemonType1Id = table.Column<int>(type: "int", nullable: true),
                    PokemonType2Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemons_PokemonAbilities_PokemonAbilityId",
                        column: x => x.PokemonAbilityId,
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_PokemonTypes1_PokemonType1Id",
                        column: x => x.PokemonType1Id,
                        principalTable: "PokemonTypes1",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_PokemonTypes2_PokemonType2Id",
                        column: x => x.PokemonType2Id,
                        principalTable: "PokemonTypes2",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PokemonAbilities",
                columns: new[] { "Id", "AbilityDescription", "AbilityName" },
                values: new object[,]
                {
                    { 1, "Increases the power of Grass-type moves by 50% (1.5x) when the ability-bearer's HP falls below a third of its maximum (known in-game as in a pinch).", "Overgrow" },
                    { 2, "Increases the power of Fire-type moves by 50% (1.5x) when the ability-bearer's HP falls below a third of its maximum (known in-game as in a pinch).", "Blaze" },
                    { 3, "Increases the power of Water-type moves by 50% (1.5x) when the ability-bearer's HP falls below a third of its maximum (known in-game as in a pinch).", "Torrent" }
                });

            migrationBuilder.InsertData(
                table: "PokemonTypes1",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Normal" },
                    { 2, "Fire" },
                    { 3, "Water" },
                    { 4, "Electric" },
                    { 5, "Grass" },
                    { 6, "Ice" },
                    { 7, "Fighting" },
                    { 8, "Poison" },
                    { 9, "Ground" },
                    { 10, "Flying" },
                    { 11, "Psychic" },
                    { 12, "Bug" },
                    { 13, "Rock" },
                    { 14, "Ghost" },
                    { 15, "Dragon" },
                    { 16, "Dark" },
                    { 17, "Steel" },
                    { 18, "Fairy" }
                });

            migrationBuilder.InsertData(
                table: "PokemonTypes2",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Normal" },
                    { 2, "Fire" },
                    { 3, "Water" },
                    { 4, "Electric" },
                    { 5, "Grass" },
                    { 6, "Ice" },
                    { 7, "Fighting" },
                    { 8, "Poison" },
                    { 9, "Ground" },
                    { 10, "Flying" },
                    { 11, "Psychic" },
                    { 12, "Bug" },
                    { 13, "Rock" },
                    { 14, "Ghost" },
                    { 15, "Dragon" },
                    { 16, "Dark" },
                    { 17, "Steel" },
                    { 18, "Fairy" },
                    { 19, "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_AbilityName",
                table: "PokemonAbilities",
                column: "AbilityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokedexEntryNumber",
                table: "Pokemons",
                column: "PokedexEntryNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokemonAbilityId",
                table: "Pokemons",
                column: "PokemonAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokemonName",
                table: "Pokemons",
                column: "PokemonName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokemonType1Id",
                table: "Pokemons",
                column: "PokemonType1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokemonType2Id",
                table: "Pokemons",
                column: "PokemonType2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes1_TypeName",
                table: "PokemonTypes1",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes2_TypeName",
                table: "PokemonTypes2",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "PokemonStats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "PokemonTypes1");

            migrationBuilder.DropTable(
                name: "PokemonTypes2");
        }
    }
}
