using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zefffood.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductoU2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "t_producto",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "t_producto");
        }
    }
}
