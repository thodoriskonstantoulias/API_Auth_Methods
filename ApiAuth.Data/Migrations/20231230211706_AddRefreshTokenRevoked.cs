using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAuth.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenRevoked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Revoked",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revoked",
                table: "RefreshTokens");
        }
    }
}
