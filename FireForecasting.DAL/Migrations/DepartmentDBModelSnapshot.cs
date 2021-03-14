﻿// <auto-generated />
using System;
using FireForecasting.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FireForecasting.DAL.Migrations
{
    [DbContext(typeof(DepartmentDB))]
    partial class DepartmentDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Incidents.Fire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CostOfDamage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Fires");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Division", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Department", "Department")
                        .WithMany("Divisions")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Employee", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Division", "Division")
                        .WithMany("Employees")
                        .HasForeignKey("DivisionId");

                    b.Navigation("Division");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Incidents.Fire", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId");

                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Division");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Department", b =>
                {
                    b.Navigation("Divisions");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Division", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
