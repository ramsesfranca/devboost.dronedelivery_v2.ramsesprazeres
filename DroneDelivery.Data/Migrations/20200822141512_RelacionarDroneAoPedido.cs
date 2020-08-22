using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DroneDelivery.Data.Migrations
{
    public partial class RelacionarDroneAoPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DroneId",
                table: "Pedidos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_DroneId",
                table: "Pedidos",
                column: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Drones_DroneId",
                table: "Pedidos",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Drones_DroneId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_DroneId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "DroneId",
                table: "Pedidos");
        }
    }
}
