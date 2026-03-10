using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Booking_System_with_Identity.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingAndPackageSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NumberOfSeats",
                table: "Bookings",
                newName: "NumberOfTravellers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfTravellers",
                table: "Bookings",
                newName: "NumberOfSeats");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
