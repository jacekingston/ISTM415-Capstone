using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPrototype.Migrations.Manager
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Managers",
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
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ManagerId", "Email", "FirstName", "LastName", "Phone", "TeamId" },
                values: new object[] { 1, "mstevens@verizon.net", "Mike", "Stevens", 9723389204L, 1 });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ManagerId", "Email", "FirstName", "LastName", "Phone", "TeamId" },
                values: new object[] { 2, "freemansports@gmail.com", "John", "Freeman", 9725478392L, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Managers");
        }
    }
}
