using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessControlApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "deleted", "email", "first_login", "password", "role_id", "username" },
                values: new object[] { 1, false, "admin@admin.com", false, "$2a$11$zAB6P8IU5GPiDS8KsuQQD.JXp6HzYgiA633NMFhKseBcmRggubZoS", 1, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: 1);
        }
    }
}
