using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IncomeTaxApi.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Tax_Bands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaxBands",
                columns: new[] { "Id", "LowerLimit", "Rate", "UpperLimit" },
                values: new object[,]
                {
                    { 1, 0, 0, 5000 },
                    { 2, 5000, 20, 20000 },
                    { 3, 20000, 40, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaxBands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaxBands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaxBands",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
