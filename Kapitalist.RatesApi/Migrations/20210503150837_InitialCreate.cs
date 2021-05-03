using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kapitalist.RatesApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatesSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RateType = table.Column<int>(type: "integer", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatesSnapshots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    From = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    To = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    RateAsk = table.Column<decimal>(type: "numeric", nullable: false),
                    RateBid = table.Column<decimal>(type: "numeric", nullable: false),
                    RatesSnapshotId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rate_RatesSnapshots_RatesSnapshotId",
                        column: x => x.RatesSnapshotId,
                        principalTable: "RatesSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rate_RatesSnapshotId",
                table: "Rate",
                column: "RatesSnapshotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "RatesSnapshots");
        }
    }
}
