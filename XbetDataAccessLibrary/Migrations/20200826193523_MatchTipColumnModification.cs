using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class MatchTipColumnModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Matches_MatchId",
                table: "Predictions");

            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Tips_TipId",
                table: "Predictions");

            migrationBuilder.AlterColumn<int>(
                name: "TipId",
                table: "Predictions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "Predictions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Matches_MatchId",
                table: "Predictions",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Tips_TipId",
                table: "Predictions",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "TipId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Matches_MatchId",
                table: "Predictions");

            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Tips_TipId",
                table: "Predictions");

            migrationBuilder.AlterColumn<int>(
                name: "TipId",
                table: "Predictions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "Predictions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Matches_MatchId",
                table: "Predictions",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Tips_TipId",
                table: "Predictions",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "TipId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
