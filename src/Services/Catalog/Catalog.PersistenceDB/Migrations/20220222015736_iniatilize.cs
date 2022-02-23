using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.PersistenceDB.Migrations
{
    public partial class iniatilize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductID = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description of product 1", "Product 1", 762m },
                    { 73, "Description of product 73", "Product 73", 150m },
                    { 72, "Description of product 72", "Product 72", 783m },
                    { 71, "Description of product 71", "Product 71", 875m },
                    { 70, "Description of product 70", "Product 70", 373m },
                    { 69, "Description of product 69", "Product 69", 181m },
                    { 68, "Description of product 68", "Product 68", 994m },
                    { 67, "Description of product 67", "Product 67", 101m },
                    { 66, "Description of product 66", "Product 66", 938m },
                    { 65, "Description of product 65", "Product 65", 270m },
                    { 64, "Description of product 64", "Product 64", 490m },
                    { 63, "Description of product 63", "Product 63", 809m },
                    { 62, "Description of product 62", "Product 62", 467m },
                    { 61, "Description of product 61", "Product 61", 344m },
                    { 60, "Description of product 60", "Product 60", 874m },
                    { 59, "Description of product 59", "Product 59", 467m },
                    { 58, "Description of product 58", "Product 58", 505m },
                    { 57, "Description of product 57", "Product 57", 200m },
                    { 56, "Description of product 56", "Product 56", 420m },
                    { 55, "Description of product 55", "Product 55", 978m },
                    { 54, "Description of product 54", "Product 54", 914m },
                    { 53, "Description of product 53", "Product 53", 865m },
                    { 74, "Description of product 74", "Product 74", 770m },
                    { 52, "Description of product 52", "Product 52", 742m },
                    { 75, "Description of product 75", "Product 75", 826m },
                    { 77, "Description of product 77", "Product 77", 253m },
                    { 98, "Description of product 98", "Product 98", 797m },
                    { 97, "Description of product 97", "Product 97", 623m },
                    { 96, "Description of product 96", "Product 96", 170m },
                    { 95, "Description of product 95", "Product 95", 167m },
                    { 94, "Description of product 94", "Product 94", 363m },
                    { 93, "Description of product 93", "Product 93", 686m },
                    { 92, "Description of product 92", "Product 92", 801m },
                    { 91, "Description of product 91", "Product 91", 283m },
                    { 90, "Description of product 90", "Product 90", 341m },
                    { 89, "Description of product 89", "Product 89", 357m },
                    { 88, "Description of product 88", "Product 88", 462m },
                    { 87, "Description of product 87", "Product 87", 356m },
                    { 86, "Description of product 86", "Product 86", 282m },
                    { 85, "Description of product 85", "Product 85", 716m },
                    { 84, "Description of product 84", "Product 84", 269m },
                    { 83, "Description of product 83", "Product 83", 383m },
                    { 82, "Description of product 82", "Product 82", 709m },
                    { 81, "Description of product 81", "Product 81", 154m },
                    { 80, "Description of product 80", "Product 80", 488m },
                    { 79, "Description of product 79", "Product 79", 237m },
                    { 78, "Description of product 78", "Product 78", 340m },
                    { 76, "Description of product 76", "Product 76", 911m },
                    { 51, "Description of product 51", "Product 51", 183m },
                    { 50, "Description of product 50", "Product 50", 717m },
                    { 49, "Description of product 49", "Product 49", 109m },
                    { 22, "Description of product 22", "Product 22", 441m },
                    { 21, "Description of product 21", "Product 21", 344m },
                    { 20, "Description of product 20", "Product 20", 364m },
                    { 19, "Description of product 19", "Product 19", 183m },
                    { 18, "Description of product 18", "Product 18", 288m },
                    { 17, "Description of product 17", "Product 17", 836m },
                    { 16, "Description of product 16", "Product 16", 182m },
                    { 15, "Description of product 15", "Product 15", 658m },
                    { 14, "Description of product 14", "Product 14", 744m },
                    { 13, "Description of product 13", "Product 13", 331m },
                    { 12, "Description of product 12", "Product 12", 379m },
                    { 11, "Description of product 11", "Product 11", 160m },
                    { 10, "Description of product 10", "Product 10", 194m },
                    { 9, "Description of product 9", "Product 9", 290m },
                    { 8, "Description of product 8", "Product 8", 696m },
                    { 7, "Description of product 7", "Product 7", 761m },
                    { 6, "Description of product 6", "Product 6", 589m },
                    { 5, "Description of product 5", "Product 5", 226m },
                    { 4, "Description of product 4", "Product 4", 417m },
                    { 3, "Description of product 3", "Product 3", 119m },
                    { 2, "Description of product 2", "Product 2", 299m },
                    { 23, "Description of product 23", "Product 23", 198m },
                    { 24, "Description of product 24", "Product 24", 614m },
                    { 25, "Description of product 25", "Product 25", 950m },
                    { 26, "Description of product 26", "Product 26", 351m },
                    { 48, "Description of product 48", "Product 48", 636m },
                    { 47, "Description of product 47", "Product 47", 297m },
                    { 46, "Description of product 46", "Product 46", 561m },
                    { 45, "Description of product 45", "Product 45", 476m },
                    { 44, "Description of product 44", "Product 44", 857m },
                    { 43, "Description of product 43", "Product 43", 288m },
                    { 42, "Description of product 42", "Product 42", 433m },
                    { 41, "Description of product 41", "Product 41", 538m },
                    { 40, "Description of product 40", "Product 40", 412m },
                    { 39, "Description of product 39", "Product 39", 237m },
                    { 99, "Description of product 99", "Product 99", 687m },
                    { 38, "Description of product 38", "Product 38", 153m },
                    { 36, "Description of product 36", "Product 36", 384m },
                    { 35, "Description of product 35", "Product 35", 662m },
                    { 34, "Description of product 34", "Product 34", 336m },
                    { 33, "Description of product 33", "Product 33", 377m },
                    { 32, "Description of product 32", "Product 32", 222m },
                    { 31, "Description of product 31", "Product 31", 924m },
                    { 30, "Description of product 30", "Product 30", 799m },
                    { 29, "Description of product 29", "Product 29", 887m },
                    { 28, "Description of product 28", "Product 28", 393m },
                    { 27, "Description of product 27", "Product 27", 345m },
                    { 37, "Description of product 37", "Product 37", 306m },
                    { 100, "Description of product 100", "Product 100", 538m }
                });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "Id", "ProductID", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 78 },
                    { 73, 73, 37 },
                    { 72, 72, 63 },
                    { 71, 71, 9 },
                    { 70, 70, 41 },
                    { 69, 69, 14 },
                    { 68, 68, 76 },
                    { 67, 67, 79 },
                    { 66, 66, 19 },
                    { 65, 65, 99 },
                    { 64, 64, 94 },
                    { 63, 63, 72 },
                    { 62, 62, 28 },
                    { 61, 61, 39 },
                    { 60, 60, 3 },
                    { 59, 59, 64 },
                    { 58, 58, 53 },
                    { 57, 57, 13 },
                    { 56, 56, 26 },
                    { 55, 55, 71 },
                    { 54, 54, 6 },
                    { 53, 53, 59 },
                    { 74, 74, 2 },
                    { 52, 52, 28 },
                    { 75, 75, 13 },
                    { 77, 77, 76 },
                    { 98, 98, 81 },
                    { 97, 97, 66 },
                    { 96, 96, 31 },
                    { 95, 95, 10 },
                    { 94, 94, 65 },
                    { 93, 93, 34 },
                    { 92, 92, 38 },
                    { 91, 91, 3 },
                    { 90, 90, 22 },
                    { 89, 89, 29 },
                    { 88, 88, 8 },
                    { 87, 87, 11 },
                    { 86, 86, 71 },
                    { 85, 85, 93 },
                    { 84, 84, 1 },
                    { 83, 83, 60 },
                    { 82, 82, 58 },
                    { 81, 81, 65 },
                    { 80, 80, 66 },
                    { 79, 79, 7 },
                    { 78, 78, 28 },
                    { 76, 76, 24 },
                    { 51, 51, 88 },
                    { 50, 50, 26 },
                    { 49, 49, 1 },
                    { 22, 22, 51 },
                    { 21, 21, 63 },
                    { 20, 20, 72 },
                    { 19, 19, 52 },
                    { 18, 18, 7 },
                    { 17, 17, 62 },
                    { 16, 16, 0 },
                    { 15, 15, 58 },
                    { 14, 14, 63 },
                    { 13, 13, 77 },
                    { 12, 12, 94 },
                    { 11, 11, 90 },
                    { 10, 10, 99 },
                    { 9, 9, 76 },
                    { 8, 8, 0 },
                    { 7, 7, 89 },
                    { 6, 6, 40 },
                    { 5, 5, 74 },
                    { 4, 4, 95 },
                    { 3, 3, 52 },
                    { 2, 2, 30 },
                    { 23, 23, 16 },
                    { 24, 24, 98 },
                    { 25, 25, 31 },
                    { 26, 26, 70 },
                    { 48, 48, 42 },
                    { 47, 47, 5 },
                    { 46, 46, 64 },
                    { 45, 45, 90 },
                    { 44, 44, 87 },
                    { 43, 43, 75 },
                    { 42, 42, 38 },
                    { 41, 41, 59 },
                    { 40, 40, 49 },
                    { 39, 39, 69 },
                    { 99, 99, 88 },
                    { 38, 38, 98 },
                    { 36, 36, 16 },
                    { 35, 35, 49 },
                    { 34, 34, 1 },
                    { 33, 33, 15 },
                    { 32, 32, 70 },
                    { 31, 31, 0 },
                    { 30, 30, 81 },
                    { 29, 29, 28 },
                    { 28, 28, 61 },
                    { 27, 27, 0 },
                    { 37, 37, 91 },
                    { 100, 100, 71 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Id",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductID",
                table: "Stock",
                column: "ProductID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
