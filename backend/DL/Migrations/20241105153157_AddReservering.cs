using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DL.Migrations
{
    /// <inheritdoc />
    public partial class AddReservering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reserveringen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckInState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckOutPictures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckInPictures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoertuigId = table.Column<int>(type: "int", nullable: false),
                    BestuurderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserveringen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserveringen_Bestuurders_BestuurderId",
                        column: x => x.BestuurderId,
                        principalTable: "Bestuurders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserveringen_Voertuigen_VoertuigId",
                        column: x => x.VoertuigId,
                        principalTable: "Voertuigen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserveringen_BestuurderId",
                table: "Reserveringen",
                column: "BestuurderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserveringen_VoertuigId",
                table: "Reserveringen",
                column: "VoertuigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserveringen");
        }
    }
}
