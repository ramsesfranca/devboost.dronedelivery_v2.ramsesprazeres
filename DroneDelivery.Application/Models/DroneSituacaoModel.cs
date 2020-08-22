using System;

namespace DroneDelivery.Application.Models
{
    public class DroneSituacaoModel
    {
        public Guid Id { get; set; }

        public string Situacao { get; set; }

        public Guid PedidoId { get; set; }

    }
}
