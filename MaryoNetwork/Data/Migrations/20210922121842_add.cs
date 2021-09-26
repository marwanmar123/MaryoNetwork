using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_AspNetUsers_ReceiverId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_AspNetUsers_SenderId",
                table: "FriendRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequest",
                table: "FriendRequest");

            migrationBuilder.RenameTable(
                name: "FriendRequest",
                newName: "FriendRequests");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequest_SenderId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequest_ReceiverId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_ReceiverId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "FriendRequests",
                newName: "FriendRequest");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequest",
                newName: "IX_FriendRequest_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "FriendRequest",
                newName: "IX_FriendRequest_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequest",
                table: "FriendRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_AspNetUsers_ReceiverId",
                table: "FriendRequest",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_AspNetUsers_SenderId",
                table: "FriendRequest",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
