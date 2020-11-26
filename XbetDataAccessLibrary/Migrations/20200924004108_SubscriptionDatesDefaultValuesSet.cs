using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class SubscriptionDatesDefaultValuesSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTimeStamp",
                table: "Subscriptions",
                nullable: false,
                defaultValueSql: "dateadd(m, 1, getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 10, 24, 2, 37, 6, 486, DateTimeKind.Local).AddTicks(6737));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTimeStamp",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 24, 2, 37, 6, 486, DateTimeKind.Local).AddTicks(6737),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "dateadd(m, 1, getdate())");
        }
    }
}
