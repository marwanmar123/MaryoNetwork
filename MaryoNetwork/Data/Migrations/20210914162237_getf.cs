using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class getf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_FriendUserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_UserId",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Friends",
                newName: "friendToId");

            migrationBuilder.RenameColumn(
                name: "FriendUserId",
                table: "Friends",
                newName: "friendFromId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_UserId",
                table: "Friends",
                newName: "IX_Friends_friendToId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_FriendUserId",
                table: "Friends",
                newName: "IX_Friends_friendFromId");

            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "Friends",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_friendFromId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_friendToId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "friendToId",
                table: "Friends",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "friendFromId",
                table: "Friends",
                newName: "FriendUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_friendToId",
                table: "Friends",
                newName: "IX_Friends_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_friendFromId",
                table: "Friends",
                newName: "IX_Friends_FriendUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_FriendUserId",
                table: "Friends",
                column: "FriendUserId",
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
