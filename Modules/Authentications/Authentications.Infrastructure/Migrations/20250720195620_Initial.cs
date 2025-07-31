using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentications.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "auth");

        migrationBuilder.CreateTable(
            name: "OtpTokens",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Token = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_OtpTokens", x => x.Id));

        migrationBuilder.CreateTable(
            name: "PasswordResetTokens",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Token = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_PasswordResetTokens", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Permissions",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Permissions", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Roles", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Sessions",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                Ip = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                UserAgent = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Sessions", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Users",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                AuthProvider = table.Column<int>(type: "int", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Users", x => x.Id));

        migrationBuilder.CreateTable(
            name: "RolesPermissions",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RolesPermissions", x => new { x.Id, x.RoleId, x.PermissionId });
                table.ForeignKey(
                    name: "FK_RolesPermissions_Permissions_PermissionId",
                    column: x => x.PermissionId,
                    principalSchema: "auth",
                    principalTable: "Permissions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_RolesPermissions_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "auth",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserCredentials",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                PasswordSetAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserCredentials", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserCredentials_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "auth",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UsersRoles",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsersRoles", x => new { x.Id, x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_UsersRoles_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "auth",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UsersRoles_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "auth",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PasswordHistories",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                SetAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UserCredentialsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PasswordHistories", x => x.Id);
                table.ForeignKey(
                    name: "FK_PasswordHistories_UserCredentials_UserCredentialsId",
                    column: x => x.UserCredentialsId,
                    principalSchema: "auth",
                    principalTable: "UserCredentials",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PasswordHistories_UserCredentialsId",
            schema: "auth",
            table: "PasswordHistories",
            column: "UserCredentialsId");

        migrationBuilder.CreateIndex(
            name: "IX_PasswordResetTokens_Token",
            schema: "auth",
            table: "PasswordResetTokens",
            column: "Token",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Permissions_Code",
            schema: "auth",
            table: "Permissions",
            column: "Code",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Roles_Name",
            schema: "auth",
            table: "Roles",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_RolesPermissions_PermissionId",
            schema: "auth",
            table: "RolesPermissions",
            column: "PermissionId");

        migrationBuilder.CreateIndex(
            name: "IX_RolesPermissions_RoleId",
            schema: "auth",
            table: "RolesPermissions",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_UserCredentials_UserId",
            schema: "auth",
            table: "UserCredentials",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UsersRoles_RoleId",
            schema: "auth",
            table: "UsersRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_UsersRoles_UserId",
            schema: "auth",
            table: "UsersRoles",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "OtpTokens",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "PasswordHistories",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "PasswordResetTokens",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "RolesPermissions",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "Sessions",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "UsersRoles",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "UserCredentials",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "Permissions",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "Users",
            schema: "auth");
    }
}
