using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class addUserToTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Covers_CoverId",
                table: "Tables");

            migrationBuilder.AlterColumn<int>(
                name: "CoverId",
                table: "Tables",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Covers_CoverId",
                table: "Tables",
                column: "CoverId",
                principalTable: "Covers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Covers_CoverId",
                table: "Tables");

            migrationBuilder.AlterColumn<int>(
                name: "CoverId",
                table: "Tables",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Covers_CoverId",
                table: "Tables",
                column: "CoverId",
                principalTable: "Covers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
