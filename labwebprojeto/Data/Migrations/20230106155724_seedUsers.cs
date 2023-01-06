using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    public partial class seedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "27151075-3b07-4ff2-b975-a0ec4480354d", "200eaa8a-32fb-42de-a5ad-3dc5e9537481" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e2704b45-a586-406b-92f0-9227de8fe1f2", "a5926de4-7dff-4ea3-9b46-c04c25fa4684" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "200eaa8a-32fb-42de-a5ad-3dc5e9537481");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a5926de4-7dff-4ea3-9b46-c04c25fa4684");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c1aef0f8-880a-4f5b-80d9-28eff5a8f72e", 0, "2ae4f393-5489-4403-b15f-4911d42b0d52", "func@gmail.com", true, false, null, "FUNC@GMAIL.COM", "FUNC", "AQAAAAEAACcQAAAAEDuz5wilue6zM0h5OvZaRyJJnHEPdOIc9w0UkzFjg+zliHaF2eVWqf2C6Ra3J2FiHQ==", null, false, "ce2f56f1-1751-47fe-8193-f06dd648d6b9", false, "Func" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c6a76765-aee9-4053-adcc-33b337a7d49b", 0, "e99321d7-a306-4e73-8b7b-0f321c0931b3", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEJulYE4BjWNbR7Bs7ObRuhO1oJrhPCiPX3N07mYFqORtZcSPnzWgT7FTPKwHHpU8PQ==", null, false, "6f896fa7-1f41-41fb-95db-336fdbc8d642", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e2704b45-a586-406b-92f0-9227de8fe1f2", "c1aef0f8-880a-4f5b-80d9-28eff5a8f72e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "27151075-3b07-4ff2-b975-a0ec4480354d", "c6a76765-aee9-4053-adcc-33b337a7d49b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e2704b45-a586-406b-92f0-9227de8fe1f2", "c1aef0f8-880a-4f5b-80d9-28eff5a8f72e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "27151075-3b07-4ff2-b975-a0ec4480354d", "c6a76765-aee9-4053-adcc-33b337a7d49b" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c1aef0f8-880a-4f5b-80d9-28eff5a8f72e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6a76765-aee9-4053-adcc-33b337a7d49b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "200eaa8a-32fb-42de-a5ad-3dc5e9537481", 0, "e473f50f-71fe-42ac-aa02-e8ed59c4891f", "admin@gmail.com", true, false, null, "ADMIN1@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEJgPXCg65/XswW8zXiWTodmAIHxDPZnTl9sb6GxrvutdeGvEmcfh5DFzdR8ntamxdw==", null, false, "160f8ed4-8c05-4ca8-9737-dc8100b483de", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a5926de4-7dff-4ea3-9b46-c04c25fa4684", 0, "7ede1a95-b4e3-44c8-a450-20ac8ce1f585", "func@gmail.com", true, false, null, "FUNC@GMAIL.COM", "FUNC", "AQAAAAEAACcQAAAAECdsQsS190JgSAB6IF0ozF5yHeo0mAhG+1WMJEMWfx1tZGPH4xVWvWLrcHsSNBBMTA==", null, false, "92d49d74-5e67-4bb1-a1de-1e11ee457255", false, "Func" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "27151075-3b07-4ff2-b975-a0ec4480354d", "200eaa8a-32fb-42de-a5ad-3dc5e9537481" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e2704b45-a586-406b-92f0-9227de8fe1f2", "a5926de4-7dff-4ea3-9b46-c04c25fa4684" });
        }
    }
}
