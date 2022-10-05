using System;
using Airtickets.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airtickets.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:segment_status", "waiting,refunded,realized");

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    TicketNumber = table.Column<string>(type: "text", nullable: false),
                    FlightNum = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<int>(type: "integer", nullable: false),
                    OperationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AirlineCode = table.Column<string>(type: "text", nullable: false),
                    DepartPlace = table.Column<string>(type: "text", nullable: false),
                    DepartDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ArrivePlace = table.Column<string>(type: "text", nullable: false),
                    ArriveDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PnrId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<SegmentStatus>(type: "segment_status", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => new { x.TicketNumber, x.FlightNum });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Segments_TicketNumber_SerialNumber",
                table: "Segments",
                columns: new[] { "TicketNumber", "SerialNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Segments");
        }
    }
}
