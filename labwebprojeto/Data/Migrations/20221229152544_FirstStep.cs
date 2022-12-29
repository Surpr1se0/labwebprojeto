using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    public partial class FirstStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id_Categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__CB90334950410431", x => x.Id_Categoria);
                });

            migrationBuilder.CreateTable(
                name: "Consola",
                columns: table => new
                {
                    Id_Consola = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Consola__EF167BDDD47BBD8A", x => x.Id_Consola);
                });

            migrationBuilder.CreateTable(
                name: "Produtora",
                columns: table => new
                {
                    Id_Produtora = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Produtor__4FC8329532DCF4E4", x => x.Id_Produtora);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    Id_Utilizador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Utilizad__FEC354F1E2DADB37", x => x.Id_Utilizador);
                });

            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    Id_Jogos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    foto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    foto1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    foto_2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id_Categoria = table.Column<int>(type: "int", nullable: false),
                    Id_Consola = table.Column<int>(type: "int", nullable: false),
                    Id_Produtora = table.Column<int>(type: "int", nullable: false),
                    preco = table.Column<decimal>(type: "money", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descricao1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Jogo__7B4D1FE19E5B4DB9", x => x.Id_Jogos);
                    table.ForeignKey(
                        name: "FK__Jogo__Id_Categor__6477ECF3",
                        column: x => x.Id_Categoria,
                        principalTable: "Categoria",
                        principalColumn: "Id_Categoria");
                    table.ForeignKey(
                        name: "FK__Jogo__Id_Consola__656C112C",
                        column: x => x.Id_Consola,
                        principalTable: "Consola",
                        principalColumn: "Id_Consola");
                    table.ForeignKey(
                        name: "FK__Jogo__Id_Produto__66603565",
                        column: x => x.Id_Produtora,
                        principalTable: "Produtora",
                        principalColumn: "Id_Produtora");
                });

            migrationBuilder.CreateTable(
                name: "Favorito",
                columns: table => new
                {
                    Id_Favorito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Categoria = table.Column<int>(type: "int", nullable: false),
                    Id_Utilizador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorito__6DACC00DC83C35E0", x => x.Id_Favorito);
                    table.ForeignKey(
                        name: "FK__Favorito__Id_Cat__6D0D32F4",
                        column: x => x.Id_Categoria,
                        principalTable: "Categoria",
                        principalColumn: "Id_Categoria");
                    table.ForeignKey(
                        name: "FK__Favorito__Id_Uti__6E01572D",
                        column: x => x.Id_Utilizador,
                        principalTable: "Utilizador",
                        principalColumn: "Id_Utilizador");
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id_Compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Jogo = table.Column<int>(type: "int", nullable: false),
                    Id_Utilizador = table.Column<int>(type: "int", nullable: false),
                    data_compra = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Compra__661E0ED0060EB70A", x => x.Id_Compra);
                    table.ForeignKey(
                        name: "FK__Compra__Id_Jogo__693CA210",
                        column: x => x.Id_Jogo,
                        principalTable: "Jogo",
                        principalColumn: "Id_Jogos");
                    table.ForeignKey(
                        name: "FK__Compra__Id_Utili__6A30C649",
                        column: x => x.Id_Utilizador,
                        principalTable: "Utilizador",
                        principalColumn: "Id_Utilizador");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_Id_Jogo",
                table: "Compra",
                column: "Id_Jogo");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_Id_Utilizador",
                table: "Compra",
                column: "Id_Utilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_Id_Categoria",
                table: "Favorito",
                column: "Id_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_Id_Utilizador",
                table: "Favorito",
                column: "Id_Utilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Id_Categoria",
                table: "Jogo",
                column: "Id_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Id_Consola",
                table: "Jogo",
                column: "Id_Consola");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Id_Produtora",
                table: "Jogo",
                column: "Id_Produtora");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Favorito");

            migrationBuilder.DropTable(
                name: "Jogo");

            migrationBuilder.DropTable(
                name: "Utilizador");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Consola");

            migrationBuilder.DropTable(
                name: "Produtora");
        }
    }
}
