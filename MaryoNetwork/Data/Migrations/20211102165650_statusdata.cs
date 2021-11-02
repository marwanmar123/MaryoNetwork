using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class statusdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "Friends");

            migrationBuilder.AddColumn<string>(
                name: "RequestStatusId",
                table: "Friends",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Accepted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refused = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Appending = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RequestStatusId",
                table: "Friends",
                column: "RequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_RequestStatus_RequestStatusId",
                table: "Friends",
                column: "RequestStatusId",
                principalTable: "RequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_RequestStatus_RequestStatusId",
                table: "Friends");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropIndex(
                name: "IX_Friends_RequestStatusId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "RequestStatusId",
                table: "Friends");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "Friends",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
