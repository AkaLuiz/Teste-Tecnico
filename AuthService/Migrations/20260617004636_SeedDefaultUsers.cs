using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Ativo", "Email", "Name", "Senha", "UsuarioPapel" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), true, "admin@example.com", "Admin", "$2a$11$wsHii5Yy26cQv2vhAsZJmuu2Is/dtyU9OKpwmbxhp73BvYjhK26my", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), true, "registrador@example.com", "Registrador", "$2a$11$2W47yF7X7vkJ1L6WIxuLOudbt9va6JK79twHwt0T5bEeKsitCgyM", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), true, "consulta@example.com", "Consulta", "$2a$11$Evh01g.08S7I/dteTfdLB.P.jAb.DgWU6cSMMCHbMCYHqE/06mruG", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));
        }
    }
}
