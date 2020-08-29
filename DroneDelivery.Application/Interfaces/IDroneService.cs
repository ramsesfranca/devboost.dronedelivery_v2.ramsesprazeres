using DroneDelivery.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Interfaces
{
    public interface IDroneService
    {
        Task<IEnumerable<DroneSituacaoModel>> ListarDronesAsync();
        Task<DroneSituacaoModel> ListarDroneAsync(Guid id);

        Task AtualizarPedidosEntregues(Guid droneId);

        Task<IEnumerable<DroneModel>> ObterAsync();

        Task<DroneModel> ObterAsync(Guid id);

        Task AdicionarAsync(CreateDroneModel drone);

        Task RemoverAsync(Guid id);
        Task Remover(DroneModel drone);

    }
}
