using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTTH.Migrations
{
    /// <inheritdoc />
    public partial class initUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Usid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Usid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
