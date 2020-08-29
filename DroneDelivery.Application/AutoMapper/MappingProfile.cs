using AutoMapper;
using DroneDelivery.Application.Models;
using DroneDelivery.Domain.Entidades;
using DroneDelivery.Domain.Enum;
using System;
using System.Linq;

namespace DroneDelivery.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Model to Domain
            CreateMap<DroneModel, Drone>();
            CreateMap<CreateDroneModel, Drone>()
                .ForMember(d => d.Status, o => o.MapFrom(x => DroneStatus.Livre));
            CreateMap<PedidoModel, Pedido>();

            CreateMap<CreatePedidoModel, Pedido>()
                .ForMember(d => d.DataPedido, o => o.MapFrom(x => DateTime.Now));


            //Domain to Model
            CreateMap<Drone, DroneModel>()
                .ForMember(d => d.Status, opts => opts.MapFrom(x => x.Status.ToString()));
            CreateMap<Pedido, PedidoModel>();

            CreateMap<Pedido, DronePedidoModel>();


            CreateMap<Drone, DroneSituacaoModel>()
                .ForMember(d => d.Situacao, opts => opts.MapFrom(x => x.Status.ToString()))
                .ForMember(d => d.Pedidos, opts => opts.MapFrom(x => x.Pedidos));
        }

    }
}
