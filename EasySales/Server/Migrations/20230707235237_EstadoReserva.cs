using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySales.Server.Migrations
{
    /// <inheritdoc />
    public partial class EstadoReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraInicio",
                table: "Reservaciones",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraFin",
                table: "Reservaciones",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");



           



            migrationBuilder.AddForeignKey(
                name: "FK_Reservaciones_EstadoReserva_EstadoReservaId",
                table: "Reservaciones",
                column: "EstadoReservaId",
                principalTable: "EstadoReserva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservaciones_EstadoReserva_EstadoReservaId",
                table: "Reservaciones");

            migrationBuilder.DropTable(
                name: "EstadoReserva");

            migrationBuilder.DropIndex(
                name: "IX_Reservaciones_EstadoReservaId",
                table: "Reservaciones");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EstadosFactura");

            migrationBuilder.RenameColumn(
                name: "EstadoReservaId",
                table: "Reservaciones",
                newName: "EstadoFacturaId");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "HoraInicio",
                table: "Reservaciones",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "HoraFin",
                table: "Reservaciones",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
