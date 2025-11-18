using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$11$pdP4G/NLGIfoWUchnI5vSuOedhMw18xv9Jlh9KPHKfyMn4CryFmoO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$11$lZ5O3J9c.mhnzxP7ZgU/y.apRERx1ehbqebIQYrievr2UmBZVYSR6");
        }
    }
}
