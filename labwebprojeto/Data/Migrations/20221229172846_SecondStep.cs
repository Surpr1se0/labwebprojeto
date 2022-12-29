using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    public partial class SecondStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JogoIdJogos",
                table: "Produtora",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JogoIdJogos",
                table: "Consola",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JogoIdJogos",
                table: "Categoria",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtora_JogoIdJogos",
                table: "Produtora",
                column: "JogoIdJogos");

            migrationBuilder.CreateIndex(
                name: "IX_Consola_JogoIdJogos",
                table: "Consola",
                column: "JogoIdJogos");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_JogoIdJogos",
                table: "Categoria",
                column: "JogoIdJogos");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Jogo_JogoIdJogos",
                table: "Categoria",
                column: "JogoIdJogos",
                principalTable: "Jogo",
                principalColumn: "Id_Jogos");

            migrationBuilder.AddForeignKey(
                name: "FK_Consola_Jogo_JogoIdJogos",
                table: "Consola",
                column: "JogoIdJogos",
                principalTable: "Jogo",
                principalColumn: "Id_Jogos");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtora_Jogo_JogoIdJogos",
                table: "Produtora",
                column: "JogoIdJogos",
                principalTable: "Jogo",
                principalColumn: "Id_Jogos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Jogo_JogoIdJogos",
                table: "Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Consola_Jogo_JogoIdJogos",
                table: "Consola");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtora_Jogo_JogoIdJogos",
                table: "Produtora");

            migrationBuilder.DropIndex(
                name: "IX_Produtora_JogoIdJogos",
                table: "Produtora");

            migrationBuilder.DropIndex(
                name: "IX_Consola_JogoIdJogos",
                table: "Consola");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_JogoIdJogos",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "JogoIdJogos",
                table: "Produtora");

            migrationBuilder.DropColumn(
                name: "JogoIdJogos",
                table: "Consola");

            migrationBuilder.DropColumn(
                name: "JogoIdJogos",
                table: "Categoria");
        }
    }
}
