using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;

namespace DroneDelivery.Application.Commands.Drones
{
    public class CriarDroneCommand : Request<ResponseVal>
    {
        public double Capacidade { get; set; }

        public double Velocidade { get; set; }

        public double Autonomia { get; set; }

        public double Carga { get; set; }
    }
}
