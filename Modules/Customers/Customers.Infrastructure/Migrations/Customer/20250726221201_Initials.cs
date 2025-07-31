using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers.Infrastructure.Migrations.Customer;

/// <inheritdoc />
public partial class Initials : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "customers");

        migrationBuilder.CreateTable(
            name: "Establishments",
            schema: "customers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                BannerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ContactInfo_PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                ContactInfo_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ContactInfo_Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ContactInfo_InstagramHandle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ContactInfo_FacebookHandle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ContactInfo_WhatsappNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Establishments", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Branches",
            schema: "customers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                EstablishmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Address_Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Address_Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Address_Neighborhood = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Address_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Address_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Location_Latitude = table.Column<double>(type: "float", nullable: false),
                Location_Longitude = table.Column<double>(type: "float", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Branches", x => x.Id);
                table.ForeignKey(
                    name: "FK_Branches_Establishments_EstablishmentId",
                    column: x => x.EstablishmentId,
                    principalSchema: "customers",
                    principalTable: "Establishments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EstablishmentUsers",
            schema: "customers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EstablishmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EstablishmentUsers", x => x.Id);
                table.ForeignKey(
                    name: "FK_EstablishmentUsers_Establishments_EstablishmentId",
                    column: x => x.EstablishmentId,
                    principalSchema: "customers",
                    principalTable: "Establishments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Branches_EstablishmentId",
            schema: "customers",
            table: "Branches",
            column: "EstablishmentId");

        migrationBuilder.CreateIndex(
            name: "IX_EstablishmentUsers_EstablishmentId_UserId",
            schema: "customers",
            table: "EstablishmentUsers",
            columns: ["EstablishmentId", "UserId"],
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Branches",
            schema: "customers");

        migrationBuilder.DropTable(
            name: "EstablishmentUsers",
            schema: "customers");

        migrationBuilder.DropTable(
            name: "Establishments",
            schema: "customers");
    }
}
