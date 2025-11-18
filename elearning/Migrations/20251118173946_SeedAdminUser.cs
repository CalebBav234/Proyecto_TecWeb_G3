using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CurrentJwtId", "Email", "PasswordHash", "RefreshToken", "RefreshTokenExpiresAt", "RefreshTokenRevokedAt", "Role", "Username" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), null, "admin@elearning.com", "$2a$11$HQmbwa4IHccNHANlvZ6zMOdy2x6jPbuHIYsZ3bGKHpSjpvkZmcBRW", null, null, null, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "AvatarUrl", "Bio", "FullName", "UserId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), null, "System Administrator", "Administrator", new Guid("00000000-0000-0000-0000-000000000001") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
