using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_CSharp.Migrations
{
    public partial class AgeOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 18, 52, 16, 265, DateTimeKind.Local).AddTicks(4831),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 40, 5, 142, DateTimeKind.Local).AddTicks(1683));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 40, 5, 142, DateTimeKind.Local).AddTicks(1683),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2022, 1, 22, 18, 52, 16, 265, DateTimeKind.Local).AddTicks(4831));
        }
    }
}
