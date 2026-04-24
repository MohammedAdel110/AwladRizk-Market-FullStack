using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AwladRizk.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeTickerMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeTickerMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TextEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeTickerMessages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "HomeTickerMessages",
                columns: new[] { "Id", "CreatedAt", "IsActive", "SortOrder", "TextAr", "TextEn", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 1, "🔥 خصم 30% على شيبسي الأحجام الكبيرة", "🔥 30% Off Chipsy Large Bags", null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "🧃 عصير جهينة: اشترِ 3 واحصل على 1 مجاناً", "🧃 Juhayna Juice: Buy 3 Get 1 Free", null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "🚚 توصيل مجاني للطلبات فوق 200 جنيه", "🚚 Free delivery on orders over EGP 200", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeTickerMessages_IsActive",
                table: "HomeTickerMessages",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_HomeTickerMessages_SortOrder",
                table: "HomeTickerMessages",
                column: "SortOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeTickerMessages");
        }
    }
}
