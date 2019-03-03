using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoKeg.DataTransfer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PulseCounts",
                columns: table => new
                {
                    PulseCountId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCounted = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    IsProcessed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PulseCounts", x => x.PulseCountId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PulseCounts");
        }
    }
}
