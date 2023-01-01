using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    public partial class AddUserBools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "isAdmin",
            //    table: "Utilizador",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "isCliente",
            //    table: "Utilizador",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "isFunc",
            //    table: "Utilizador",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "isAdmin",
            //    table: "Utilizador");

            //migrationBuilder.DropColumn(
            //    name: "isCliente",
            //    table: "Utilizador");

            //migrationBuilder.DropColumn(
            //    name: "isFunc",
            //    table: "Utilizador");
        }
    }
}
