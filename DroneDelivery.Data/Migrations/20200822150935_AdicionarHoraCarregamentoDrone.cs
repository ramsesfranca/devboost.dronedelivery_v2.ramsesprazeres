using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DroneDelivery.Data.Migrations
{
    public partial class AdicionarHoraCarregamentoDrone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HoraCarregamento",
                table: "Drones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraCarregamento",
                table: "Drones");
        }
    }
}
