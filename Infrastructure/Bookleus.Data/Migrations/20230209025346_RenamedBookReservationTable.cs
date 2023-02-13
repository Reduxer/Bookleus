using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookleus.Data.Migrations
{
    public partial class RenamedBookReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservations_Books_BookSKU",
                table: "BookReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookReservations",
                table: "BookReservations");

            migrationBuilder.RenameTable(
                name: "BookReservations",
                newName: "CustomerBookReservations");

            migrationBuilder.RenameIndex(
                name: "IX_BookReservations_BookSKU",
                table: "CustomerBookReservations",
                newName: "IX_CustomerBookReservations_BookSKU");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerBookReservations",
                table: "CustomerBookReservations",
                column: "CustomerBookReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBookReservations_Books_BookSKU",
                table: "CustomerBookReservations",
                column: "BookSKU",
                principalTable: "Books",
                principalColumn: "SKU",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBookReservations_Books_BookSKU",
                table: "CustomerBookReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerBookReservations",
                table: "CustomerBookReservations");

            migrationBuilder.RenameTable(
                name: "CustomerBookReservations",
                newName: "BookReservations");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBookReservations_BookSKU",
                table: "BookReservations",
                newName: "IX_BookReservations_BookSKU");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookReservations",
                table: "BookReservations",
                column: "CustomerBookReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservations_Books_BookSKU",
                table: "BookReservations",
                column: "BookSKU",
                principalTable: "Books",
                principalColumn: "SKU",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
