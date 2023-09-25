using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTTH.Migrations
{
    /// <inheritdoc />
    public partial class ClassStudentSubject2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubjectCls");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SubjectCls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
