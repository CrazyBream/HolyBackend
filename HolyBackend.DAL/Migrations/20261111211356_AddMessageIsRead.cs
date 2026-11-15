using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageIsRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "OrderMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "OrderMessages");
        }
    }
}
