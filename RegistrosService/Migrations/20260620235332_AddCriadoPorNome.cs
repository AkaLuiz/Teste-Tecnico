using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrosService.Migrations
{
    /// <inheritdoc />
    public partial class AddCriadoPorNome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CriadoPorNome",
                table: "Registros",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoPorNome",
                table: "Registros");
        }
    }
}
