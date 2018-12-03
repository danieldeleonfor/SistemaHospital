using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaHospitalario.Migrations
{
    public partial class ConsultaCita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    ConsultaPacienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CitasId = table.Column<int>(nullable: false),
                    FechaProximaCita = table.Column<DateTime>(nullable: false),
                    PagoServicio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.ConsultaPacienteId);
                    table.ForeignKey(
                        name: "FK_Consultas_Citas_CitasId",
                        column: x => x.CitasId,
                        principalTable: "Citas",
                        principalColumn: "CitasId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receta",
                columns: table => new
                {
                    RecetaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(nullable: false),
                    ConsultaPacienteId = table.Column<int>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receta", x => x.RecetaId);
                    table.ForeignKey(
                        name: "FK_Receta_Consultas_ConsultaPacienteId",
                        column: x => x.ConsultaPacienteId,
                        principalTable: "Consultas",
                        principalColumn: "ConsultaPacienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_CitasId",
                table: "Consultas",
                column: "CitasId");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_ConsultaPacienteId",
                table: "Receta",
                column: "ConsultaPacienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receta");

            migrationBuilder.DropTable(
                name: "Consultas");
        }
    }
}
