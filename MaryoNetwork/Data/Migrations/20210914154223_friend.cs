using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class friend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FriendUsersId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendUser");

            migrationBuilder.DropTable(
                name: "Friends");
        }
    }
}
