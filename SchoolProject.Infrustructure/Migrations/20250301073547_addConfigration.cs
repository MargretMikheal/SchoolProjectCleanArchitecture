using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addConfigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudID",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmetSubjects",
                table: "DepartmetSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmetSubjects_DID",
                table: "DepartmetSubjects");

            migrationBuilder.DropColumn(
                name: "StudSubID",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "DeptSubID",
                table: "DepartmetSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "InsManagerID",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                columns: new[] { "StudID", "SubID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmetSubjects",
                table: "DepartmetSubjects",
                columns: new[] { "DID", "SubID" });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    DID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InsID);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_DID",
                        column: x => x.DID,
                        principalTable: "Departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructors_Instructors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructors",
                        principalColumn: "InsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ins_Subject",
                columns: table => new
                {
                    InsID = table.Column<int>(type: "int", nullable: false),
                    SubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ins_Subject", x => new { x.InsID, x.SubID });
                    table.ForeignKey(
                        name: "FK_Ins_Subject_Instructors_InsID",
                        column: x => x.InsID,
                        principalTable: "Instructors",
                        principalColumn: "InsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ins_Subject_Subjects_SubID",
                        column: x => x.SubID,
                        principalTable: "Subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsManagerID",
                table: "Departments",
                column: "InsManagerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ins_Subject_SubID",
                table: "Ins_Subject",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DID",
                table: "Instructors",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InsManagerID",
                table: "Departments",
                column: "InsManagerID",
                principalTable: "Instructors",
                principalColumn: "InsID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InsManagerID",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Ins_Subject");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmetSubjects",
                table: "DepartmetSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InsManagerID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "InsManagerID",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "StudSubID",
                table: "StudentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DeptSubID",
                table: "DepartmetSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                column: "StudSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmetSubjects",
                table: "DepartmetSubjects",
                column: "DeptSubID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudID",
                table: "StudentSubjects",
                column: "StudID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmetSubjects_DID",
                table: "DepartmetSubjects",
                column: "DID");
        }
    }
}
