using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTTH.Migrations
{
    /// <inheritdoc />
    public partial class ClassStudentSubject : Migration
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
                    SbjOrder = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCls", x => x.Sbjid);
                });

            migrationBuilder.CreateTable(
                name: "Student",
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
                    ClassroomidClsid = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Stdid);
                    table.ForeignKey(
                        name: "FK_Student_ClassCourse_ClassroomidClsid",
                        column: x => x.ClassroomidClsid,
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
                    Sbjid = table.Column<int>(type: "int", nullable: true),
                    Stdid = table.Column<int>(type: "int", nullable: true),
                    StudentsStdid = table.Column<int>(type: "int", nullable: false),
                    SubjectClsSbjid = table.Column<int>(type: "int", nullable: false),
                    ExamMark = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STDbio", x => x.STDbioId);
                    table.ForeignKey(
                        name: "FK_STDbio_Student_StudentsStdid",
                        column: x => x.StudentsStdid,
                        principalTable: "Student",
                        principalColumn: "Stdid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STDbio_SubjectCls_SubjectClsSbjid",
                        column: x => x.SubjectClsSbjid,
                        principalTable: "SubjectCls",
                        principalColumn: "Sbjid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_STDbio_StudentsStdid",
                table: "STDbio",
                column: "StudentsStdid");

            migrationBuilder.CreateIndex(
                name: "IX_STDbio_SubjectClsSbjid",
                table: "STDbio",
                column: "SubjectClsSbjid");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassroomidClsid",
                table: "Student",
                column: "ClassroomidClsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STDbio");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "SubjectCls");

            migrationBuilder.DropTable(
                name: "ClassCourse");
        }
    }
}
