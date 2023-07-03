using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackBackend.Migrations
{
    /// <inheritdoc />
    public partial class migration_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "orderSheets",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orderStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderSheetId = table.Column<int>(type: "INTEGER", nullable: true),
                    stage = table.Column<int>(type: "INTEGER", nullable: false),
                    LengthCompleted = table.Column<double>(type: "REAL", nullable: true),
                    LengthUnit = table.Column<int>(type: "INTEGER", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderStages_orderSheets_OrderSheetId",
                        column: x => x.OrderSheetId,
                        principalTable: "orderSheets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "orderManufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderSheetId = table.Column<int>(type: "INTEGER", nullable: true),
                    ManufacturerId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderManufacturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderManufacturers_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_orderManufacturers_orderSheets_OrderSheetId",
                        column: x => x.OrderSheetId,
                        principalTable: "orderSheets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "orderPriorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ManufacturerId = table.Column<int>(type: "INTEGER", nullable: true),
                    PriorityName = table.Column<string>(type: "TEXT", nullable: true),
                    DaysRequiredForProcessing = table.Column<double>(type: "REAL", nullable: true),
                    LinesPerDay = table.Column<double>(type: "REAL", nullable: true),
                    DaysRequiredForTrimming = table.Column<double>(type: "REAL", nullable: true),
                    DaysRequiredForWashing = table.Column<double>(type: "REAL", nullable: true),
                    DaysRequiredForFinalTriming = table.Column<double>(type: "REAL", nullable: true),
                    DaysRequiredForFinishing = table.Column<double>(type: "REAL", nullable: true),
                    DaysRequiredForPackaging = table.Column<double>(type: "REAL", nullable: true),
                    DaysRequiredForShipping = table.Column<double>(type: "REAL", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderPriorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderPriorities_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "orderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OrderSheetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderPriorityId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderStatuses_orderPriorities_OrderPriorityId",
                        column: x => x.OrderPriorityId,
                        principalTable: "orderPriorities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_orderStatuses_orderSheets_OrderSheetId",
                        column: x => x.OrderSheetId,
                        principalTable: "orderSheets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderManufacturers_ManufacturerId",
                table: "orderManufacturers",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_orderManufacturers_OrderSheetId",
                table: "orderManufacturers",
                column: "OrderSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_orderPriorities_ManufacturerId",
                table: "orderPriorities",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_orderStages_OrderSheetId",
                table: "orderStages",
                column: "OrderSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_orderStatuses_OrderPriorityId",
                table: "orderStatuses",
                column: "OrderPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_orderStatuses_OrderSheetId",
                table: "orderStatuses",
                column: "OrderSheetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderManufacturers");

            migrationBuilder.DropTable(
                name: "orderStages");

            migrationBuilder.DropTable(
                name: "orderStatuses");

            migrationBuilder.DropTable(
                name: "orderPriorities");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "orderSheets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }
    }
}
