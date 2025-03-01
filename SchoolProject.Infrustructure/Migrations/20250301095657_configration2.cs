using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InsManagerID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InsManagerID",
                table: "Departments",
                column: "InsManagerID",
                principalTable: "Instructors",
                principalColumn: "InsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId",
                principalTable: "Instructors",
                principalColumn: "InsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InsManagerID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InsManagerID",
                table: "Departments",
                column: "InsManagerID",
                principalTable: "Instructors",
                principalColumn: "InsID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId",
                principalTable: "Instructors",
                principalColumn: "InsID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
