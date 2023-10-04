using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySales.Server.Migrations
{
    /// <inheritdoc />
    public partial class TblReservacions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NumeroTelefono",
                table: "Clientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservaciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<long>(type: "bigint", nullable: false),
                    ProductoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaReservacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MontoReservacion = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CantidadHoras = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    EstadoFacturaId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreacion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoModificacionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservaciones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservaciones_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservaciones_TipoModificacion_TipoModificacionId",
                        column: x => x.TipoModificacionId,
                        principalTable: "TipoModificacion",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalle_ProductoId",
                table: "FacturaDetalle",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_ClienteId",
                table: "Reservaciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_ProductoId",
                table: "Reservaciones",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_TipoModificacionId",
                table: "Reservaciones",
                column: "TipoModificacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturaDetalle_Productos_ProductoId",
                table: "FacturaDetalle",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturaDetalle_Productos_ProductoId",
                table: "FacturaDetalle");

            migrationBuilder.DropTable(
                name: "Reservaciones");

            migrationBuilder.DropIndex(
                name: "IX_FacturaDetalle_ProductoId",
                table: "FacturaDetalle");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NumeroTelefono",
                table: "Clientes");
        }
    }
}
