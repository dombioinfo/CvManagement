using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorBaseApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListeType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    DefaultLibelle = table.Column<string>(type: "VARCHAR", maxLength: 300, nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 200, nullable: true),
                    Tel = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profil",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(type: "TEXT", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListeItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    DefaultLibelle = table.Column<string>(type: "VARCHAR", maxLength: 300, nullable: false),
                    ListeTypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Adresse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rue = table.Column<string>(type: "TEXT", nullable: false),
                    Complement = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    Ville = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    CodePostal = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    PersonneId = table.Column<long>(type: "INTEGER", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresse_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cv",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "VARCHAR", maxLength: 200, nullable: false),
                    RelativePath = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    FileSize = table.Column<int>(type: "INTEGER", nullable: false),
                    BlobFile = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PersonneId = table.Column<long>(type: "INTEGER", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cv", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cv_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfilId = table.Column<long>(type: "INTEGER", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Profil_ProfilId",
                        column: x => x.ProfilId,
                        principalTable: "Profil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidature",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCandidature = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Annotation = table.Column<string>(type: "VARCHAR", maxLength: 5000, nullable: false),
                    PersonneId = table.Column<long>(type: "INTEGER", nullable: false),
                    MetierId = table.Column<long>(type: "INTEGER", nullable: false),
                    StatutId = table.Column<long>(type: "INTEGER", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidature_ListeItem_MetierId",
                        column: x => x.MetierId,
                        principalTable: "ListeItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidature_ListeItem_StatutId",
                        column: x => x.StatutId,
                        principalTable: "ListeItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidature_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresse_PersonneId",
                table: "Adresse",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_MetierId",
                table: "Candidature",
                column: "MetierId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_PersonneId",
                table: "Candidature",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_StatutId",
                table: "Candidature",
                column: "StatutId");

            migrationBuilder.CreateIndex(
                name: "IX_Cv_PersonneId",
                table: "Cv",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeItem_ListeTypeId",
                table: "ListeItem",
                column: "ListeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfilId",
                table: "User",
                column: "ProfilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresse");

            migrationBuilder.DropTable(
                name: "Candidature");

            migrationBuilder.DropTable(
                name: "Cv");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ListeItem");

            migrationBuilder.DropTable(
                name: "Personnes");

            migrationBuilder.DropTable(
                name: "Profil");

            migrationBuilder.DropTable(
                name: "ListeType");
        }
    }
}
