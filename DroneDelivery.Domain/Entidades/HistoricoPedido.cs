using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneDelivery.Domain.Entidades
{
    public class HistoricoPedido
    {

        public Guid Id { get; private set; }

        [ForeignKey("Drone")]
        public Guid DroneId { get; private set; }
        public Drone Drone { get; private set; }

        [ForeignKey("Pedido")]
        public Guid PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }

        public DateTime DataSaida { get; private set; }
        public DateTime? DataEntrega { get; private set; }

        public HistoricoPedido(Guid droneId, Guid pedidoId)
        {
            DroneId = droneId;
            PedidoId = pedidoId;
            DataSaida = DateTime.Now;
        }

        public void MarcarEntregaCompleta()
        {
            DataEntrega = DateTime.Now;
        }
    }
}
