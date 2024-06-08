using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zefffood.Data
{
    /// <inheritdoc />
    public partial class ContactoNuevo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "t_contacto",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "t_contacto");
        }
    }
}
