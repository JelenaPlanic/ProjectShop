using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopService.Migrations
{
    public partial class AddedSellersAndShops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sellers_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 1, "Bulevar vojvode Stepe 2", "Lidl" });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 2, "Futoski put 93c", "Univerexport" });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 3, "Bulevar vojvode Stepe 32", "Idea" });

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "Id", "Name", "ShopId", "Surname", "Year" },
                values: new object[,]
                {
                    { 1, "Marko", 2, "Markovic", 1987 },
                    { 2, "Milica", 1, "Milic", 1990 },
                    { 3, "Petar", 3, "Petrovic", 1974 },
                    { 4, "Luka", 1, "Lukic", 1997 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_ShopId",
                table: "Sellers",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "Shops");
        }
    }
}
