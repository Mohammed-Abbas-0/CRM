using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_Infrastraction.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteCampaign1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Campaigns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Campaigns",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Campaigns");
        }
    }
}
