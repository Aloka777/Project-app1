using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectapp.Migrations
{
    /// <inheritdoc />
    public partial class FinalSupervisorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResearchAreaId",
                table: "SupervisorInterests",
                newName: "ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Expertise",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "SupervisorInterests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SupervisorInterests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorInterests_ProjectId",
                table: "SupervisorInterests",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorInterests_ProjectProposals_ProjectId",
                table: "SupervisorInterests",
                column: "ProjectId",
                principalTable: "ProjectProposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorInterests_ProjectProposals_ProjectId",
                table: "SupervisorInterests");

            migrationBuilder.DropIndex(
                name: "IX_SupervisorInterests_ProjectId",
                table: "SupervisorInterests");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Expertise",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SupervisorInterests");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "SupervisorInterests",
                newName: "ResearchAreaId");

            migrationBuilder.AlterColumn<int>(
                name: "SupervisorId",
                table: "SupervisorInterests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
