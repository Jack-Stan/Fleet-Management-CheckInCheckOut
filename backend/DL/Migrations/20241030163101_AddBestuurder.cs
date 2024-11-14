using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DL.Migrations
{
    /// <inheritdoc />
    public partial class AddBestuurder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bestuurders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Geboortedatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RijbewijsNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RijbewijsType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bedrijfsnaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedrijfsBTW = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestuurders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bestuurders");
        }
    }
}
