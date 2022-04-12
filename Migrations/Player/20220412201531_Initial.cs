using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPrototype.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    NumAtBats = table.Column<int>(nullable: false),
                    NumHits = table.Column<int>(nullable: false),
                    NumHittingStrikeouts = table.Column<int>(nullable: false),
                    NumHomeruns = table.Column<int>(nullable: false),
                    NumRBI = table.Column<int>(nullable: false),
                    NumWalks = table.Column<int>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    NumPlays = table.Column<int>(nullable: false),
                    NumErrors = table.Column<int>(nullable: false),
                    NumInningsPitched = table.Column<int>(nullable: false),
                    NumEarnedRunsAllowed = table.Column<int>(nullable: false),
                    NumWalksAllowed = table.Column<int>(nullable: false),
                    NumPitchingStrikeouts = table.Column<int>(nullable: false),
                    NumHomerunsAllowed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "DOB", "FirstName", "Height", "LastName", "NumAtBats", "NumEarnedRunsAllowed", "NumErrors", "NumHits", "NumHittingStrikeouts", "NumHomeruns", "NumHomerunsAllowed", "NumInningsPitched", "NumPitchingStrikeouts", "NumPlays", "NumRBI", "NumWalks", "NumWalksAllowed", "Position", "TeamId", "Weight" },
                values: new object[] { 1, new DateTime(2013, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max", 49, "Smith", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "C", 1, 65 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "DOB", "FirstName", "Height", "LastName", "NumAtBats", "NumEarnedRunsAllowed", "NumErrors", "NumHits", "NumHittingStrikeouts", "NumHomeruns", "NumHomerunsAllowed", "NumInningsPitched", "NumPitchingStrikeouts", "NumPlays", "NumRBI", "NumWalks", "NumWalksAllowed", "Position", "TeamId", "Weight" },
                values: new object[] { 2, new DateTime(2013, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jackson", 57, "Frome", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "RF", 2, 91 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
