using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class statusdatamodif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "RequestStatus");

            migrationBuilder.DropColumn(
                name: "Appending",
                table: "RequestStatus");

            migrationBuilder.RenameColumn(
                name: "Refused",
                table: "RequestStatus",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "RequestStatus",
                newName: "Refused");

            migrationBuilder.AddColumn<string>(
                name: "Accepted",
                table: "RequestStatus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Appending",
                table: "RequestStatus",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
