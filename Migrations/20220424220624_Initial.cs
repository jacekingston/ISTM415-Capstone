using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPrototype.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(nullable: false),
                    Mascot = table.Column<string>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ManagerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Phone = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ManagerId);
                    table.ForeignKey(
                        name: "FK_Manager_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    Outcome = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Match_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Match_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
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
                    Position = table.Column<string>(nullable: false),
                    NumAtBats = table.Column<int>(nullable: false),
                    NumHits = table.Column<int>(nullable: false),
                    NumHittingStrikeouts = table.Column<int>(nullable: false),
                    NumHomeruns = table.Column<int>(nullable: false),
                    NumRBI = table.Column<int>(nullable: false),
                    NumWalks = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "TeamId", "Losses", "Mascot", "TeamName", "Wins" },
                values: new object[,]
                {
                    { 1, 0, "Horse", "Roughriders", 0 },
                    { 2, 0, "Hammerhead", "Sharks", 0 },
                    { 3, 0, "Brett the Bear", "Bad News Bears", 0 },
                    { 4, 0, "Keeth", "The Hitmen", 0 },
                    { 5, 0, "Beevo", "Bisons", 0 },
                    { 6, 0, "Batkid", "Bat Attitudes", 0 },
                    { 7, 0, "McLovin", "Sliders", 0 },
                    { 8, 0, "Mr. Electro", "Lightning", 0 }
                });

            migrationBuilder.InsertData(
                table: "Manager",
                columns: new[] { "ManagerId", "Email", "FirstName", "LastName", "Phone", "TeamId" },
                values: new object[,]
                {
                    { 1, "mstevens@verizon.net", "Mike", "Stevens", 9723389204L, 1 },
                    { 2, "freemansports@gmail.com", "John", "Freeman", 9725478392L, 2 },
                    { 3, "DubWalker@gmail.com", "Ryan", "Walker", 9725478392L, 3 },
                    { 4, "goated@gmail.com", "Joe", "Burrow", 9725478392L, 4 },
                    { 5, "Deflate@gmail.com", "Tom", "Brady", 9725478392L, 5 },
                    { 6, "OldBrett@gmail.com", "Brett", "Favre", 9725478392L, 6 },
                    { 7, "TreyK@gmail.com", "Trey", "Kingston", 9725478392L, 7 },
                    { 8, "KadeK@gmail.com", "Kade", "Kingston", 9725478392L, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manager_TeamId",
                table: "Manager",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_GameId",
                table: "Match",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TeamId",
                table: "Match",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamId",
                table: "Player",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
