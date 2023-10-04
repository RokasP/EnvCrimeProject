﻿// <auto-generated />
using System;
using EnvCrime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnvCrime.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231004202436_AddEmployees")]
    partial class AddEmployees
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EnvCrime.Models.poco.Department", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("EnvCrime.Models.poco.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EnvCrime.Models.poco.Errand", b =>
                {
                    b.Property<int>("ErrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ErrandId"), 1L, 1);

                    b.Property<DateTime>("DateOfObservation")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InformerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InformerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvestigatorAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvestigatorInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfCrime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ErrandId");

                    b.ToTable("Errands");
                });

            modelBuilder.Entity("EnvCrime.Models.poco.ErrandStatus", b =>
                {
                    b.Property<string>("StatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StatusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("ErrandStatus1");
                });

            modelBuilder.Entity("EnvCrime.Models.poco.Picture", b =>
                {
                    b.Property<int>("PictureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PictureId"), 1L, 1);

                    b.Property<int>("ErrandId")
                        .HasColumnType("int");

                    b.Property<string>("PictureName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PictureId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("EnvCrime.Models.poco.Sample", b =>
                {
                    b.Property<int>("SampleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SampleId"), 1L, 1);

                    b.Property<int>("ErrandId")
                        .HasColumnType("int");

                    b.Property<string>("SampleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SampleId");

                    b.ToTable("Samples");
                });

            modelBuilder.Entity("EnvCrime.Models.poco.Sequence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CurrentValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sequences");
                });
#pragma warning restore 612, 618
        }
    }
}
