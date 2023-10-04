using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySales.Server.Migrations
{
    /// <inheritdoc />
    public partial class IdentityRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CajaId",
                table: "Facturas",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacion",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "TipoModificacionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "AspNetRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "AspNetRoles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "AspNetRoles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "AspNetRoles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoModificacionId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "AspNetRoles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "AspNetRoles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CajaId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "TipoModificacionId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UsuarioModificacion",
                keyValue: null,
                column: "UsuarioModificacion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacion",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "TipoModificacionId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
