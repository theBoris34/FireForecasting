using Microsoft.EntityFrameworkCore.Migrations;

namespace FireForecasting.DAL.Migrations
{
    public partial class FireTrackUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FireTruckId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FireTruckId",
                table: "Employees",
                column: "FireTruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FireTruckBase_FireTruckId",
                table: "Employees",
                column: "FireTruckId",
                principalTable: "FireTruckBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FireTruckBase_FireTruckId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FireTruckId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FireTruckId",
                table: "Employees");
        }
    }
}
