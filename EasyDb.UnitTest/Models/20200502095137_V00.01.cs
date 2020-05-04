using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyDb.UnitTest.Models
{
    public partial class V0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaseUOM",
                table: "tblprod_Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblpuom_ProductUOM",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    Multiple = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblpuom_ProductUOM", x => new { x.Name, x.ProductID });
                    table.ForeignKey(
                        name: "FK_tblpuom_ProductUOM_tblprod_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tblprod_Product",
                        principalColumn: "prod_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "tblprod_Product",
                keyColumn: "prod_ID",
                keyValue: 1,
                column: "BaseUOM",
                value: "Piece");

            migrationBuilder.UpdateData(
                table: "tblprod_Product",
                keyColumn: "prod_ID",
                keyValue: 2,
                column: "BaseUOM",
                value: "Piece");

            migrationBuilder.InsertData(
                table: "tblpuom_ProductUOM",
                columns: new[] { "Name", "ProductID", "Multiple" },
                values: new object[,]
                {
                    { "Box", 1, 12m },
                    { "Box", 2, 12m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblpuom_ProductUOM_ProductID",
                table: "tblpuom_ProductUOM",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblpuom_ProductUOM");

            migrationBuilder.DropColumn(
                name: "BaseUOM",
                table: "tblprod_Product");
        }
    }
}
