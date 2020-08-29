using AutoMapper;
using DroneDelivery.Application.Models;
using DroneDelivery.Application.Queries.Pedidos;
using DroneDelivery.Application.Response;
using DroneDelivery.Application.Validador;
using DroneDelivery.Data.Repositorios.IRepository;
using DroneDelivery.Domain.Entidades;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Handlers.Pedidos
{
    public class ListarPedidosHandler : ValidatorResponse, IRequestHandler<ListarPedidosQuery, ResponseVal>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListarPedidosHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseVal> Handle(ListarPedidosQuery request, CancellationToken cancellationToken)
        {

            var pedidos = await _unitOfWork.Pedidos.ObterAsync();

            _response.AddValue(new
            {
                pedidos = _mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoModel>>(pedidos)
            });

            return _response;
        }
    }
}
