using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class PlanDescriptionModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Plans");

            migrationBuilder.AddColumn<string>(
                name: "AdventurousTipsAmount",
                table: "Plans",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonotonousTipsAmount",
                table: "Plans",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdventurousTipsAmount",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MonotonousTipsAmount",
                table: "Plans");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Plans",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
