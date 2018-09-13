namespace Sales.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    public partial class SalesAddDateDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Sale",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 31, 15, 25, 36, 831, DateTimeKind.Local),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                maxLength: 250,
                nullable: false,
                defaultValue: "No Description",
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true,
                oldDefaultValue: "No Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Sale",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 31, 15, 25, 36, 831, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                maxLength: 250,
                nullable: true,
                defaultValue: "No Description",
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldDefaultValue: "No Description");
        }
    }
}