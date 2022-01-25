using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharperioBackend.Infrastructure.Persistence.Migrations
{
    public partial class removeWorkspace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accesses_Workspace_WorkspaceId",
                table: "Accesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Workspace_WorkspaceId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "Workspace");

            migrationBuilder.DropIndex(
                name: "IX_Tables_WorkspaceId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Accesses_WorkspaceId",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Accesses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Tables",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Accesses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspace", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_WorkspaceId",
                table: "Tables",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_WorkspaceId",
                table: "Accesses",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accesses_Workspace_WorkspaceId",
                table: "Accesses",
                column: "WorkspaceId",
                principalTable: "Workspace",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Workspace_WorkspaceId",
                table: "Tables",
                column: "WorkspaceId",
                principalTable: "Workspace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
