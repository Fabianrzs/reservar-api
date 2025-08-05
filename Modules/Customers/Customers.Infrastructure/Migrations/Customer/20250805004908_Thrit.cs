using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers.Infrastructure.Migrations.Customer;

/// <inheritdoc />
public partial class Thrit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "ContactInfo_WhatsappNumber",
            schema: "customers",
            table: "Establishments",
            newName: "WhatsappNumber");

        migrationBuilder.RenameColumn(
            name: "ContactInfo_Website",
            schema: "customers",
            table: "Establishments",
            newName: "Website");

        migrationBuilder.RenameColumn(
            name: "ContactInfo_PhoneNumber",
            schema: "customers",
            table: "Establishments",
            newName: "PhoneNumber");

        migrationBuilder.RenameColumn(
            name: "ContactInfo_InstagramHandle",
            schema: "customers",
            table: "Establishments",
            newName: "InstagramHandle");

        migrationBuilder.RenameColumn(
            name: "ContactInfo_FacebookHandle",
            schema: "customers",
            table: "Establishments",
            newName: "FacebookHandle");

        migrationBuilder.RenameColumn(
            name: "ContactInfo_Email",
            schema: "customers",
            table: "Establishments",
            newName: "Email");

        migrationBuilder.RenameColumn(
            name: "Location_Longitude",
            schema: "customers",
            table: "Branches",
            newName: "Longitude");

        migrationBuilder.RenameColumn(
            name: "Location_Latitude",
            schema: "customers",
            table: "Branches",
            newName: "Latitude");

        migrationBuilder.RenameColumn(
            name: "Address_ZipCode",
            schema: "customers",
            table: "Branches",
            newName: "ZipCode");

        migrationBuilder.RenameColumn(
            name: "Address_Street",
            schema: "customers",
            table: "Branches",
            newName: "Street");

        migrationBuilder.RenameColumn(
            name: "Address_State",
            schema: "customers",
            table: "Branches",
            newName: "State");

        migrationBuilder.RenameColumn(
            name: "Address_Number",
            schema: "customers",
            table: "Branches",
            newName: "Number");

        migrationBuilder.RenameColumn(
            name: "Address_Neighborhood",
            schema: "customers",
            table: "Branches",
            newName: "Neighborhood");

        migrationBuilder.RenameColumn(
            name: "Address_Country",
            schema: "customers",
            table: "Branches",
            newName: "Country");

        migrationBuilder.RenameColumn(
            name: "Address_City",
            schema: "customers",
            table: "Branches",
            newName: "City");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "WhatsappNumber",
            schema: "customers",
            table: "Establishments",
            newName: "ContactInfo_WhatsappNumber");

        migrationBuilder.RenameColumn(
            name: "Website",
            schema: "customers",
            table: "Establishments",
            newName: "ContactInfo_Website");

        migrationBuilder.RenameColumn(
            name: "PhoneNumber",
            schema: "customers",
            table: "Establishments",
            newName: "ContactInfo_PhoneNumber");

        migrationBuilder.RenameColumn(
            name: "InstagramHandle",
            schema: "customers",
            table: "Establishments",
            newName: "ContactInfo_InstagramHandle");

        migrationBuilder.RenameColumn(
            name: "FacebookHandle",
            schema: "customers",
            table: "Establishments",
            newName: "ContactInfo_FacebookHandle");

        migrationBuilder.RenameColumn(
            name: "Email",
            schema: "customers",
            table: "Establishments",
            newName: "ContactInfo_Email");

        migrationBuilder.RenameColumn(
            name: "ZipCode",
            schema: "customers",
            table: "Branches",
            newName: "Address_ZipCode");

        migrationBuilder.RenameColumn(
            name: "Street",
            schema: "customers",
            table: "Branches",
            newName: "Address_Street");

        migrationBuilder.RenameColumn(
            name: "State",
            schema: "customers",
            table: "Branches",
            newName: "Address_State");

        migrationBuilder.RenameColumn(
            name: "Number",
            schema: "customers",
            table: "Branches",
            newName: "Address_Number");

        migrationBuilder.RenameColumn(
            name: "Neighborhood",
            schema: "customers",
            table: "Branches",
            newName: "Address_Neighborhood");

        migrationBuilder.RenameColumn(
            name: "Longitude",
            schema: "customers",
            table: "Branches",
            newName: "Location_Longitude");

        migrationBuilder.RenameColumn(
            name: "Latitude",
            schema: "customers",
            table: "Branches",
            newName: "Location_Latitude");

        migrationBuilder.RenameColumn(
            name: "Country",
            schema: "customers",
            table: "Branches",
            newName: "Address_Country");

        migrationBuilder.RenameColumn(
            name: "City",
            schema: "customers",
            table: "Branches",
            newName: "Address_City");
    }
}
