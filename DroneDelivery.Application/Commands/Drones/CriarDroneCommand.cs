using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;
using Flunt.Validations;

namespace DroneDelivery.Application.Commands.Drones
{
    public class CriarDroneCommand : Request<ResponseVal>
    {
        public double Capacidade { get; set; }

        public double Velocidade { get; set; }

        public double Autonomia { get; set; }

        public double Carga { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Capacidade, 0, nameof(Capacidade), "A Capacidade tem que ser maior que zero"));

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Velocidade, 0, nameof(Velocidade), "A Velocidade tem que ser maior que zero"));

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Autonomia, 0, nameof(Autonomia), "A Autonomia tem que ser maior que zero"));

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Carga, 0, nameof(Carga), "A Carga tem que ser maior que zero"));
        }
    }
}
