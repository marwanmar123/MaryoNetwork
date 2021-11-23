using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class favoritpostcollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavoritePosts_PostId",
                table: "FavoritePosts");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePosts_PostId",
                table: "FavoritePosts",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavoritePosts_PostId",
                table: "FavoritePosts");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePosts_PostId",
                table: "FavoritePosts",
                column: "PostId",
                unique: true,
                filter: "[PostId] IS NOT NULL");
        }
    }
}
