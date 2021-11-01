using Microsoft.EntityFrameworkCore.Migrations;

namespace MaryoNetwork.Data.Migrations
{
    public partial class relats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_SkillUsers_SkillUserId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SkillUserId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SkillUserId",
                table: "Skills");

            migrationBuilder.AlterColumn<string>(
                name: "SkillId",
                table: "SkillUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillUsers_SkillId",
                table: "SkillUsers",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillUsers_Skills_SkillId",
                table: "SkillUsers",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillUsers_Skills_SkillId",
                table: "SkillUsers");

            migrationBuilder.DropIndex(
                name: "IX_SkillUsers_SkillId",
                table: "SkillUsers");

            migrationBuilder.AlterColumn<string>(
                name: "SkillId",
                table: "SkillUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkillUserId",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillUserId",
                table: "Skills",
                column: "SkillUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_SkillUsers_SkillUserId",
                table: "Skills",
                column: "SkillUserId",
                principalTable: "SkillUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
