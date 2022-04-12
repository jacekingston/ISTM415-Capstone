using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPrototype.Migrations.Team
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
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
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "Losses", "Mascot", "TeamName", "Wins" },
                values: new object[] { 1, 0, "Horse", "Roughriders", 0 });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "Losses", "Mascot", "TeamName", "Wins" },
                values: new object[] { 2, 0, "Hammerhead", "Sharks", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
