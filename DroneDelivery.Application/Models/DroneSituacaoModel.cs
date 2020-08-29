using System;
using System.Collections.Generic;

namespace DroneDelivery.Application.Models
{
    public class DroneSituacaoModel
    {
        public Guid Id { get; set; }

        public string Situacao { get; set; }

        public IEnumerable<DronePedidoModel> Pedidos { get; set; }
    }
}
