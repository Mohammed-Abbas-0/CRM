using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_Infrastraction.Migrations
{
    /// <inheritdoc />
    public partial class applyDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BudgetAfterDiscount",
                table: "Campaigns",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RateDiscount",
                table: "Campaigns",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetAfterDiscount",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "RateDiscount",
                table: "Campaigns");
        }
    }
}
