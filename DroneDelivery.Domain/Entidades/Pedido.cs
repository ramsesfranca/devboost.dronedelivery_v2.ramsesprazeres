using DroneDelivery.Domain.Core;
using DroneDelivery.Domain.Enum;
using System;

namespace DroneDelivery.Domain.Entidades
{
    public class Pedido : EntidadeBase<Guid>
    {
        public double Peso { get; private set; }

        public DateTime DataPedido { get; private set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public PedidoStatus Status { get; private set; }

        public Guid? DroneId { get; private set; }
        public Drone Drone { get; private set; }

        protected Pedido() { }

        public Pedido(double peso, double latitude, double longitude, PedidoStatus status)
        {
            Peso = peso;
            Longitude = longitude;
            Latitude = latitude;
            Status = status;
        }

        public void AtualizarStatusPedido(PedidoStatus status)
        {
            Status = status;
        }


        public bool ValidarPesoPedido(double peso)
        {
            return peso >= Peso;
        }

        public void AssociarDrone(Guid droneId)
        {
            DroneId = droneId;
        }





    }
}
