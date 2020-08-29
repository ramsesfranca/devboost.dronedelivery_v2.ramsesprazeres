using AutoMapper;
using DroneDelivery.Application.Models;
using DroneDelivery.Application.Queries;
using DroneDelivery.Application.Queries.Drones;
using DroneDelivery.Application.Response;
using DroneDelivery.Application.Validador;
using DroneDelivery.Data.Repositorios.IRepository;
using DroneDelivery.Domain.Entidades;
using DroneDelivery.Domain.Enum;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Handlers.Drones
{
    public class ListarSituacaoDronesHandler : ValidatorResponse, IRequestHandler<ListarSituacaoDronesQuery, ResponseVal>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListarSituacaoDronesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseVal> Handle(ListarSituacaoDronesQuery request, CancellationToken cancellationToken)
        {
            var drones = await ObterDronesParaViagem();

            foreach (var drone in drones)
                drone.SepararPedidosParaEntrega();

            _response.AddValue(new
            {
                drones = _mapper.Map<IEnumerable<Drone>, IEnumerable<DroneSituacaoModel>>(drones)
            });

            return _response;
        }

        private async Task<IEnumerable<Drone>> ObterDronesParaViagem()
        {
            // obter drones que tem pedido e estao aguardando
            var dronesProntos = await _unitOfWork.Drones.ObterDronesParaEntregaAsync();

            foreach (var drone in dronesProntos)
            {
                drone.AtualizarStatus(DroneStatus.EmEntrega);
                await _unitOfWork.Pedidos.CriarHistoricoPedidoAsync(drone.Pedidos.Where(x => x.Status == PedidoStatus.EmEntrega));
            }

            await _unitOfWork.SaveAsync();

            return dronesProntos;
        }
    }
}
