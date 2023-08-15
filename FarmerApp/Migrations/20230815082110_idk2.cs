using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmerApp.Migrations
{
    /// <inheritdoc />
    public partial class idk2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFromInvestor",
                table: "Expenses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFromInvestor",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
