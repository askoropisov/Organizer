using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordMonths",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<string>(type: "TEXT", nullable: true),
                    Eat = table.Column<double>(type: "REAL", nullable: false),
                    Transport = table.Column<double>(type: "REAL", nullable: false),
                    Home = table.Column<double>(type: "REAL", nullable: false),
                    Services = table.Column<double>(type: "REAL", nullable: false),
                    Relaxation = table.Column<double>(type: "REAL", nullable: false),
                    Other = table.Column<double>(type: "REAL", nullable: false),
                    Income = table.Column<double>(type: "REAL", nullable: false),
                    Difference = table.Column<double>(type: "REAL", nullable: false),
                    Total = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordMonths", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordMonths");
        }
    }
}
