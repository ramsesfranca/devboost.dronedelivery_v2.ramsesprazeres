using AutoMapper;
using DroneDelivery.Application.Commands.Drones;
using DroneDelivery.Application.Response;
using DroneDelivery.Application.Validador;
using DroneDelivery.Data.Repositorios.IRepository;
using DroneDelivery.Domain.Entidades;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Handlers.Drones
{
    public class CriarDroneHandler : ValidatorResponse, IRequestHandler<CriarDroneCommand, ResponseVal>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CriarDroneHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseVal> Handle(CriarDroneCommand request, CancellationToken cancellationToken)
        {
            if (request.Capacidade > Utility.Utils.CARGA_MAXIMA_GRAMAS)
            {
                _response.AddNotification(new Notification("drone", $"capacidade do drone não pode ser maior que {Utility.Utils.CARGA_MAXIMA_GRAMAS / 1000} KGs"));
                return _response;
            }

            var drone = _mapper.Map<CriarDroneCommand, Drone>(request);

            await _unitOfWork.Drones.AdicionarAsync(drone);
            await _unitOfWork.SaveAsync();

            return _response;
        }
    }
}
