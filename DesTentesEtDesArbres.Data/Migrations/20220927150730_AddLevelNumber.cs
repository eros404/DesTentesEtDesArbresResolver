using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesTentesEtDesArbres.Migrations
{
    public partial class AddLevelNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelDefinitions",
                table: "LevelDefinitions");

            migrationBuilder.AddColumn<uint>(
                name: "Number",
                table: "LevelDefinitions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelDefinitions",
                table: "LevelDefinitions",
                columns: new[] { "Height", "Width", "Letter", "Number" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelDefinitions",
                table: "LevelDefinitions");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "LevelDefinitions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelDefinitions",
                table: "LevelDefinitions",
                columns: new[] { "Height", "Width", "Letter" });
        }
    }
}
