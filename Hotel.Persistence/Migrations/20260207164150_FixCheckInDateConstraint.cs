using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCheckInDateConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Reservation_CheckInDate",
                table: "Reservations");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Reservation_CheckInDate",
                table: "Reservations",
                sql: "CAST([CheckInDate] AS DATE) >= CAST(GETDATE() AS DATE)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Reservation_CheckInDate",
                table: "Reservations");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Reservation_CheckInDate",
                table: "Reservations",
                sql: "[CheckInDate] >= GETDATE()");
        }
    }
}
