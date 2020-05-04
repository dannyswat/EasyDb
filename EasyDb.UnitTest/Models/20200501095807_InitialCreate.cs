using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyDb.UnitTest.Models
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblprod_Product",
                columns: table => new
                {
                    prod_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 30, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Price_Currency = table.Column<string>(maxLength: 3, nullable: true),
                    Price_Amount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblprod_Product", x => x.prod_ID);
                });

            migrationBuilder.InsertData(
                table: "tblprod_Product",
                columns: new[] { "prod_ID", "Code", "Name", "Price_Currency", "Price_Amount" },
                values: new object[] { 1, "001", "Product No.1", "HKD", 99 });

            migrationBuilder.InsertData(
                table: "tblprod_Product",
                columns: new[] { "prod_ID", "Code", "Name", "Price_Currency", "Price_Amount" },
                values: new object[] { 2, "002", "Product No.2", "HKD", 188 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblprod_Product");
        }
    }
}
