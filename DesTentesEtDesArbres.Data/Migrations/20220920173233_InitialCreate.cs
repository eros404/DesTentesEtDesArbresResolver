using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesTentesEtDesArbres.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelDefinitions",
                columns: table => new
                {
                    Height = table.Column<uint>(type: "INTEGER", nullable: false),
                    Width = table.Column<uint>(type: "INTEGER", nullable: false),
                    Letter = table.Column<char>(type: "TEXT", nullable: false),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    SerializedPlaygroundInitializer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelDefinitions", x => new { x.Height, x.Width, x.Letter });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelDefinitions");
        }
    }
}
