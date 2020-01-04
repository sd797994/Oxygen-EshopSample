using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.Infrastructure.Migrations
{
    public partial class InitDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "OrderItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "OrderItem");
        }
    }
}
