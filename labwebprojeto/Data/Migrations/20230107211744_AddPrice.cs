using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    public partial class AddPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Jogo",
                keyColumn: "Id_Jogos",
                keyValue: 1,
                column: "preco",
                value: 15m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Jogo",
                keyColumn: "Id_Jogos",
                keyValue: 1,
                column: "preco",
                value: 0m);
        }
    }
}
