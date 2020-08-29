using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;
using Flunt.Validations;

namespace DroneDelivery.Application.Commands.Users
{
    public class CriarUsuarioCommand : Request<ResponseVal>
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "O Nome não pode ser vazio"));
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Senha, nameof(Senha), "O Senha não pode ser vazio"));
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Role, nameof(Role), "A Role não pode ser vazia"));
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Email, nameof(Email), "o Email não pode ser vazio"));
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(Latitude, 0, nameof(Latitude), "A Latitude não pode ser vazia"));
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(Longitude, 0, nameof(Longitude), "A Longitude não pode ser vazia"));
        }
    }
}
