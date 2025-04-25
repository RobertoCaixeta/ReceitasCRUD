using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceitasCRUD.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaNome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Receitas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Receitas");
        }
    }
}
