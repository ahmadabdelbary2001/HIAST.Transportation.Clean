using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIAST.Transportation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorSupervisorAndLineStopRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Supervisors_SupervisorId",
                table: "Lines");

            migrationBuilder.DropTable(
                name: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Lines_Name",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "ScheduleType",
                table: "Lines");

            migrationBuilder.AddColumn<int>(
                name: "StopType",
                table: "Stops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusId1",
                table: "Lines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverId1",
                table: "Lines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lines_BusId1",
                table: "Lines",
                column: "BusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_DriverId1",
                table: "Lines",
                column: "DriverId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_Name",
                table: "Lines",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Buses_BusId1",
                table: "Lines",
                column: "BusId1",
                principalTable: "Buses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Drivers_DriverId1",
                table: "Lines",
                column: "DriverId1",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Employees_SupervisorId",
                table: "Lines",
                column: "SupervisorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Buses_BusId1",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Drivers_DriverId1",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Employees_SupervisorId",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_BusId1",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_DriverId1",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_Name",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "StopType",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "BusId1",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "DriverId1",
                table: "Lines");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleType",
                table: "Lines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ContactInfo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervisors_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lines_Name",
                table: "Lines",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_EmployeeId",
                table: "Supervisors",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Supervisors_SupervisorId",
                table: "Lines",
                column: "SupervisorId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
