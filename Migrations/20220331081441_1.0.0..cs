using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace OpenExchange.Migrations
{
    public partial class _100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EURs",
                columns: table => new
                {
                    EurId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Open = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    Close = table.Column<double>(type: "float", nullable: false),
                    Average = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EURs", x => x.EurId);
                });

            migrationBuilder.CreateTable(
                name: "GBPs",
                columns: table => new
                {
                    GbpId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Open = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    Close = table.Column<double>(type: "float", nullable: false),
                    Average = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GBPs", x => x.GbpId);
                });

            migrationBuilder.CreateTable(
                name: "RatesExs",
                columns: table => new
                {
                    RatestId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Eur = table.Column<double>(type: "float", nullable: false),
                    Gbp = table.Column<double>(type: "float", nullable: false),
                    Rsd = table.Column<double>(type: "float", nullable: false),
                    DateRate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatesExs", x => x.RatestId);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    RatestId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EurId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    GbpId = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.RatestId);
                    table.ForeignKey(
                        name: "FK_Rates_EURs_EurId",
                        column: x => x.EurId,
                        principalTable: "EURs",
                        principalColumn: "EurId");
                    table.ForeignKey(
                        name: "FK_Rates_GBPs_GbpId",
                        column: x => x.GbpId,
                        principalTable: "GBPs",
                        principalColumn: "GbpId");
                });

            migrationBuilder.CreateTable(
                name: "RootsExs",
                columns: table => new
                {
                    RootExId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Disclaimer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<int>(type: "int", nullable: false),
                    @base = table.Column<string>(name: "base", type: "nvarchar(max)", nullable: true),
                    RatestId = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RootsExs", x => x.RootExId);
                    table.ForeignKey(
                        name: "FK_RootsExs_RatesExs_RatestId",
                        column: x => x.RatestId,
                        principalTable: "RatesExs",
                        principalColumn: "RatestId");
                });

            migrationBuilder.CreateTable(
                name: "Roots",
                columns: table => new
                {
                    RootId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Disclaimer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    @base = table.Column<string>(name: "base", type: "nvarchar(max)", nullable: true),
                    RatestId = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roots", x => x.RootId);
                    table.ForeignKey(
                        name: "FK_Roots_Rates_RatestId",
                        column: x => x.RatestId,
                        principalTable: "Rates",
                        principalColumn: "RatestId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_EurId",
                table: "Rates",
                column: "EurId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_GbpId",
                table: "Rates",
                column: "GbpId");

            migrationBuilder.CreateIndex(
                name: "IX_Roots_RatestId",
                table: "Roots",
                column: "RatestId");

            migrationBuilder.CreateIndex(
                name: "IX_RootsExs_RatestId",
                table: "RootsExs",
                column: "RatestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roots");

            migrationBuilder.DropTable(
                name: "RootsExs");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "RatesExs");

            migrationBuilder.DropTable(
                name: "EURs");

            migrationBuilder.DropTable(
                name: "GBPs");
        }
    }
}
