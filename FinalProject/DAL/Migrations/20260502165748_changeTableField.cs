using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeTableField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSalary",
                table: "TeacherSalaries",
                newName: "TotalLessonHours");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Teachers",
                newName: "HourlySalary");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossSalary",
                table: "TeacherSalaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalEarlyLeaveMinutes",
                table: "TeacherSalaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalLateMinutes",
                table: "TeacherSalaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossSalary",
                table: "TeacherSalaries");

            migrationBuilder.DropColumn(
                name: "TotalEarlyLeaveMinutes",
                table: "TeacherSalaries");

            migrationBuilder.DropColumn(
                name: "TotalLateMinutes",
                table: "TeacherSalaries");

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "TotalLessonHours",
                table: "TeacherSalaries",
                newName: "TotalSalary");

            migrationBuilder.RenameColumn(
                name: "HourlySalary",
                table: "Teachers",
                newName: "Salary");
        }
    }
}
