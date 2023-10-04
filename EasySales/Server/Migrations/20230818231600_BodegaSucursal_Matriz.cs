using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySales.Server.Migrations
{
    /// <inheritdoc />
    public partial class BodegaSucursal_Matriz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Activa",
                table: "Bodegas",
                newName: "Activo");

            migrationBuilder.CreateTable(
                name: "BodegaSucursalMatriz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    BodegaId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodegaSucursalMatriz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodegaSucursalMatriz_Bodegas_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodegas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodegaSucursalMatriz_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodegaSucursalMatriz_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BodegaSucursalMatriz_BodegaId",
                table: "BodegaSucursalMatriz",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_BodegaSucursalMatriz_EmpresaId",
                table: "BodegaSucursalMatriz",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_BodegaSucursalMatriz_SucursalId",
                table: "BodegaSucursalMatriz",
                column: "SucursalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodegaSucursalMatriz");

            migrationBuilder.RenameColumn(
                name: "Activo",
                table: "Bodegas",
                newName: "Activa");
        }
    }
}
