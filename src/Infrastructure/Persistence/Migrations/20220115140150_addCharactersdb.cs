using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class addCharactersdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NickName = table.Column<string>(type: "text", nullable: false),
                    Class = table.Column<int>(type: "integer", nullable: false),
                    Server = table.Column<int>(type: "integer", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Strength = table.Column<int>(type: "integer", nullable: false),
                    Dexterity = table.Column<int>(type: "integer", nullable: false),
                    Inteligence = table.Column<int>(type: "integer", nullable: false),
                    HitPoints = table.Column<int>(type: "integer", nullable: false),
                    MaxHitPoints = table.Column<int>(type: "integer", nullable: false),
                    AttackSpeed = table.Column<int>(type: "integer", nullable: false),
                    Healing = table.Column<int>(type: "integer", nullable: false),
                    ManaOrEnergy = table.Column<int>(type: "integer", nullable: false),
                    PhisicalDamage = table.Column<int>(type: "integer", nullable: false),
                    RangeDamage = table.Column<int>(type: "integer", nullable: false),
                    MagicalDamage = table.Column<int>(type: "integer", nullable: false),
                    PoisonDamage = table.Column<int>(type: "integer", nullable: false),
                    Armor = table.Column<int>(type: "integer", nullable: false),
                    MagicalResistance = table.Column<int>(type: "integer", nullable: false),
                    PoisonResistance = table.Column<int>(type: "integer", nullable: false),
                    PhisicalAbsorption = table.Column<int>(type: "integer", nullable: false),
                    MagicalAbsorption = table.Column<int>(type: "integer", nullable: false),
                    Fear = table.Column<int>(type: "integer", nullable: false),
                    PhysicalCriticalHitChange = table.Column<double>(type: "double precision", nullable: false),
                    RangeCriticalHitChange = table.Column<double>(type: "double precision", nullable: false),
                    MagicalCriticalHitChange = table.Column<double>(type: "double precision", nullable: false),
                    PhysicalCriticModifier = table.Column<double>(type: "double precision", nullable: false),
                    RangeCriticModifier = table.Column<double>(type: "double precision", nullable: false),
                    MagicalCriticModifier = table.Column<double>(type: "double precision", nullable: false),
                    ArmorPenetration = table.Column<double>(type: "double precision", nullable: false),
                    DodgeChanse = table.Column<double>(type: "double precision", nullable: false),
                    BlockChanse = table.Column<double>(type: "double precision", nullable: false),
                    PairChanse = table.Column<double>(type: "double precision", nullable: false),
                    Fights = table.Column<int>(type: "integer", nullable: false),
                    Victories = table.Column<int>(type: "integer", nullable: false),
                    Defeats = table.Column<int>(type: "integer", nullable: false),
                    GeneralKills = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
