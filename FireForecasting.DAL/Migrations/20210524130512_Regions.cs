using Microsoft.EntityFrameworkCore.Migrations;

namespace FireForecasting.DAL.Migrations
{
    public partial class Regions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "Fires",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fires_RegionId",
                table: "Fires",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fires_Region_RegionId",
                table: "Fires",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fires_Region_RegionId",
                table: "Fires");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropIndex(
                name: "IX_Fires_RegionId",
                table: "Fires");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Fires");
        }
    }
}
