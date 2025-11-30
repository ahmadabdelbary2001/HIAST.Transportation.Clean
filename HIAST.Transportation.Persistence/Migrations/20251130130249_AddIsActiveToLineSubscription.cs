using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIAST.Transportation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToLineSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LineSubscriptions_EmployeeId",
                table: "LineSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IsActive",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LineSubscriptions",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_LineSubscriptions_EmployeeId",
                table: "LineSubscriptions",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LineSubscriptions_EmployeeId",
                table: "LineSubscriptions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LineSubscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineSubscriptions_EmployeeId",
                table: "LineSubscriptions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IsActive",
                table: "Employees",
                column: "IsActive");
        }
    }
}
