using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySales.Server.Migrations
{
    /// <inheritdoc />
    public partial class tituloreservaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Observacion",
                table: "Reservaciones",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoReservaId",
                table: "Reservaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Reservaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CantidadHoras",
                table: "Reservaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

             migrationBuilder.AlterColumn<string>(
                name: "NumeroTelefono",
                table: "Clientes",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservaciones_EstadoReserva_EstadoReservaId",
                table: "Reservaciones");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Reservaciones");

            migrationBuilder.UpdateData(
                table: "Reservaciones",
                keyColumn: "Observacion",
                keyValue: null,
                column: "Observacion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Observacion",
                table: "Reservaciones",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoReservaId",
                table: "Reservaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Reservaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CantidadHoras",
                table: "Reservaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroTelefono",
                table: "Clientes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservaciones_EstadoReserva_EstadoReservaId",
                table: "Reservaciones",
                column: "EstadoReservaId",
                principalTable: "EstadoReserva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
