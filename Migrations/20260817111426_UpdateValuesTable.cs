using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInfotecsCS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateValuesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ExecutionTime",
                table: "Values",
                type: "numeric(18,4)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Values",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutionTime",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Values");
        }
    }
}
