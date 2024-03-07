using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorBaseApi.Migrations
{
    /// <inheritdoc />
    public partial class ChampsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_ListeItem_ResultatEntretienId",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_ListeItem_StatutId",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnes_ListeItem_CiviliteId",
                table: "Personnes");

            migrationBuilder.AlterColumn<long>(
                name: "CiviliteId",
                table: "Personnes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<long>(
                name: "StatutId",
                table: "Candidature",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<long>(
                name: "ResultatEntretienId",
                table: "Candidature",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_ListeItem_ResultatEntretienId",
                table: "Candidature",
                column: "ResultatEntretienId",
                principalTable: "ListeItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_ListeItem_StatutId",
                table: "Candidature",
                column: "StatutId",
                principalTable: "ListeItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnes_ListeItem_CiviliteId",
                table: "Personnes",
                column: "CiviliteId",
                principalTable: "ListeItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_ListeItem_ResultatEntretienId",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_ListeItem_StatutId",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnes_ListeItem_CiviliteId",
                table: "Personnes");

            migrationBuilder.AlterColumn<long>(
                name: "CiviliteId",
                table: "Personnes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StatutId",
                table: "Candidature",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ResultatEntretienId",
                table: "Candidature",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_ListeItem_ResultatEntretienId",
                table: "Candidature",
                column: "ResultatEntretienId",
                principalTable: "ListeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_ListeItem_StatutId",
                table: "Candidature",
                column: "StatutId",
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
    }
}
