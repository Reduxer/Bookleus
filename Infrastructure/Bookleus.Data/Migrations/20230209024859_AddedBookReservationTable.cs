using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookleus.Data.Migrations
{
    public partial class AddedBookReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookReservations",
                columns: table => new
                {
                    CustomerBookReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookSKU = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReservations", x => x.CustomerBookReservationId);
                    table.ForeignKey(
                        name: "FK_BookReservations_Books_BookSKU",
                        column: x => x.BookSKU,
                        principalTable: "Books",
                        principalColumn: "SKU",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_BookSKU",
                table: "BookReservations",
                column: "BookSKU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookReservations");
        }
    }
}
