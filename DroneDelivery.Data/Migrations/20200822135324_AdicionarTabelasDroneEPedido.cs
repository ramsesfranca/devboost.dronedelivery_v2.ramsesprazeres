using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DroneDelivery.Data.Migrations
{
    public partial class AdicionarTabelasDroneEPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Capacidade = table.Column<double>(nullable: false),
                    Velocidade = table.Column<double>(nullable: false),
                    Autonomia = table.Column<double>(nullable: false),
                    Carga = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Peso = table.Column<double>(nullable: false),
                    DataPedido = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
