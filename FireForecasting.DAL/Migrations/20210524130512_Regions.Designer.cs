﻿// <auto-generated />
using System;
using FireForecasting.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FireForecasting.DAL.Migrations
{
    [DbContext(typeof(DepartmentDB))]
    [Migration("20210524130512_Regions")]
    partial class Regions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Base.FireTruckBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FireEngine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("FoamVolume")
                        .HasColumnType("real");

                    b.Property<int>("FuelVolume")
                        .HasColumnType("int");

                    b.Property<byte>("LiftingHeight")
                        .HasColumnType("tinyint");

                    b.Property<byte>("MaxSpeed")
                        .HasColumnType("tinyint");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("NumberOfSeats")
                        .HasColumnType("tinyint");

                    b.Property<double>("PumpCapacity")
                        .HasColumnType("float");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TankVolume")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("YearOfCreation")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("FireTruckBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("FireTruckBase");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Base.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Region");
                });

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

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.DutyShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DispatcherId")
                        .HasColumnType("int");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("int");

                    b.Property<int?>("ShiftSupervisorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DispatcherId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("ShiftSupervisorId");

                    b.ToTable("DutyShift");
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

                    b.Property<int?>("FireBrigadeId")
                        .HasColumnType("int");

                    b.Property<int?>("FireTruckId")
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

                    b.HasIndex("FireBrigadeId");

                    b.HasIndex("FireTruckId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.FireBrigade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DutyShiftId")
                        .HasColumnType("int");

                    b.Property<int?>("FireTruckId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DutyShiftId");

                    b.HasIndex("FireTruckId");

                    b.ToTable("FireBrigade");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Incidents.Fire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Affected")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Casualties")
                        .HasColumnType("int");

                    b.Property<string>("CauseOfFire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CheckOutTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CompletionTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CostOfDamage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostOfSaved")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescriptionOfFire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DistanceToFire")
                        .HasColumnType("float");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DurationOfLiquidation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DurationOfLocalization")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DurationOfWork")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FirstBarrelTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LiquidationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LocalizationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RankOfFire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("RegionId");

                    b.ToTable("Fires");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.FireTruck", b =>
                {
                    b.HasBaseType("FireForecasting.DAL.Entityes.Base.FireTruckBase");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("int");

                    b.HasIndex("DivisionId");

                    b.HasDiscriminator().HasValue("FireTruck");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Division", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Department", "Department")
                        .WithMany("Divisions")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.DutyShift", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Employee", "Dispatcher")
                        .WithMany()
                        .HasForeignKey("DispatcherId");

                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Division", "Division")
                        .WithMany("DutyShifts")
                        .HasForeignKey("DivisionId");

                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Employee", "ShiftSupervisor")
                        .WithMany()
                        .HasForeignKey("ShiftSupervisorId");

                    b.Navigation("Dispatcher");

                    b.Navigation("Division");

                    b.Navigation("ShiftSupervisor");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Employee", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Division", "Division")
                        .WithMany("Employees")
                        .HasForeignKey("DivisionId");

                    b.HasOne("FireForecasting.DAL.Entityes.Departments.FireBrigade", null)
                        .WithMany("Crew")
                        .HasForeignKey("FireBrigadeId");

                    b.HasOne("FireForecasting.DAL.Entityes.Departments.FireTruck", null)
                        .WithMany("Crew")
                        .HasForeignKey("FireTruckId");

                    b.Navigation("Division");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.FireBrigade", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.DutyShift", null)
                        .WithMany("FireBrigades")
                        .HasForeignKey("DutyShiftId");

                    b.HasOne("FireForecasting.DAL.Entityes.Departments.FireTruck", "FireTruck")
                        .WithMany()
                        .HasForeignKey("FireTruckId");

                    b.Navigation("FireTruck");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Incidents.Fire", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId");

                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("FireForecasting.DAL.Entityes.Base.Region", null)
                        .WithMany("FiresInRegion")
                        .HasForeignKey("RegionId");

                    b.Navigation("Division");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.FireTruck", b =>
                {
                    b.HasOne("FireForecasting.DAL.Entityes.Departments.Division", "Division")
                        .WithMany("FireTrucks")
                        .HasForeignKey("DivisionId");

                    b.Navigation("Division");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Base.Region", b =>
                {
                    b.Navigation("FiresInRegion");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Department", b =>
                {
                    b.Navigation("Divisions");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.Division", b =>
                {
                    b.Navigation("DutyShifts");

                    b.Navigation("Employees");

                    b.Navigation("FireTrucks");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.DutyShift", b =>
                {
                    b.Navigation("FireBrigades");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.FireBrigade", b =>
                {
                    b.Navigation("Crew");
                });

            modelBuilder.Entity("FireForecasting.DAL.Entityes.Departments.FireTruck", b =>
                {
                    b.Navigation("Crew");
                });
#pragma warning restore 612, 618
        }
    }
}
