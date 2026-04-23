using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConnetDB.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Image", "Username" },
                values: new object[,]
                {
                    { 1, "apple.jpg", "Apple" },
                    { 2, "samsung.jpg", "Samsung" },
                    { 3, "xiaomi.jpg", "Xiaomi" },
                    { 4, "oppo.jpg", "Oppo" },
                    { 5, "dell.jpg", "Dell" },
                    { 6, "hp.jpg", "HP" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "phone.jpg", "Điện thoại" },
                    { 2, "laptop.jpg", "Laptop" },
                    { 3, "tablet.jpg", "Tablet" },
                    { 4, "accessory.jpg", "Phụ kiện" },
                    { 5, "watch.jpg", "Smartwatch" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BuyTurn", "CategoryId", "Description", "Image", "Name", "OldPrice", "Price", "Quantity", "Specification" },
                values: new object[,]
                {
                    { 1, 1, "500", 1, "Điện thoại Apple mới", "iphone15.jpg", "iPhone 15", 2200, 2000, 20, "A17 chip" },
                    { 2, 2, "300", 1, "Samsung flagship", "s23.jpg", "Galaxy S23", 1700, 1500, 15, "Snapdragon 8" },
                    { 3, 3, "200", 1, "Giá rẻ cấu hình mạnh", "mi13.jpg", "Xiaomi 13", 1100, 900, 25, "Snapdragon 8 Gen 2" },
                    { 4, 1, "150", 2, "Laptop Apple", "macbook.jpg", "Macbook M2", 2700, 2500, 10, "M2 chip" },
                    { 5, 5, "120", 2, "Laptop cao cấp", "xps.jpg", "Dell XPS", 2000, 1800, 8, "Intel i7" },
                    { 6, 6, "100", 2, "Laptop phổ thông", "hp.jpg", "HP Pavilion", 1400, 1200, 12, "Intel i5" },
                    { 7, 1, "80", 3, "Tablet Apple", "ipad.jpg", "iPad Pro", 1500, 1300, 10, "M2 chip" },
                    { 8, 1, "600", 4, "Tai nghe Apple", "airpods.jpg", "AirPods", 350, 300, 50, "Bluetooth" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
