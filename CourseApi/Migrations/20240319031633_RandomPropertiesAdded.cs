using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApi.Migrations
{
    /// <inheritdoc />
    public partial class RandomPropertiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Courses");

            migrationBuilder.AddColumn<double>(
                name: "Ratings",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ratings",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
