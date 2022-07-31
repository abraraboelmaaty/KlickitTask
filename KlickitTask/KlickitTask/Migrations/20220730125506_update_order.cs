using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlickitTask.Migrations
{
    public partial class update_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
