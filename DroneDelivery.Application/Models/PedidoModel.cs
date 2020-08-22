using DroneDelivery.Domain.Enum;
using System;

namespace DroneDelivery.Application.Models
{
    public class PedidoModel
    {
        public Guid Id { get; set; }

        public double Peso { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }


        public DroneModel Drone { get; set; }
    }
}
