using AutoMapper;
using DroneDelivery.Application.Models;
using DroneDelivery.Domain.Entidades;

namespace DroneDelivery.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Model to Domain
            CreateMap<DroneModel, Drone>();
            CreateMap<PedidoModel, Pedido>();


            //Domain to Model
            CreateMap<Drone, DroneModel>()
                .ForMember(d => d.Status, opts => opts.MapFrom(x => x.Status.ToString()));
            CreateMap<Pedido, PedidoModel>();


            CreateMap<Drone, DroneSituacaoModel>()
                .ForMember(d => d.Situacao, opts => opts.MapFrom(x => x.Status.ToString()))
                .ForMember(d => d.PedidoId, opts => opts.MapFrom(x => x.Pedido.Id));
        }

    }
}
