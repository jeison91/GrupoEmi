﻿// <auto-generated />
using System;
using Emi.Employees.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Emi.Employees.Infrastructure.Migrations
{
    [DbContext(typeof(EmiDbContext))]
    [Migration("20250721005148_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Emi.Employees.Domain.Entities.DepartmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Departments", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Gerencia"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Contabilidad"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Tecnología"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Administrativo"
                        });
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CurrentPosition")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentPosition");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.EmployeeProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("EmployeeProjects", (string)null);
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.PositionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<bool>("IsManager")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Department");

                    b.ToTable("Positions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Department = 1,
                            IsManager = true,
                            Name = "Presidente"
                        },
                        new
                        {
                            Id = 2,
                            Department = 3,
                            IsManager = true,
                            Name = "Director Tecnología"
                        },
                        new
                        {
                            Id = 3,
                            Department = 2,
                            IsManager = true,
                            Name = "Director Contable"
                        },
                        new
                        {
                            Id = 4,
                            Department = 2,
                            IsManager = false,
                            Name = "Auxiliar contable"
                        },
                        new
                        {
                            Id = 5,
                            Department = 4,
                            IsManager = true,
                            Name = "Director Financiero"
                        },
                        new
                        {
                            Id = 6,
                            Department = 3,
                            IsManager = false,
                            Name = "Ingenieros"
                        },
                        new
                        {
                            Id = 7,
                            Department = 4,
                            IsManager = false,
                            Name = "Secretaria"
                        });
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.PositionHistoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PositionId");

                    b.ToTable("PositionHistories", (string)null);
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.ProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Projects", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Proyecto 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Proyecto 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Proyecto 3"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Proyecto 4"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Proyecto 5"
                        });
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Role");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "Admin123",
                            Role = 1,
                            Username = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            Password = "User123*",
                            Role = 2,
                            Username = "User1"
                        },
                        new
                        {
                            Id = 3,
                            Password = "useR987*",
                            Role = 2,
                            Username = "User2"
                        });
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.UserRolEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("UserRols", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "ADM",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Code = "USR",
                            Name = "User"
                        });
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("Emi.Employees.Domain.Entities.PositionEntity", "PositionTrace")
                        .WithMany("Employees")
                        .HasForeignKey("CurrentPosition")
                        .IsRequired()
                        .HasConstraintName("FK_Employee_Position");

                    b.Navigation("PositionTrace");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.EmployeeProjectEntity", b =>
                {
                    b.HasOne("Emi.Employees.Domain.Entities.EmployeeEntity", "EmployeeTrace")
                        .WithMany("EmployeeProjectTrace")
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("FK_EmployeeProject_Employee");

                    b.HasOne("Emi.Employees.Domain.Entities.ProjectEntity", "ProjectTrace")
                        .WithMany("EmployeeProjectTrace")
                        .HasForeignKey("ProjectId")
                        .IsRequired()
                        .HasConstraintName("FK_EmployeeProject_Project");

                    b.Navigation("EmployeeTrace");

                    b.Navigation("ProjectTrace");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.PositionEntity", b =>
                {
                    b.HasOne("Emi.Employees.Domain.Entities.DepartmentEntity", "DepartmentTrace")
                        .WithMany("DepartmentPositionsTrace")
                        .HasForeignKey("Department")
                        .IsRequired()
                        .HasConstraintName("FK_Positions_Department");

                    b.Navigation("DepartmentTrace");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.PositionHistoryEntity", b =>
                {
                    b.HasOne("Emi.Employees.Domain.Entities.EmployeeEntity", "Employee")
                        .WithMany("PositionHistoryTrace")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PositionHistory_Employee");

                    b.HasOne("Emi.Employees.Domain.Entities.PositionEntity", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_PositionHistory_Position");

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("Emi.Employees.Domain.Entities.UserRolEntity", "RoleTrace")
                        .WithMany("UsersTrace")
                        .HasForeignKey("Role")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RoleTrace");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.DepartmentEntity", b =>
                {
                    b.Navigation("DepartmentPositionsTrace");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.EmployeeEntity", b =>
                {
                    b.Navigation("EmployeeProjectTrace");

                    b.Navigation("PositionHistoryTrace");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.PositionEntity", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.ProjectEntity", b =>
                {
                    b.Navigation("EmployeeProjectTrace");
                });

            modelBuilder.Entity("Emi.Employees.Domain.Entities.UserRolEntity", b =>
                {
                    b.Navigation("UsersTrace");
                });
#pragma warning restore 612, 618
        }
    }
}
