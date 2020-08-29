using AutoMapper;
using DroneDelivery.Application.Interfaces;
using DroneDelivery.Application.Models;
using DroneDelivery.Data.Repositorios.IRepository;
using DroneDelivery.Domain.Entidades;
using DroneDelivery.Domain.Enum;
using DroneDelivery.Domain.Interfaces;
using DroneDelivery.Infra.BaseDrone;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ITempoEntregaService _calcularDistancia;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOptions<BaseDroneConfig> _config;

        public PedidoService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<BaseDroneConfig> config, ITempoEntregaService calcularDistancia)
        {
            _calcularDistancia = calcularDistancia;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public IMapper Mapper { get; }

        public async Task<bool> AdicionarAsync(CreatePedidoModel createPedidoModel)
        {
            var pedido = _mapper.Map<CreatePedidoModel, Pedido>(createPedidoModel);

            if (!pedido.ValidarPesoPedido(Utility.Utils.CARGA_MAXIMA_GRAMAS))
                return false;

            // temos que procurar drones disponiveis
            Drone droneDisponivel = null;

            //temos que olhar TODOS os drones, e nao somente os disponiveis, pq se todos estiverem ocupados... 
            //ainda sim, precisamos validar se temos capacidade de entregar o pedido
            var drones = await _unitOfWork.Drones.ObterAsync();

            if (drones.Count() == 0)
                return false;

            foreach (var drone in drones)
            {

                //valida se algum drone tem autonomia para entregar o pedido
                var droneTemAutonomia = drone.ValidarAutonomia(_calcularDistancia, _config.Value.Latitude, _config.Value.Longitude, pedido.Latitude, pedido.Longitude);
                if (!droneTemAutonomia)
                    return false;

                //verifica se algum drone aceita o peso
                var droneAceitaPeso = drone.VerificarDroneAceitaOPesoPedido(pedido.Peso);
                if (!droneAceitaPeso)
                    return false;

                //verificar se tem algum drone disponivel
                if (drone.Status != DroneStatus.Livre)
                    continue;

                //verifica se o drone possui espaço para adicionar mais peso
                if (!drone.ValidarCapacidadeSobra(pedido.Peso))
                    continue;

                //verifica se o drone possui autonomia para enttregar o pedido
                if (!drone.ValidarAutonomiaSobraPorPontoEntrega(_calcularDistancia, _config.Value.Latitude, _config.Value.Longitude, pedido.Latitude, pedido.Longitude))
                    continue;

                droneDisponivel = drone;
                break;
            }

            if (droneDisponivel == null)
            {
                pedido.AtualizarStatusPedido(PedidoStatus.AguardandoEntrega);
            }
            else
            {
                pedido.AssociarDrone(droneDisponivel.Id);
                pedido.AtualizarStatusPedido(PedidoStatus.EmEntrega);

                //v1 em grupo --- ja atualizava o status do drone
                //droneDisponivel.AtualizarStatus(DroneStatus.EmEntrega);
            }

            await _unitOfWork.Pedidos.AdicionarAsync(pedido);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<PedidoModel>> ObterAsync()
        {
            var pedidos = await _unitOfWork.Pedidos.ObterAsync();

            return _mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoModel>>(pedidos);
        }

        public async Task<PedidoModel> ObterAsync(Guid id)
        {
            var pedido = await _unitOfWork.Pedidos.ObterAsync(id);

            return _mapper.Map<Pedido, PedidoModel>(pedido);
        }

        public async Task Remover(PedidoModel pedidoModel)
        {
            var pedido = _mapper.Map<PedidoModel, Pedido>(pedidoModel);

            _unitOfWork.Pedidos.Remover(pedido);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            await _unitOfWork.Pedidos.RemoverAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
