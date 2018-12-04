using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaHospitalario.Migrations
{
    public partial class ultimoMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Citas_CitasId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_CitasId",
                table: "Consultas");

            migrationBuilder.RenameColumn(
                name: "CitasId",
                table: "Consultas",
                newName: "CitaId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DiaActual",
                table: "Consultas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaActual",
                table: "Consultas");

            migrationBuilder.RenameColumn(
                name: "CitaId",
                table: "Consultas",
                newName: "CitasId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_CitasId",
                table: "Consultas",
                column: "CitasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Citas_CitasId",
                table: "Consultas",
                column: "CitasId",
                principalTable: "Citas",
                principalColumn: "CitasId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
