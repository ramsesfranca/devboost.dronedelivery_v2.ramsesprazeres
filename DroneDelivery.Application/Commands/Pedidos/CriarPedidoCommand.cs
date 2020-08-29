using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;

namespace DroneDelivery.Application.Commands.Pedidos
{
    public class CriarPedidoCommand : Request<ResponseVal>
    {
        public double Peso { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
