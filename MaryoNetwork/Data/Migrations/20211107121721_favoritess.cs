using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class favoritess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorites_EditorId",
                table: "Favorites");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_EditorId",
                table: "Favorites",
                column: "EditorId",
                unique: true,
                filter: "[EditorId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorites_EditorId",
                table: "Favorites");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_EditorId",
                table: "Favorites",
                column: "EditorId");
        }
    }
}
