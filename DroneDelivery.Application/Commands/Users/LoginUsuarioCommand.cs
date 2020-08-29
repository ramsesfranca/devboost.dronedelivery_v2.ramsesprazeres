using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;
using Flunt.Validations;

namespace DroneDelivery.Application.Commands.Users
{
    public class LoginUsuarioCommand : Request<ResponseVal>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Email, nameof(Email), "O Email não pode ser vazio"));
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Password, nameof(Password), "O Senha não pode ser vazio"));
        }
    }
}
