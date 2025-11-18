using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning.Migrations
{
    /// <inheritdoc />
    public partial class AdminSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$11$lK5azdgMZW59rr9wwFDInuttJaXEUpLEEnIWkmXF9RVUBXGByip/m");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$11$sd9Ey5fvffQKvoV6R4yYkOaXyAUP9HedjuF3tS9ZSaBpPb8PtP4aS");
        }
    }
}
