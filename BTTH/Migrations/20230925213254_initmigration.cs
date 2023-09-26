using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTTH.Migrations
{
    /// <inheritdoc />
    public partial class initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassCourse",
                columns: table => new
                {
                    Clsid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClsDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClsOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCourse", x => x.Clsid);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCls",
                columns: table => new
                {
                    Sbjid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SbjName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SbjDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SbjOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCls", x => x.Sbjid);
                });

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

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Stdid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StdBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StdTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StdAdr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StdImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clsid = table.Column<int>(type: "int", nullable: false),
                    ClsroomClsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Stdid);
                    table.ForeignKey(
                        name: "FK_Students_ClassCourse_ClsroomClsid",
                        column: x => x.Clsid,
                        principalTable: "ClassCourse",
                        principalColumn: "Clsid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STDbio",
                columns: table => new
                {
                    STDbioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sbjid = table.Column<int>(type: "int", nullable: false),
                    ClassroomIDSbjid = table.Column<int>(type: "int", nullable: false),
                    Stdid = table.Column<int>(type: "int", nullable: false),
                    StudentIDStdid = table.Column<int>(type: "int", nullable: false),
                    ExamMark = table.Column<int>(type: "int", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STDbio", x => x.STDbioId);
                    table.ForeignKey(
                        name: "FK_STDbio_Students_StudentIDStdid",
                        column: x => x.Stdid,
                        principalTable: "Students",
                        principalColumn: "Stdid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STDbio_SubjectCls_ClassroomIDSbjid",
                        column: x => x.Sbjid,
                        principalTable: "SubjectCls",
                        principalColumn: "Sbjid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_STDbio_ClassroomIDSbjid",
                table: "STDbio",
                column: "ClassroomIDSbjid");

            migrationBuilder.CreateIndex(
                name: "IX_STDbio_StudentIDStdid",
                table: "STDbio",
                column: "StudentIDStdid");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClsroomClsid",
                table: "Students",
                column: "ClsroomClsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STDbio");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "SubjectCls");

            migrationBuilder.DropTable(
                name: "ClassCourse");
        }
    }
}
