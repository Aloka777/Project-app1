using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectapp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKeywordSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResearchAreaId",
                table: "MasterKeywords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MasterKeywords_ResearchAreaId",
                table: "MasterKeywords",
                column: "ResearchAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterKeywords_ResearchAreas_ResearchAreaId",
                table: "MasterKeywords",
                column: "ResearchAreaId",
                principalTable: "ResearchAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterKeywords_ResearchAreas_ResearchAreaId",
                table: "MasterKeywords");

            migrationBuilder.DropIndex(
                name: "IX_MasterKeywords_ResearchAreaId",
                table: "MasterKeywords");

            migrationBuilder.DropColumn(
                name: "ResearchAreaId",
                table: "MasterKeywords");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
