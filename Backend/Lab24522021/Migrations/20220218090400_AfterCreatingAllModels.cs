using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.Migrations
{
    public partial class AfterCreatingAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorieIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_categoriie_ingredient = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descriere_categorie_ingredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Nume_Categorie_Retete = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieReteta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pahar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_Pahar = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pahar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipReteta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_Tip_Retete = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipReteta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unitate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_unitate = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "SubCategorieIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_Subcategoriie_ingredient = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descriere_subcategorie_ingredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorieIngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdCategorieIngredient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorieIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategorieIngredient_CategorieIngredient_CategorieIngredientId",
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
                    Nume_reteta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descriere_reteta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instructiuni_reteta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poza_reteta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating_retea = table.Column<float>(type: "real", nullable: false),
                    CategorieRetetaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdCategorieReteta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipRetetaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdTipReteta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaharId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdPahar = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Reteta_Pahar_PaharId",
                        column: x => x.PaharId,
                        principalTable: "Pahar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reteta_TipReteta_TipRetetaId",
                        column: x => x.TipRetetaId,
                        principalTable: "TipReteta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume_ingredient = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubCategorieIngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdSubCategorieIngredient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_SubCategorieIngredient_SubCategorieIngredientId",
                        column: x => x.SubCategorieIngredientId,
                        principalTable: "SubCategorieIngredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Apreciere",
                columns: table => new
                {
                    IdReteta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apreciere", x => new { x.IdReteta, x.IdUser });
                    table.ForeignKey(
                        name: "FK_Apreciere_Reteta_IdReteta",
                        column: x => x.IdReteta,
                        principalTable: "Reteta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apreciere_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Apreciere_IdReteta_IdUser",
                table: "Apreciere",
                columns: new[] { "IdReteta", "IdUser" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apreciere_IdUser",
                table: "Apreciere",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_CategorieIngredient_Nume_categoriie_ingredient",
                table: "CategorieIngredient",
                column: "Nume_categoriie_ingredient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategorieReteta_Nume_Categorie_Retete",
                table: "CategorieReteta",
                column: "Nume_Categorie_Retete",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Nume_ingredient",
                table: "Ingredient",
                column: "Nume_ingredient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_SubCategorieIngredientId",
                table: "Ingredient",
                column: "SubCategorieIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Pahar_Nume_Pahar",
                table: "Pahar",
                column: "Nume_Pahar",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reteta_CategorieRetetaId",
                table: "Reteta",
                column: "CategorieRetetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reteta_Nume_reteta",
                table: "Reteta",
                column: "Nume_reteta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reteta_PaharId",
                table: "Reteta",
                column: "PaharId");

            migrationBuilder.CreateIndex(
                name: "IX_Reteta_TipRetetaId",
                table: "Reteta",
                column: "TipRetetaId");

            migrationBuilder.CreateIndex(
                name: "IX_RetetaIngredient_IdIngredient",
                table: "RetetaIngredient",
                column: "IdIngredient");

            migrationBuilder.CreateIndex(
                name: "IX_RetetaIngredient_UnitateId",
                table: "RetetaIngredient",
                column: "UnitateId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorieIngredient_CategorieIngredientId",
                table: "SubCategorieIngredient",
                column: "CategorieIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorieIngredient_Nume_Subcategoriie_ingredient",
                table: "SubCategorieIngredient",
                column: "Nume_Subcategoriie_ingredient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipReteta_Nume_Tip_Retete",
                table: "TipReteta",
                column: "Nume_Tip_Retete",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unitate_Nume_unitate",
                table: "Unitate",
                column: "Nume_unitate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apreciere");

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
                name: "SubCategorieIngredient");

            migrationBuilder.DropTable(
                name: "CategorieReteta");

            migrationBuilder.DropTable(
                name: "Pahar");

            migrationBuilder.DropTable(
                name: "TipReteta");

            migrationBuilder.DropTable(
                name: "CategorieIngredient");
        }
    }
}
