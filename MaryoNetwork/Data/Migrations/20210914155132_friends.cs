using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class friends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendUser");

            migrationBuilder.DropColumn(
                name: "FriendUsersId",
                table: "Friends");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Friends",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendUserId",
                table: "Friends",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendUserId",
                table: "Friends",
                column: "FriendUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserId",
                table: "Friends",
                column: "UserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_FriendUserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_UserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_FriendUserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_UserId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "FriendUserId",
                table: "Friends");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendUsersId",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FriendUser",
                columns: table => new
                {
                    FriendUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendUser", x => new { x.FriendUsersId, x.FriendsId });
                    table.ForeignKey(
                        name: "FK_FriendUser_AspNetUsers_FriendUsersId",
                        column: x => x.FriendUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendUser_Friends_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "Friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendUser_FriendsId",
                table: "FriendUser",
                column: "FriendsId");
        }
    }
}
