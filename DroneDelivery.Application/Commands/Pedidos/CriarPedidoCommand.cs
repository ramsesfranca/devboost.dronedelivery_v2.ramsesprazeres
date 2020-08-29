using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;
using DroneDelivery.Utility;
using Flunt.Validations;

namespace DroneDelivery.Application.Commands.Pedidos
{
    public class CriarPedidoCommand : Request<ResponseVal>
    {
        public double Peso { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Peso, 0, nameof(Peso), "O Peso tem que ser maior que zero"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(Peso, Utils.CARGA_MAXIMA_GRAMAS, nameof(Peso), $"O Peso tem que ser menor ou igual a {Utils.CARGA_MAXIMA_GRAMAS / 1000} KGs"));

            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(Latitude, 0, nameof(Latitude), "A Latitude não pode ser vazia"));

            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(Longitude, 0, nameof(Longitude), "A Longitude não pode ser vazia"));
        }
    }
}
