using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistrosService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    NomeApresentante = table.Column<string>(type: "text", nullable: true),
                    CpfCnpj = table.Column<string>(type: "text", nullable: true),
                    DataEntrada = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CriadoPor = table.Column<Guid>(type: "uuid", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CriadoPorNome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Registros",
                columns: new[] { "Id", "AtualizadoEm", "CpfCnpj", "CriadoEm", "CriadoPor", "CriadoPorNome", "DataEntrada", "NomeApresentante", "Observacoes", "Status", "Tipo" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 6, 20, 10, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 20, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000001"), "Admin", new DateOnly(2026, 6, 20), "Empresa Contrato LTDA", "Registro seed contrato", 0, 0 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 19, 9, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 19, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000002"), "Registrador", new DateOnly(2026, 6, 19), "João da Silva", "Registro seed procuração", 0, 1 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 },
                    { new Guid("10000000-0000-0000-0000-000000000010"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 },
                    { new Guid("10000000-0000-0000-0000-000000000011"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "42591651000143", new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), "Consulta", new DateOnly(2026, 6, 18), "Empresa Notificação SA", "Registro seed notificação", 0, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registros");
        }
    }
}
