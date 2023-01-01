using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    public partial class AlterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "email",
            //    table: "Utilizador",
            //    type: "nvarchar(40)",
            //    maxLength: 40,
            //    nullable: false,
            //    defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "email",
            //    table: "Utilizador");
        }
    }
}
