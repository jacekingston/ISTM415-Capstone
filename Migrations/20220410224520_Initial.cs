using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPrototype.Migrations.Game
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    WinnerScore = table.Column<int>(nullable: false),
                    LoserScore = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    WinnerTeamId = table.Column<int>(nullable: false),
                    LoserTeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "DateTime", "Location", "LoserScore", "LoserTeamId", "WinnerScore", "WinnerTeamId" },
                values: new object[] { 1, new DateTime(2022, 4, 10, 2, 30, 0, 0, DateTimeKind.Unspecified), "College Station, TX", 1, 2, 4, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
