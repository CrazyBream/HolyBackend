using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddHolySponsorFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHolySponsor",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHolySponsor",
                table: "Users");
        }
    }
}
