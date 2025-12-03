using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serenity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGoogleIdAndAvatarToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "google_id",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "google_id",
                table: "users");
        }
    }
}
