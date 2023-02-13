using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookleus.Data.Migrations
{
    public partial class AddedIsActiveColumnInBookReservationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CustomerBookReservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CustomerBookReservations");
        }
    }
}
