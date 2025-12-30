using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessControlApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: 1,
                column: "password",
                value: "$2a$11$E3TFrOyXaIpbgH0IhjUyAuVombZaZt.vOyIkN2s/tdk6NpZZzrL6q");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: 1,
                column: "password",
                value: "$2a$11$zAB6P8IU5GPiDS8KsuQQD.JXp6HzYgiA633NMFhKseBcmRggubZoS");
        }
    }
}
