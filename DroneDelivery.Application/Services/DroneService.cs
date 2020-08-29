using AutoMapper;
using DroneDelivery.Application.Interfaces;
using DroneDelivery.Application.Models;
using DroneDelivery.Data.Repositorios.IRepository;
using DroneDelivery.Domain.Entidades;
using DroneDelivery.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Services
{
    public class DroneService : IDroneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DroneService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AdicionarAsync(CreateDroneModel createDroneModel)
        {
            var drone = _mapper.Map<CreateDroneModel, Drone>(createDroneModel);
            await _unitOfWork.Drones.AdicionarAsync(drone);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<DroneSituacaoModel>> ListarDronesAsync()
        {
            var drones = await ObterDronesParaViagem();

            foreach (var drone in drones)
                drone.SepararPedidosParaEntrega();

            return _mapper.Map<IEnumerable<Drone>, IEnumerable<DroneSituacaoModel>>(drones);
        }

        public async Task<DroneSituacaoModel> ListarDroneAsync(Guid id)
        {
            var drone = await _unitOfWork.Drones.ObterAsync(id);

            return _mapper.Map<Drone, DroneSituacaoModel>(drone);
        }

        public async Task<IEnumerable<DroneModel>> ObterAsync()
        {
            var drones = await _unitOfWork.Drones.ObterAsync();

            return _mapper.Map<IEnumerable<Drone>, IEnumerable<DroneModel>>(drones);
        }

        public async Task<DroneModel> ObterAsync(Guid id)
        {
            var drone = await _unitOfWork.Drones.ObterAsync(id);

            return _mapper.Map<Drone, DroneModel>(drone);
        }

        public async Task Remover(DroneModel droneModel)
        {
            var drone = _mapper.Map<DroneModel, Drone>(droneModel);

            _unitOfWork.Drones.Remover(drone);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            await _unitOfWork.Drones.RemoverAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task AtualizarPedidosEntregues(Guid droneId)
        {
            var drone = await _unitOfWork.Drones.ObterAsync(droneId);

            if (drone != null)
            {
                drone.AtualizarStatus(DroneStatus.Livre);

                var historicoPedidos = await _unitOfWork.Pedidos.ObterPedidosDoDroneAsync(droneId);
                foreach (var hist in historicoPedidos.Where(x => x.DataEntrega == null))
                {
                    hist.Pedido.AtualizarStatusPedido(PedidoStatus.Entregue);
                    hist.MarcarEntregaCompleta();
                }

                await _unitOfWork.SaveAsync();
            }
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
