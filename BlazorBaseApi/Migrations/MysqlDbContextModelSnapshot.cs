﻿// <auto-generated />
using System;
using BlazorBaseApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorBaseApi.Migrations
{
    [DbContext(typeof(MysqlDbContext))]
    partial class MysqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BlazorBaseModel.Db.Adresse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Actif")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("CodePostal");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Complement");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("PersonneId")
                        .HasColumnType("bigint")
                        .HasColumnName("PersonneId");

                    b.Property<string>("Rue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Rue");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Ville");

                    b.HasKey("Id");

                    b.HasIndex("PersonneId");

                    b.ToTable("Adresse", (string)null);
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Candidature", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Actif")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Annotation")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Annotation");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCandidature")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateCandidature");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("MetierId")
                        .HasColumnType("bigint");

                    b.Property<long>("PersonneId")
                        .HasColumnType("bigint")
                        .HasColumnName("PersonneId");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MetierId");

                    b.HasIndex("PersonneId");

                    b.ToTable("Candidature", (string)null);
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Cv", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Actif")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("BlobFile")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("BlobFile");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("FileName");

                    b.Property<int>("FileSize")
                        .HasColumnType("int")
                        .HasColumnName("FileSize");

                    b.Property<long>("PersonneId")
                        .HasColumnType("bigint")
                        .HasColumnName("PersonneId");

                    b.Property<string>("RelativePath")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("RelativePath");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonneId");

                    b.ToTable("Cv", (string)null);
                });

            modelBuilder.Entity("BlazorBaseModel.Db.ListeItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Actif")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Code");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DefaultLibelle")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("DefaultLibelle");

                    b.Property<long>("ListeTypeId")
                        .HasColumnType("bigint");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ListeTypeId");

                    b.ToTable("ListeItem", (string)null);
                });

            modelBuilder.Entity("BlazorBaseModel.Db.ListeType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Actif")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Code");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DefaultLibelle")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("DefaultLibelle");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ListeType", (string)null);
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Personne", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Actif")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateNaissance")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateNaissance");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Nom");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Prenom");

                    b.Property<string>("Tel")
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Tel");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Personnes", (string)null);
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Profil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Actif")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Libelle");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Profil", (string)null);
                });

            modelBuilder.Entity("BlazorBaseModel.Db.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Actif")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasColumnName("Age");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Login");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Nom");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Password");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Prenom");

                    b.Property<long>("ProfilId")
                        .HasColumnType("bigint")
                        .HasColumnName("ProfilId");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfilId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Adresse", b =>
                {
                    b.HasOne("BlazorBaseModel.Db.Personne", "Personne")
                        .WithMany("Adresses")
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Candidature", b =>
                {
                    b.HasOne("BlazorBaseModel.Db.ListeItem", "ListeItem")
                        .WithMany()
                        .HasForeignKey("MetierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorBaseModel.Db.Personne", "Personne")
                        .WithMany("Candidatures")
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ListeItem");

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Cv", b =>
                {
                    b.HasOne("BlazorBaseModel.Db.Personne", "Personne")
                        .WithMany("Cvs")
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.ListeItem", b =>
                {
                    b.HasOne("BlazorBaseModel.Db.ListeType", "ListeType")
                        .WithMany("ListeItems")
                        .HasForeignKey("ListeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ListeType");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.User", b =>
                {
                    b.HasOne("BlazorBaseModel.Db.Profil", "Profil")
                        .WithMany()
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profil");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.ListeType", b =>
                {
                    b.Navigation("ListeItems");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Personne", b =>
                {
                    b.Navigation("Adresses");

                    b.Navigation("Candidatures");

                    b.Navigation("Cvs");
                });
#pragma warning restore 612, 618
        }
    }
}
