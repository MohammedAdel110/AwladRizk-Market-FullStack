using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AwladRizk.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Admin"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsletterSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubscribedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterSubscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    BadgeText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Pending"),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryStreet = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DeliveryArea = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeliveryCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeliveryGovernorate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OldPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false),
                    StockQty = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    TransactionRef = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductNameSnapshot = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@awladrizk.com", "Admin", "$2a$12$LJ3m4ys5LlGGMr6VJR8X7OE4J.K3FZkBGDMHgJHGQ3T5jR5Yn2mVe", "Admin", null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Icon", "NameAr", "NameEn", "Slug", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🍿", "شيبسي ومقرمشات", "Chips & Snacks", "chips-snacks", 1, null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🧃", "عصائر ومشروبات", "Juices & Drinks", "juices-drinks", 2, null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "☕", "قهوة وشاي", "Coffee & Tea", "coffee-tea", 3, null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🧀", "ألبان وأجبان", "Dairy & Cheese", "dairy-cheese", 4, null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🍪", "بسكويت وحلويات", "Biscuits & Sweets", "biscuits-sweets", 5, null },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🥫", "معلبات", "Canned Food", "canned-food", 6, null },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🧹", "منظفات", "Cleaning", "cleaning", 7, null },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🍝", "معكرونة وأرز", "Pasta & Rice", "pasta-rice", 8, null }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "BadgeText", "CreatedAt", "DescriptionAr", "DescriptionEn", "DiscountPercent", "Icon", "IsActive", "TitleAr", "TitleEn", "UpdatedAt", "ValidUntil" },
                values: new object[,]
                {
                    { 1, "30%", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "خصم 30% على كل منتجات شيبسي", "30% off all Chipsy products", 30, "🔥", true, "🔥 خصم 30% على شيبسي الأحجام الكبيرة", "🔥 30% Off Chipsy Large Bags", null, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) },
                    { 2, "B3G1", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "اشترِ 3 جهينة واحصل على 1 مجاناً", "Buy 3 Juhayna get 1 free", 25, "🧃", true, "🧃 عصير جهينة: اشترِ 3 واحصل على 1 مجاناً", "🧃 Juhayna Juice: Buy 3 Get 1 Free", null, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) },
                    { 3, "٨٥ ج", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "نسكافيه ٣ في ١ بـ ٨٥ جنيه بدلاً من ١٢٠", "Nescafé 3in1 EGP 85 instead of 120", 29, "☕", true, "☕ عرض نسكافيه: الكيس الكبير بـ ٨٥ جنيه بدلاً من ١٢٠", "☕ Nescafé Offer: Large Pack EGP 85 Instead of 120", null, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) },
                    { 4, "20%", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "خصم 20% على كل المنظفات", "20% off all cleaning products", 20, "🧹", true, "🧹 خصم 20% على كل المنظفات", "🧹 20% Off All Cleaning Products", null, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) },
                    { 5, "٩٥ ج", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "شاي العروسة ٢٠٠ كيس بـ ٩٥ جنيه فقط", "El Arosa Tea 200 bags for EGP 95 only", 24, "☕", true, "☕ شاي العروسة ٢٠٠ كيس بـ ٩٥ جنيه فقط", "☕ El Arosa Tea 200 Bags for EGP 95 Only", null, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) },
                    { 6, "25%", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "فول كاليفورنيا بخصم ٢٥%", "California Garden Foul 25% off", 25, "🥫", true, "🥫 فول كاليفورنيا بخصم ٢٥%", "🥫 California Garden Foul 25% Off", null, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "ImageUrl", "IsOnSale", "NameAr", "NameEn", "OldPrice", "Price", "StockQty", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/chipsy-cheese.png", true, "شيبسي كبيرة - بالجبنة", "Chipsy Large - Cheese Flavor", 35m, 25m, 100, null },
                    { 2, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/juhayna-mango.png", false, "عصير جهينة كلاسيك - مانجو", "Juhayna Classic Juice - Mango", null, 18m, 200, null },
                    { 3, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/nescafe-3in1.png", true, "نسكافيه ٣ في ١ - ٣٠ كيس", "Nescafé 3in1 - 30 Sachets", 120m, 85m, 80, null },
                    { 4, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/domty-cheese.png", false, "جبنة دومتي أبيض مثلثات", "Domty White Cheese Triangles", null, 42m, 150, null },
                    { 5, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/arosa-tea.png", true, "شاي العروسة - ٢٠٠ كيس", "El Arosa Tea - 200 Bags", 125m, 95m, 60, null },
                    { 6, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/molto-croissant.png", false, "مولتو كرواسون - ١٢ قطعة", "Molto Croissant - 12 Pack", null, 60m, 120, null },
                    { 7, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/chipsy-ketchup.png", false, "شيبسي بالكاتشب", "Chipsy - Ketchup Flavor", null, 15m, 180, null },
                    { 8, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/beyti-guava.png", false, "عصير بيتي - جوافة", "Beyti Juice - Guava", null, 12m, 200, null },
                    { 9, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/california-foul.png", true, "فول كاليفورنيا جاردن", "California Garden Foul", 38m, 28m, 90, null },
                    { 10, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/gena-tuna.png", false, "تونة جنة", "Gena Tuna", null, 35m, 110, null },
                    { 11, 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/regina-pasta.png", false, "مكرونة ريجينا - ٥٠٠ جرام", "Regina Pasta - 500g", null, 22m, 140, null },
                    { 12, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/images/persil-detergent.png", true, "برسيل غسيل - ٤ كيلو", "Persil Detergent - 4kg", 190m, 155m, 50, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Email",
                table: "AdminUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Slug",
                table: "Categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterSubscribers_Email",
                table: "NewsletterSubscribers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_IsActive",
                table: "Offers",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SessionId",
                table: "Orders",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status",
                table: "Orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsOnSale",
                table: "Products",
                column: "IsOnSale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "NewsletterSubscribers");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
