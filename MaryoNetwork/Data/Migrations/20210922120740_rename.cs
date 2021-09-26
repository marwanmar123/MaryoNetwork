using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_FriendId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_UserId",
                table: "Friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friends",
                table: "Friends");

            migrationBuilder.RenameTable(
                name: "Friends",
                newName: "UserFriend");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_UserId",
                table: "UserFriend",
                newName: "IX_UserFriend_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_FriendId",
                table: "UserFriend",
                newName: "IX_UserFriend_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriend",
                table: "UserFriend",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_AspNetUsers_FriendId",
                table: "UserFriend",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_AspNetUsers_UserId",
                table: "UserFriend",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_AspNetUsers_FriendId",
                table: "UserFriend");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_AspNetUsers_UserId",
                table: "UserFriend");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriend",
                table: "UserFriend");

            migrationBuilder.RenameTable(
                name: "UserFriend",
                newName: "Friends");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriend_UserId",
                table: "Friends",
                newName: "IX_Friends_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriend_FriendId",
                table: "Friends",
                newName: "IX_Friends_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friends",
                table: "Friends",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_FriendId",
                table: "Friends",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_UserId",
                table: "Friends",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
