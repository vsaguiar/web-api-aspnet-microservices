using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageUrl, CategoryId) " +
                "VALUES('Caderno',7.55,'Caderno',10,'caderno1.jpg',1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageUrl, CategoryId) " +
                "VALUES('Lápis',3.45,'Lápis Preto',20,'lapis1.jpg',1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageUrl, CategoryId) " +
                "VALUES('Clips',5.33,'Clips para papel',50,'clips1.jpg',2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
