using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireForecasting.DAL.Migrations
{
    public partial class FireBrige : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FireBrigadeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DutyShift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispatcherId = table.Column<int>(type: "int", nullable: true),
                    ShiftSupervisorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyShift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DutyShift_Divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyShift_Employees_DispatcherId",
                        column: x => x.DispatcherId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyShift_Employees_ShiftSupervisorId",
                        column: x => x.ShiftSupervisorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FireBrigade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FireTruckId = table.Column<int>(type: "int", nullable: true),
                    DutyShiftId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireBrigade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireBrigade_DutyShift_DutyShiftId",
                        column: x => x.DutyShiftId,
                        principalTable: "DutyShift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FireBrigade_FireTruckBase_FireTruckId",
                        column: x => x.FireTruckId,
                        principalTable: "FireTruckBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FireBrigadeId",
                table: "Employees",
                column: "FireBrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyShift_DispatcherId",
                table: "DutyShift",
                column: "DispatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyShift_DivisionId",
                table: "DutyShift",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyShift_ShiftSupervisorId",
                table: "DutyShift",
                column: "ShiftSupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_FireBrigade_DutyShiftId",
                table: "FireBrigade",
                column: "DutyShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_FireBrigade_FireTruckId",
                table: "FireBrigade",
                column: "FireTruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FireBrigade_FireBrigadeId",
                table: "Employees",
                column: "FireBrigadeId",
                principalTable: "FireBrigade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FireBrigade_FireBrigadeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "FireBrigade");

            migrationBuilder.DropTable(
                name: "DutyShift");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FireBrigadeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FireBrigadeId",
                table: "Employees");
        }
    }
}
