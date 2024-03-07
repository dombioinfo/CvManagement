using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorBaseApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCivilite_FieldForCandidature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CiviliteId",
                table: "Personnes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEntree",
                table: "Candidature",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEntretien",
                table: "Candidature",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSortie",
                table: "Candidature",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModaliteOrientation",
                table: "Candidature",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotifRefus",
                table: "Candidature",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrescripteurLibelle",
                table: "Candidature",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrescripteurNom",
                table: "Candidature",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ResultatEntretienId",
                table: "Candidature",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_CiviliteId",
                table: "Personnes",
                column: "CiviliteId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_ResultatEntretienId",
                table: "Candidature",
                column: "ResultatEntretienId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_ListeItem_ResultatEntretienId",
                table: "Candidature",
                column: "ResultatEntretienId",
                principalTable: "ListeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnes_ListeItem_CiviliteId",
                table: "Personnes",
                column: "CiviliteId",
                principalTable: "ListeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_ListeItem_ResultatEntretienId",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnes_ListeItem_CiviliteId",
                table: "Personnes");

            migrationBuilder.DropIndex(
                name: "IX_Personnes_CiviliteId",
                table: "Personnes");

            migrationBuilder.DropIndex(
                name: "IX_Candidature_ResultatEntretienId",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "CiviliteId",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "DateEntree",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "DateEntretien",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "DateSortie",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "ModaliteOrientation",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "MotifRefus",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "PrescripteurLibelle",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "PrescripteurNom",
                table: "Candidature");

            migrationBuilder.DropColumn(
                name: "ResultatEntretienId",
                table: "Candidature");
        }
    }
}
