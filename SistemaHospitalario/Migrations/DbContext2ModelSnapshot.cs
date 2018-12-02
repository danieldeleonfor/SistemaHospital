﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SistemaHospitalario.Models;
using System;

namespace SistemaHospitalario.Migrations
{
    [DbContext(typeof(DbContext2))]
    partial class DbContext2ModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SistemaHospitalario.Models.Citas", b =>
                {
                    b.Property<int>("CitasId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DoctorUsuarioId");

                    b.Property<int>("Duracion");

                    b.Property<DateTime>("HoraCita");

                    b.Property<int>("PacienteId");

                    b.HasKey("CitasId");

                    b.HasIndex("DoctorUsuarioId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("SistemaHospitalario.Models.Paciente", b =>
                {
                    b.Property<int>("PacienteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellido")
                        .IsRequired();

                    b.Property<string>("Cedula")
                        .IsRequired();

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<string>("TipoSangre")
                        .IsRequired();

                    b.HasKey("PacienteId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("SistemaHospitalario.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos");

                    b.Property<string>("Contrasenia");

                    b.Property<bool>("EsAdministrador");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("NombreUsuario");

                    b.Property<string>("Nombres");

                    b.Property<string>("Rol");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SistemaHospitalario.Models.Citas", b =>
                {
                    b.HasOne("SistemaHospitalario.Models.Usuario", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemaHospitalario.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
