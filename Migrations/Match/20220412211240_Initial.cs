using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPrototype.Migrations.Match
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "GameId", "Score", "TeamId" },
                values: new object[] { 1, 1, 2, 1 });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "GameId", "Score", "TeamId" },
                values: new object[] { 2, 1, 12, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
