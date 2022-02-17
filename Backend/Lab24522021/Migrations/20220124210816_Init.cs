using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorieIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_Subcategoriie_ingredient = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Descriere_subcategorie_ingredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieIngredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategorieReteta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_Categorie_Retete = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieReteta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataBaseModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBaseModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unitate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_unitate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unitate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_ingredient = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategorieIngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdCategorieIngredient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_CategorieIngredient_CategorieIngredientId",
                        column: x => x.CategorieIngredientId,
                        principalTable: "CategorieIngredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reteta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_reteta = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Descriere_reteta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link_reteta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poza_reteta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    durata_preparare = table.Column<int>(type: "int", nullable: false),
                    durata_gatire = table.Column<int>(type: "int", nullable: false),
                    durata_totala = table.Column<int>(type: "int", nullable: false),
                    Scor_retea = table.Column<float>(type: "real", nullable: false),
                    CategorieRetetaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdCategorieReteta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reteta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reteta_CategorieReteta_CategorieRetetaId",
                        column: x => x.CategorieRetetaId,
                        principalTable: "CategorieReteta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetetaIngredient",
                columns: table => new
                {
                    IdReteta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdIngredient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUnitate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cantitate_Ingredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observatii = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetetaIngredient", x => new { x.IdReteta, x.IdIngredient });
                    table.ForeignKey(
                        name: "FK_RetetaIngredient_Ingredient_IdIngredient",
                        column: x => x.IdIngredient,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RetetaIngredient_Reteta_IdReteta",
                        column: x => x.IdReteta,
                        principalTable: "Reteta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RetetaIngredient_Unitate_UnitateId",
                        column: x => x.UnitateId,
                        principalTable: "Unitate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorieIngredient_Nume_Subcategoriie_ingredient",
                table: "CategorieIngredient",
                column: "Nume_Subcategoriie_ingredient",
                unique: true,
                filter: "[Nume_Subcategoriie_ingredient] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CategorieReteta_Nume_Categorie_Retete",
                table: "CategorieReteta",
                column: "Nume_Categorie_Retete",
                unique: true,
                filter: "[Nume_Categorie_Retete] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_CategorieIngredientId",
                table: "Ingredient",
                column: "CategorieIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Nume_ingredient",
                table: "Ingredient",
                column: "Nume_ingredient",
                unique: true,
                filter: "[Nume_ingredient] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reteta_CategorieRetetaId",
                table: "Reteta",
                column: "CategorieRetetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reteta_Nume_reteta",
                table: "Reteta",
                column: "Nume_reteta",
                unique: true,
                filter: "[Nume_reteta] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RetetaIngredient_IdIngredient",
                table: "RetetaIngredient",
                column: "IdIngredient");

            migrationBuilder.CreateIndex(
                name: "IX_RetetaIngredient_UnitateId",
                table: "RetetaIngredient",
                column: "UnitateId");

            migrationBuilder.CreateIndex(
                name: "IX_Unitate_Nume_unitate",
                table: "Unitate",
                column: "Nume_unitate",
                unique: true,
                filter: "[Nume_unitate] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataBaseModels");

            migrationBuilder.DropTable(
                name: "RetetaIngredient");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Reteta");

            migrationBuilder.DropTable(
                name: "Unitate");

            migrationBuilder.DropTable(
                name: "CategorieIngredient");

            migrationBuilder.DropTable(
                name: "CategorieReteta");
        }
    }
}
