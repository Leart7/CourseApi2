using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApi.Migrations
{
    /// <inheritdoc />
    public partial class FixedRunTimeSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RuntTime",
                table: "Courses",
                newName: "Runtime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Runtime",
                table: "Courses",
                newName: "RuntTime");
        }
    }
}
