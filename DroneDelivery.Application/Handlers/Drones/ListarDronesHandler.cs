using AutoMapper;
using DroneDelivery.Application.Models;
using DroneDelivery.Application.Queries.Drones;
using DroneDelivery.Application.Response;
using DroneDelivery.Application.Validador;
using DroneDelivery.Data.Repositorios.IRepository;
using DroneDelivery.Domain.Entidades;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Handlers.Drones
{
    public class ListarDronesHandler : ValidatorResponse, IRequestHandler<ListarDronesQuery, ResponseVal>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListarDronesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseVal> Handle(ListarDronesQuery request, CancellationToken cancellationToken)
        {
            var drones = await _unitOfWork.Drones.ObterAsync();

            _response.AddValue(new
            {
                drones = _mapper.Map<IEnumerable<Drone>, IEnumerable<DroneModel>>(drones)
            });

            return _response;
        }
    }
}
