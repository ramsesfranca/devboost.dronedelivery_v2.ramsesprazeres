using DroneDelivery.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoModel>> ObterAsync();

        Task<PedidoModel> ObterAsync(Guid id);

        Task<bool> AdicionarAsync(PedidoModel pedidoModel);

        Task RemoverAsync(Guid id);
        Task Remover(PedidoModel pedidoModel);
    }
}
