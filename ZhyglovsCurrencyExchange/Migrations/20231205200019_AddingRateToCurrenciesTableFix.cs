using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZhyglovsCurrencyExchange.Migrations
{
    public partial class AddingRateToCurrenciesTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                schema: "exchange",
                table: "Currencies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                schema: "exchange",
                table: "Currencies");
        }
    }
}
