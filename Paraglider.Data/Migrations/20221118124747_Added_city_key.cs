using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paraglider.Data.Migrations
{
    public partial class Added_city_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WeddingPlannings_CityId",
                table: "WeddingPlannings",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeddingPlannings_Cities_CityId",
                table: "WeddingPlannings",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeddingPlannings_Cities_CityId",
                table: "WeddingPlannings");

            migrationBuilder.DropIndex(
                name: "IX_WeddingPlannings_CityId",
                table: "WeddingPlannings");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Cities");
        }
    }
}
