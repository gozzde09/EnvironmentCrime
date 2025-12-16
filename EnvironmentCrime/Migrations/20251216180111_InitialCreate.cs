using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvironmentCrime.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "Errands",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Errands",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                table: "Errands",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrandId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfCrime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfObservation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Errands_DepartmentId",
                table: "Errands",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Errands_EmployeeId",
                table: "Errands",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Errands_StatusId",
                table: "Errands",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Errands_Departments_DepartmentId",
                table: "Errands",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Errands_Employees_EmployeeId",
                table: "Errands",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Errands_ErrandStatuses_StatusId",
                table: "Errands",
                column: "StatusId",
                principalTable: "ErrandStatuses",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Errands_Departments_DepartmentId",
                table: "Errands");

            migrationBuilder.DropForeignKey(
                name: "FK_Errands_Employees_EmployeeId",
                table: "Errands");

            migrationBuilder.DropForeignKey(
                name: "FK_Errands_ErrandStatuses_StatusId",
                table: "Errands");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Errands_DepartmentId",
                table: "Errands");

            migrationBuilder.DropIndex(
                name: "IX_Errands_EmployeeId",
                table: "Errands");

            migrationBuilder.DropIndex(
                name: "IX_Errands_StatusId",
                table: "Errands");

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "Errands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Errands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                table: "Errands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
