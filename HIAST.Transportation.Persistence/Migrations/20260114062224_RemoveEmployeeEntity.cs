using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIAST.Transportation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEmployeeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Employees_SupervisorId",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_LineSubscriptions_Employees_EmployeeId",
                table: "LineSubscriptions");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_LineSubscriptions_EmployeeId",
                table: "LineSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_LineSubscriptions_EmployeeId_LineId_StartDate",
                table: "LineSubscriptions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LineSubscriptions");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeUserId",
                table: "LineSubscriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "Lines",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineSubscriptions_EmployeeUserId",
                table: "LineSubscriptions",
                column: "EmployeeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LineSubscriptions_EmployeeUserId_LineId_StartDate",
                table: "LineSubscriptions",
                columns: new[] { "EmployeeUserId", "LineId", "StartDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LineSubscriptions_EmployeeUserId",
                table: "LineSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_LineSubscriptions_EmployeeUserId_LineId_StartDate",
                table: "LineSubscriptions");

            migrationBuilder.DropColumn(
                name: "EmployeeUserId",
                table: "LineSubscriptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "LineSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SupervisorId",
                table: "Lines",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineSubscriptions_EmployeeId",
                table: "LineSubscriptions",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineSubscriptions_EmployeeId_LineId_StartDate",
                table: "LineSubscriptions",
                columns: new[] { "EmployeeId", "LineId", "StartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeNumber",
                table: "Employees",
                column: "EmployeeNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Employees_SupervisorId",
                table: "Lines",
                column: "SupervisorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LineSubscriptions_Employees_EmployeeId",
                table: "LineSubscriptions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
