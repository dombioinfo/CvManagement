using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorBaseApi.Migrations
{
    /// <inheritdoc />
    public partial class _005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Actif",
                table: "User",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Actif",
                table: "Profil",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Actif",
                table: "Personnes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Actif",
                table: "Cv",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Actif",
                table: "Candidature",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "MetierId",
                table: "Candidature",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Actif",
                table: "Adresse",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ListeType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultLibelle = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    DateCreation = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: true),
                    Actif = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ListeItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultLibelle = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ListeTypeId = table.Column<long>(type: "bigint", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    DateCreation = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: true),
                    Actif = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListeItem_ListeType_ListeTypeId",
                        column: x => x.ListeTypeId,
                        principalTable: "ListeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_MetierId",
                table: "Candidature",
                column: "MetierId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeItem_ListeTypeId",
                table: "ListeItem",
                column: "ListeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_ListeItem_MetierId",
                table: "Candidature",
                column: "MetierId",
                principalTable: "ListeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_ListeItem_MetierId",
                table: "Candidature");

            migrationBuilder.DropTable(
                name: "ListeItem");

            migrationBuilder.DropTable(
                name: "ListeType");

            migrationBuilder.DropIndex(
                name: "IX_Candidature_MetierId",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "Actif",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Actif",
                table: "Profil");

            migrationBuilder.DropColumn(
                name: "Actif",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Actif",
                table: "Cv");

            migrationBuilder.DropColumn(
                name: "Actif",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "MetierId",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "Actif",
                table: "Adresse");
        }
    }
}
