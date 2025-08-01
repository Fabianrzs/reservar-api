using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers.Infrastructure.Migrations.Customer;

/// <inheritdoc />
public partial class Second : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            schema: "customers",
            table: "EstablishmentUsers",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            schema: "customers",
            table: "Establishments",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            schema: "customers",
            table: "Branches",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsActive",
            schema: "customers",
            table: "EstablishmentUsers");

        migrationBuilder.DropColumn(
            name: "IsActive",
            schema: "customers",
            table: "Establishments");

        migrationBuilder.DropColumn(
            name: "IsActive",
            schema: "customers",
            table: "Branches");
    }
}
