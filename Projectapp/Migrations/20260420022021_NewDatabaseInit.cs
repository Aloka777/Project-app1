using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectapp.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabaseInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "ResearchArea",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "SupervisorName",
                table: "ProjectProposals");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "ProjectProposals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "ProjectProposals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterKeywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterKeywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectKeywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    KeywordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectKeywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    ResearchAreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorInterests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    FacultyId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchAreas_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResearchAreas_Faculties_FacultyId1",
                        column: x => x.FacultyId1,
                        principalTable: "Faculties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResearchAreas_FacultyId",
                table: "ResearchAreas",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchAreas_FacultyId1",
                table: "ResearchAreas",
                column: "FacultyId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterKeywords");

            migrationBuilder.DropTable(
                name: "ProjectKeywords");

            migrationBuilder.DropTable(
                name: "ResearchAreas");

            migrationBuilder.DropTable(
                name: "SupervisorInterests");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "ProjectProposals");

            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResearchArea",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SupervisorName",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
