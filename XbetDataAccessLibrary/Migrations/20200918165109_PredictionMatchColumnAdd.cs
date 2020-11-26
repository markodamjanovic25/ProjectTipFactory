using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class PredictionMatchColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Chance",
                table: "Predictions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ClubAwayGoalsHalf",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubHomeGoalsHalf",
                table: "Matches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chance",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "ClubAwayGoalsHalf",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ClubHomeGoalsHalf",
                table: "Matches");
        }
    }
}
