using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactPro.Migrations
{
    public partial class UpdateCategoryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Categories",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "MyProperty");
        }
    }
}
