using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTTH.Migrations
{
    /// <inheritdoc />
    public partial class ClassStudentSubject3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STDbio_Student_StudentsStdid",
                table: "STDbio");

            migrationBuilder.DropForeignKey(
                name: "FK_STDbio_SubjectCls_SubjectClsSbjid",
                table: "STDbio");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_ClassCourse_ClassroomidClsid",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_ClassroomidClsid",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_STDbio_StudentsStdid",
                table: "STDbio");

            migrationBuilder.DropIndex(
                name: "IX_STDbio_SubjectClsSbjid",
                table: "STDbio");

            migrationBuilder.DropColumn(
                name: "ClassroomidClsid",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentsStdid",
                table: "STDbio");

            migrationBuilder.DropColumn(
                name: "SubjectClsSbjid",
                table: "STDbio");

            migrationBuilder.AlterColumn<int>(
                name: "Stdid",
                table: "STDbio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sbjid",
                table: "STDbio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_Clsid",
                table: "Student",
                column: "Clsid");

            migrationBuilder.CreateIndex(
                name: "IX_STDbio_Sbjid",
                table: "STDbio",
                column: "Sbjid");

            migrationBuilder.CreateIndex(
                name: "IX_STDbio_Stdid",
                table: "STDbio",
                column: "Stdid");

            migrationBuilder.AddForeignKey(
                name: "FK_STDbio_Student_Stdid",
                table: "STDbio",
                column: "Stdid",
                principalTable: "Student",
                principalColumn: "Stdid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STDbio_SubjectCls_Sbjid",
                table: "STDbio",
                column: "Sbjid",
                principalTable: "SubjectCls",
                principalColumn: "Sbjid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_ClassCourse_Clsid",
                table: "Student",
                column: "Clsid",
                principalTable: "ClassCourse",
                principalColumn: "Clsid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STDbio_Student_Stdid",
                table: "STDbio");

            migrationBuilder.DropForeignKey(
                name: "FK_STDbio_SubjectCls_Sbjid",
                table: "STDbio");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_ClassCourse_Clsid",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_Clsid",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_STDbio_Sbjid",
                table: "STDbio");

            migrationBuilder.DropIndex(
                name: "IX_STDbio_Stdid",
                table: "STDbio");

            migrationBuilder.AddColumn<int>(
                name: "ClassroomidClsid",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Stdid",
                table: "STDbio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Sbjid",
                table: "STDbio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StudentsStdid",
                table: "STDbio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectClsSbjid",
                table: "STDbio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassroomidClsid",
                table: "Student",
                column: "ClassroomidClsid");

            migrationBuilder.CreateIndex(
                name: "IX_STDbio_StudentsStdid",
                table: "STDbio",
                column: "StudentsStdid");

            migrationBuilder.CreateIndex(
                name: "IX_STDbio_SubjectClsSbjid",
                table: "STDbio",
                column: "SubjectClsSbjid");

            migrationBuilder.AddForeignKey(
                name: "FK_STDbio_Student_StudentsStdid",
                table: "STDbio",
                column: "StudentsStdid",
                principalTable: "Student",
                principalColumn: "Stdid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STDbio_SubjectCls_SubjectClsSbjid",
                table: "STDbio",
                column: "SubjectClsSbjid",
                principalTable: "SubjectCls",
                principalColumn: "Sbjid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_ClassCourse_ClassroomidClsid",
                table: "Student",
                column: "ClassroomidClsid",
                principalTable: "ClassCourse",
                principalColumn: "Clsid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
