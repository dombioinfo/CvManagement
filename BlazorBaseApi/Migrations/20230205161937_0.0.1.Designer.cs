﻿// <auto-generated />
using System;
using BlazorBaseApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorBaseApi.Migrations
{
    [DbContext(typeof(MysqlDbContext))]
    [Migration("20230205161937_0.0.1")]
    partial class _001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BlazorBaseModel.Db.Adresse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CodePostal")
                        .HasColumnType("bigint")
                        .HasColumnName("CodePostal");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Complement");

                    b.Property<int>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("PersonneId")
                        .HasColumnType("bigint");

                    b.Property<string>("Rue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Rue");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("int");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Ville");

                    b.HasKey("Id");

                    b.HasIndex("PersonneId");

                    b.ToTable("Adresse");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Candidature", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Annotation")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Annotation");

                    b.Property<int>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCandidature")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateCandidature");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("PersonneId")
                        .HasColumnType("bigint")
                        .HasColumnName("PersonneId");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonneId");

                    b.ToTable("Candidature");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Personne", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateNaissance")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateNaissance");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("Email");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Nom");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Prenom");

                    b.Property<string>("Tel")
                        .HasColumnType("longtext")
                        .HasColumnName("Tel");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Personnes");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.Profil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Libelle");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Profil");
                });

            modelBuilder.Entity("BlazorBaseModel.Db.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasColumnName("Age");

                    b.Property<int>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
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

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfilId");

                    b.ToTable("User");
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
                    b.HasOne("BlazorBaseModel.Db.Personne", "Personne")
                        .WithMany()
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personne");
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

            modelBuilder.Entity("BlazorBaseModel.Db.Personne", b =>
                {
                    b.Navigation("Adresses");
                });
#pragma warning restore 612, 618
        }
    }
}