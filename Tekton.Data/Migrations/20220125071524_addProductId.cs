using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tekton.Data.Migrations
{
    public partial class addProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Master",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductMockId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductMockId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Master",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
