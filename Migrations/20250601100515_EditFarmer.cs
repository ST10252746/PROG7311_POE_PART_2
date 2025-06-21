using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10251759_PROG7313_POE_PART_2.Migrations
{
    /// <inheritdoc />
    public partial class EditFarmer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LifespanYears",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LifespanYears",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
