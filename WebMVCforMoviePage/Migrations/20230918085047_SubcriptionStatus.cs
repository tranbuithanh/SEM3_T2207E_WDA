using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVCforMoviePage.Migrations
{
    /// <inheritdoc />
    public partial class SubcriptionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubcriptionStatus",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubcriptionStatus",
                table: "users");
        }
    }
}
