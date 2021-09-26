using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class ModifFriend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_friendFromId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_friendToId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_friendFromId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "friendFromId",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "friendToId",
                table: "Friends",
                newName: "FriendUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_friendToId",
                table: "Friends",
                newName: "IX_Friends_FriendUserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_FriendUserId",
                table: "Friends",
                column: "FriendUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_FriendUserId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "FriendUserId",
                table: "Friends",
                newName: "friendToId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_FriendUserId",
                table: "Friends",
                newName: "IX_Friends_friendToId");

            migrationBuilder.AddColumn<string>(
                name: "friendFromId",
                table: "Friends",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friends_friendFromId",
                table: "Friends",
                column: "friendFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_friendFromId",
                table: "Friends",
                column: "friendFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_friendToId",
                table: "Friends",
                column: "friendToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
