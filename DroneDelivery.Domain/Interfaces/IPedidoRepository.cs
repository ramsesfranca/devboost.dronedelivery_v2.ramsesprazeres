using DroneDelivery.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ObterAsync();

        Task<Pedido> ObterAsync(Guid id);

        Task AdicionarAsync(Pedido drone);

        Task RemoverAsync(Guid id);
        void Remover(Pedido drone);
    }
}
