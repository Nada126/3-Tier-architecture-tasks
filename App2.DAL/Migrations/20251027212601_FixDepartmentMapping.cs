using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App2.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixDepartmentMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Area",
                table: "Department",
                type: "float",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Department",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Department",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Department",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Department",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Department",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Department",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Department",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Department");

            migrationBuilder.AlterColumn<string>(
                name: "Area",
                table: "Department",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 100);
        }
    }
}
