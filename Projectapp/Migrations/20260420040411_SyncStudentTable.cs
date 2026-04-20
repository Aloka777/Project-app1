using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectapp.Migrations
{
    /// <inheritdoc />
    public partial class SyncStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResearchAreas_Faculties_FacultyId1",
                table: "ResearchAreas");

            migrationBuilder.DropIndex(
                name: "IX_ResearchAreas_FacultyId1",
                table: "ResearchAreas");

            migrationBuilder.DropColumn(
                name: "FacultyId1",
                table: "ResearchAreas");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ProjectProposals",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "ProjectProposals",
                newName: "Abstract");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIC",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ProposalFilePath",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResearchAreaId",
                table: "ProjectProposals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeamMembers",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposals_ResearchAreaId",
                table: "ProjectProposals",
                column: "ResearchAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposals_ResearchAreas_ResearchAreaId",
                table: "ProjectProposals",
                column: "ResearchAreaId",
                principalTable: "ResearchAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposals_ResearchAreas_ResearchAreaId",
                table: "ProjectProposals");

            migrationBuilder.DropIndex(
                name: "IX_ProjectProposals_ResearchAreaId",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NIC",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProposalFilePath",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "ResearchAreaId",
                table: "ProjectProposals");

            migrationBuilder.DropColumn(
                name: "TeamMembers",
                table: "ProjectProposals");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ProjectProposals",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Abstract",
                table: "ProjectProposals",
                newName: "Category");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId1",
                table: "ResearchAreas",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupervisorId",
                table: "ProjectProposals",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "ProjectProposals",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchAreas_FacultyId1",
                table: "ResearchAreas",
                column: "FacultyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchAreas_Faculties_FacultyId1",
                table: "ResearchAreas",
                column: "FacultyId1",
                principalTable: "Faculties",
                principalColumn: "Id");
        }
    }
}
