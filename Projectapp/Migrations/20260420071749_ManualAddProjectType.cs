using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectapp.Migrations
{
    public partial class ManualAddProjectType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // We only ask the DB to add the column. No table creation.
            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Individual");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectType",
                table: "ProjectProposals");
        }
    }
}