using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeTaxApi.Migrations
{
    /// <inheritdoc />
    public partial class Add_tax_band : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Sequence-TaxBand",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "TaxBands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    LowerLimit = table.Column<int>(type: "integer", nullable: false),
                    UpperLimit = table.Column<int>(type: "integer", nullable: true),
                    Rate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBands", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxBands");

            migrationBuilder.DropSequence(
                name: "Sequence-TaxBand");
        }
    }
}
