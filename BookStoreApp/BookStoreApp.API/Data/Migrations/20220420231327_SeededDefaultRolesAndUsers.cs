using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Data.Migrations
{
    public partial class SeededDefaultRolesAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "eb0e6c14-5924-40ba-a158-2e21a2f0ef3c", "162dc2d8-af32-484e-8036-5cd3fc88dd61", "Administrator", "ADMINISTRATOR" },
                    { "feb3de40-39ed-4b3b-8d62-5ba0662b6479", "2077118b-099e-47ce-9aa1-2dff3b3dd7fd", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0991be93-9fde-4701-aaae-7c201ba8a61b", 0, "da908bed-aeb1-4d1c-a070-d922933da8bb", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEGe2dVeYnDlfsjjI0coEEabJ5mCCuhQrQ+7Q2cGsPn54ZL6e0apLLgTRg0T84nxWdQ==", null, false, "8ec11157-2a2c-4df4-8e99-ab742af296a9", false, "admin@bookstore.com" },
                    { "0ad538cd-3d73-4e0d-a0b5-99429657e987", 0, "c970a535-3a46-42f9-a51e-443503cac992", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEAANVpb0zdx7czGAqql+KjQbHhWgqxRkq/99xHkh3oREy6E9bWzMKL3cpzB7VLOn9Q==", null, false, "9388c8ac-d7c8-4c0e-9d53-c83aa51b6410", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "eb0e6c14-5924-40ba-a158-2e21a2f0ef3c", "0991be93-9fde-4701-aaae-7c201ba8a61b" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "feb3de40-39ed-4b3b-8d62-5ba0662b6479", "0ad538cd-3d73-4e0d-a0b5-99429657e987" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eb0e6c14-5924-40ba-a158-2e21a2f0ef3c", "0991be93-9fde-4701-aaae-7c201ba8a61b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "feb3de40-39ed-4b3b-8d62-5ba0662b6479", "0ad538cd-3d73-4e0d-a0b5-99429657e987" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb0e6c14-5924-40ba-a158-2e21a2f0ef3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "feb3de40-39ed-4b3b-8d62-5ba0662b6479");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0991be93-9fde-4701-aaae-7c201ba8a61b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0ad538cd-3d73-4e0d-a0b5-99429657e987");
        }
    }
}
