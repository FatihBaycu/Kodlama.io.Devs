﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    [Migration("20221010201227_technology-Added")]
    partial class technologyAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.ProgrammingLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("ProgrammingLanguages", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("ProgrammingLanguageId")
                        .HasColumnType("int")
                        .HasColumnName("ProgrammingLanguageId");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammingLanguageId");

                    b.ToTable("Technologies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Spring",
                            ProgrammingLanguageId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Jsp",
                            ProgrammingLanguageId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "WPF",
                            ProgrammingLanguageId = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = "ASP.NET",
                            ProgrammingLanguageId = 2
                        },
                        new
                        {
                            Id = 5,
                            Name = "Vue",
                            ProgrammingLanguageId = 3
                        },
                        new
                        {
                            Id = 6,
                            Name = "React",
                            ProgrammingLanguageId = 3
                        });
                });

            modelBuilder.Entity("Domain.Entities.Technology", b =>
                {
                    b.HasOne("Domain.Entities.ProgrammingLanguage", "ProgrammingLanguage")
                        .WithMany()
                        .HasForeignKey("ProgrammingLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgrammingLanguage");
                });
#pragma warning restore 612, 618
        }
    }
}
