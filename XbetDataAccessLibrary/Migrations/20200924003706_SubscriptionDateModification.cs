using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class SubscriptionDateModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTimeStamp",
                table: "Subscriptions",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 9, 24, 2, 32, 19, 774, DateTimeKind.Local).AddTicks(6624));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTimeStamp",
                table: "Subscriptions",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 24, 2, 37, 6, 486, DateTimeKind.Local).AddTicks(6737),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 10, 24, 2, 32, 19, 778, DateTimeKind.Local).AddTicks(9575));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTimeStamp",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 24, 2, 32, 19, 774, DateTimeKind.Local).AddTicks(6624),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTimeStamp",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 24, 2, 32, 19, 778, DateTimeKind.Local).AddTicks(9575),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 10, 24, 2, 37, 6, 486, DateTimeKind.Local).AddTicks(6737));
        }
    }
}
