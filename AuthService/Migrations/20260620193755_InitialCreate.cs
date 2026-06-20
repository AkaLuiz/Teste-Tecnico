using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: true),
                    UsuarioPapel = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Ativo", "Email", "Name", "Senha", "UsuarioPapel" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), true, "admin@example.com", "Admin", "$2a$11$wsHii5Yy26cQv2vhAsZJmuu2Is/dtyU9OKpwmbxhp73BvYjhK26my", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), true, "registrador@example.com", "Registrador", "$2a$11$SOlOp.7wMedXA.mb6K0kzup9BlJVzKOB4KIlnGjObrRxeXS745kZa", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), true, "consulta@example.com", "Consulta", "$2a$11$Evh01g.08S7I/dteTfdLB.P.jAb.DgWU6cSMMCHbMCYHqE/06mruG", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
