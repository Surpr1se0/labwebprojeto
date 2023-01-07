using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    public partial class AddSeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", "6fb1175d-3374-4304-bccb-78e18cca98a2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fb1175d-3374-4304-bccb-78e18cca98a2");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id_Categoria", "JogoIdJogos", "nome" },
                values: new object[,]
                {
                    { 1, null, "FPS" },
                    { 2, null, "Sports" },
                    { 3, null, "Action" }
                });

            migrationBuilder.InsertData(
                table: "Consola",
                columns: new[] { "Id_Consola", "JogoIdJogos", "nome" },
                values: new object[,]
                {
                    { 1, null, "PS5" },
                    { 2, null, "XBox Series X" },
                    { 3, null, "PC" }
                });

            migrationBuilder.InsertData(
                table: "Produtora",
                columns: new[] { "Id_Produtora", "JogoIdJogos", "nome" },
                values: new object[,]
                {
                    { 1, null, "EA Games" },
                    { 2, null, "Rockstar Games" },
                    { 3, null, "Blizzard" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id_Categoria",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id_Categoria",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id_Categoria",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Consola",
                keyColumn: "Id_Consola",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Consola",
                keyColumn: "Id_Consola",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Consola",
                keyColumn: "Id_Consola",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Produtora",
                keyColumn: "Id_Produtora",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produtora",
                keyColumn: "Id_Produtora",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produtora",
                keyColumn: "Id_Produtora",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", "02174cf0–9412–4cfe-afbf-59f706d72cf6", "SystemADMIN", "SystemADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6fb1175d-3374-4304-bccb-78e18cca98a2", 0, "ca737d2f-5141-41be-b9f8-a6258bae955b", "sysadmin@gmail.com", true, false, null, "SYSADMIN@GMAIL.COM", "SYSADMIN", "AQAAAAEAACcQAAAAEFKdceF1hFz0XLtmEpbJdOMEFNYtVUEvbNpIQo9TJTV++Zdm5obdk9k+ShLbCAaAkw==", null, false, "8d7c9aef-5bd3-4ffa-8673-8d6ca43a97c1", false, "SysAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", "6fb1175d-3374-4304-bccb-78e18cca98a2" });
        }
    }
}
