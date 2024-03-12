using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PnsApp.Ef.Migrations
{
    /// <inheritdoc />
    public partial class foto_blob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Fotka",
                table: "Zakaznik",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fotka",
                table: "Zakaznik");
        }
    }
}
