using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockTracker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ticker = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Security",
                columns: table => new
                {
                    SecurityId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SecurityStatus = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Strike = table.Column<double>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security", x => x.SecurityId);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    TradeId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TradeDate = table.Column<DateTime>(nullable: false),
                    TradeAction = table.Column<int>(nullable: false),
                    TradeContractCount = table.Column<double>(nullable: false),
                    TradePricePerContract = table.Column<double>(nullable: false),
                    SecurityId = table.Column<long>(nullable: true),
                    PositionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.TradeId);
                    table.ForeignKey(
                        name: "FK_Trades_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trades_Security_SecurityId",
                        column: x => x.SecurityId,
                        principalTable: "Security",
                        principalColumn: "SecurityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trades_PositionId",
                table: "Trades",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_SecurityId",
                table: "Trades",
                column: "SecurityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Security");
        }
    }
}
