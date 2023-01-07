using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    public partial class AddSeededJogos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Jogo",
                columns: new[] { "Id_Jogos", "descricao", "descricao1", "foto", "foto1", "foto_2", "Id_Categoria", "Id_Consola", "Id_Produtora", "nome", "preco" },
                values: new object[] { 1, "Jogo de Fantasia", "Game Of The Year 2019", "https://res.cloudinary.com/dghxejpvl/image/upload/v1673112685/qomoenwxorzr3gkssezm.jpg", "https://res.cloudinary.com/dghxejpvl/image/upload/v1673112684/yttpg2vaa2bwkpjysibe.jpg", "https://res.cloudinary.com/dghxejpvl/image/upload/v1673112686/wy8lyivtkxskxapvkk4p.png", 3, 3, 3, "The Witcher", 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jogo",
                keyColumn: "Id_Jogos",
                keyValue: 1);
        }
    }
}
