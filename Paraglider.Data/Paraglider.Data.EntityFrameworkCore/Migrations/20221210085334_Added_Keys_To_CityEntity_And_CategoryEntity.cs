using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paraglider.Data.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedKeysToCityEntityAndCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ComponentId",
                table: "UserComponents",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "ComponentId",
                table: "PlanningComponents",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "ExternalCategoryKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalCategoryKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalCategoryKey_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExternalCityKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalCityKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalCityKey_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalCategoryKey_CategoryId",
                table: "ExternalCategoryKey",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalCityKey_CityId",
                table: "ExternalCityKey",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalCategoryKey");

            migrationBuilder.DropTable(
                name: "ExternalCityKey");

            migrationBuilder.AlterColumn<Guid>(
                name: "ComponentId",
                table: "UserComponents",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "ComponentId",
                table: "PlanningComponents",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
