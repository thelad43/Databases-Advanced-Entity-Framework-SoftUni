namespace BusTicketsSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    public partial class ArrivedTripsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArrivedTrips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualArrivalTime = table.Column<DateTime>(nullable: false),
                    OriginBusStationId = table.Column<int>(nullable: true),
                    DestinationBusStationId = table.Column<int>(nullable: true),
                    PassengersCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrivedTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArrivedTrips_BusStations_DestinationBusStationId",
                        column: x => x.DestinationBusStationId,
                        principalTable: "BusStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArrivedTrips_BusStations_OriginBusStationId",
                        column: x => x.OriginBusStationId,
                        principalTable: "BusStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArrivedTrips_DestinationBusStationId",
                table: "ArrivedTrips",
                column: "DestinationBusStationId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrivedTrips_OriginBusStationId",
                table: "ArrivedTrips",
                column: "OriginBusStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArrivedTrips");
        }
    }
}