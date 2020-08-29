using AutoMapper;
using DroneDelivery.Application.Commands.Pedidos;
using DroneDelivery.Application.Interfaces;
using DroneDelivery.Application.Response;
using DroneDelivery.Application.Validador;
using DroneDelivery.Data.Repositorios.IRepository;
using DroneDelivery.Domain.Entidades;
using DroneDelivery.Domain.Enum;
using DroneDelivery.Domain.Interfaces;
using DroneDelivery.Infra.BaseDrone;
using DroneDelivery.Utility;
using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Handlers.Pedidos
{
    public class CriarPedidoHandler : ValidatorResponse, IRequestHandler<CriarPedidoCommand, ResponseVal>
    {
        private readonly ITempoEntregaService _calcularDistancia;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOptions<BaseDroneConfig> _config;
        private readonly IClienteService _clienteService;
        private readonly IUserRepository _userRepository;

        public CriarPedidoHandler(IUnitOfWork unitOfWork, IMapper mapper, IOptions<BaseDroneConfig> config, ITempoEntregaService calcularDistancia, IClienteService clienteService, IUserRepository userRepository)
        {
            this._calcularDistancia = calcularDistancia;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._config = config;
            this._clienteService = clienteService;
            this._userRepository = userRepository;
        }


        public async Task<ResponseVal> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (request.Notifications.Any())
            {
                this._response.AddNotifications(request.Notifications);

                return this._response;
            }

            var pedido = _mapper.Map<CriarPedidoCommand, Pedido>(request);

            if (!pedido.ValidarPesoPedido(Utils.CARGA_MAXIMA_GRAMAS))
            {
                _response.AddNotification(new Notification("pedido", $"capacidade do pedido não pode ser maior que {Utils.CARGA_MAXIMA_GRAMAS / 1000} KGs"));
            }

            // temos que procurar drones disponiveis
            Drone droneDisponivel = null;

            //temos que olhar TODOS os drones, e nao somente os disponiveis, pq se todos estiverem ocupados... 
            //ainda sim, precisamos validar se temos capacidade de entregar o pedido
            var drones = await this._unitOfWork.Drones.ObterAsync();

            if (!drones.ToList().Any())
            {
                _response.AddNotification(new Notification("pedido", "não existe drone cadastrado"));
            }
            if (_response.HasFails)
            {
                return _response;
            }

            var cliente = await this._userRepository.ObterAsync(this._clienteService.GetCurrentId());

            if (cliente == null)
            {
                _response.AddNotification(new Notification("", "Cliente nõo encontrado"));
                return _response;
            }

            foreach (var drone in drones)
            {
                //valida se algum drone tem autonomia e aceita capacidade para entregar o pedido
                var droneTemAutonomia = drone.ValidarAutonomia(this._calcularDistancia, _config.Value.Latitude, _config.Value.Longitude, cliente.Latitude, cliente.Longitude);
                var droneAceitaPeso = drone.VerificarDroneAceitaOPesoPedido(pedido.Peso);

                if (!droneTemAutonomia || !droneAceitaPeso)
                {
                    continue;
                }
                // verificar se tem algum drone disponivel
                if (drone.Status != DroneStatus.Livre)
                {
                    continue;
                }
                // verifica se o drone possui espaço para adicionar mais peso
                if (!drone.ValidarCapacidadeSobra(pedido.Peso))
                {
                    continue;
                }
                // verifica se o drone possui autonomia para enttregar o pedido
                if (!drone.ValidarAutonomiaSobraPorPontoEntrega(this._calcularDistancia, _config.Value.Latitude, _config.Value.Longitude, cliente.Latitude, cliente.Longitude))
                {
                    continue;
                }

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
                pedido.AssociarCliente(cliente.Id);
                pedido.AtualizarStatusPedido(PedidoStatus.EmEntrega);
            }

            await this._unitOfWork.Pedidos.AdicionarAsync(pedido);
            await this._unitOfWork.SaveAsync();

            _response.AddValue(new
            {
                status = "ok"
            });

            return _response;
        }
    }
}
