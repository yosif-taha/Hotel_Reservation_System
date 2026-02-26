using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoomOfferInhereteFromBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OfferRoom",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OfferRoom",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OfferRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OfferRoom",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OfferRoom");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OfferRoom");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OfferRoom");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OfferRoom");
        }
    }
}
