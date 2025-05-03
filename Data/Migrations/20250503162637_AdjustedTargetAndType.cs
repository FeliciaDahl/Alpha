using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedTargetAndType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "TargetGroups");

            migrationBuilder.DropColumn(
                name: "NewClient",
                table: "NotificationTypes");

            migrationBuilder.DropColumn(
                name: "NewMember",
                table: "NotificationTypes");

            migrationBuilder.RenameColumn(
                name: "All",
                table: "TargetGroups",
                newName: "TargetGroup");

            migrationBuilder.RenameColumn(
                name: "NewProject",
                table: "NotificationTypes",
                newName: "NotificationType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetGroup",
                table: "TargetGroups",
                newName: "All");

            migrationBuilder.RenameColumn(
                name: "NotificationType",
                table: "NotificationTypes",
                newName: "NewProject");

            migrationBuilder.AddColumn<string>(
                name: "Admin",
                table: "TargetGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewClient",
                table: "NotificationTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewMember",
                table: "NotificationTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
