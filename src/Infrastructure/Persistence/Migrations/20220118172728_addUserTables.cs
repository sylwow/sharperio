using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class addUserTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserTablesId",
                table: "Tables",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTables", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_UserTablesId",
                table: "Tables",
                column: "UserTablesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_UserTables_UserTablesId",
                table: "Tables",
                column: "UserTablesId",
                principalTable: "UserTables",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_UserTables_UserTablesId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "UserTables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_UserTablesId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "UserTablesId",
                table: "Tables");
        }
    }
}
