using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessControlApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class softDeleteRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: 1,
                column: "deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: 2,
                column: "deleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "role");
        }
    }
}
