using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOfNotificationEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDismiss_AspNetUsers_UserId",
                table: "NotificationDismiss");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDismiss_Notifications_NotificationId",
                table: "NotificationDismiss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationDismiss",
                table: "NotificationDismiss");

            migrationBuilder.RenameTable(
                name: "NotificationDismiss",
                newName: "DismissedNotifications");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationDismiss_UserId",
                table: "DismissedNotifications",
                newName: "IX_DismissedNotifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationDismiss_NotificationId",
                table: "DismissedNotifications",
                newName: "IX_DismissedNotifications_NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DismissedNotifications",
                table: "DismissedNotifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DismissedNotifications_AspNetUsers_UserId",
                table: "DismissedNotifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DismissedNotifications_Notifications_NotificationId",
                table: "DismissedNotifications",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DismissedNotifications_AspNetUsers_UserId",
                table: "DismissedNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_DismissedNotifications_Notifications_NotificationId",
                table: "DismissedNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DismissedNotifications",
                table: "DismissedNotifications");

            migrationBuilder.RenameTable(
                name: "DismissedNotifications",
                newName: "NotificationDismiss");

            migrationBuilder.RenameIndex(
                name: "IX_DismissedNotifications_UserId",
                table: "NotificationDismiss",
                newName: "IX_NotificationDismiss_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DismissedNotifications_NotificationId",
                table: "NotificationDismiss",
                newName: "IX_NotificationDismiss_NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationDismiss",
                table: "NotificationDismiss",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationDismiss_AspNetUsers_UserId",
                table: "NotificationDismiss",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationDismiss_Notifications_NotificationId",
                table: "NotificationDismiss",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
