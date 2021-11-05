using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class msg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_ToRoomId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ToRoomId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ToRoomId1",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "ToRoomId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToRoomId",
                table: "Messages",
                column: "ToRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_ToRoomId",
                table: "Messages",
                column: "ToRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_ToRoomId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ToRoomId",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "ToRoomId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToRoomId1",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToRoomId1",
                table: "Messages",
                column: "ToRoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_ToRoomId1",
                table: "Messages",
                column: "ToRoomId1",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
