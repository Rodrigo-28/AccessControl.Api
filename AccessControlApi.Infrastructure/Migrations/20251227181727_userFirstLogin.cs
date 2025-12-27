using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessControlApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userFirstLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "first_login",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_login",
                table: "users");
        }
    }
}
