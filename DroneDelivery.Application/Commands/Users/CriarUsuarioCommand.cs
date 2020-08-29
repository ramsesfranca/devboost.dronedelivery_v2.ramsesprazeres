using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;

namespace DroneDelivery.Application.Commands.Users
{
    public class CriarUsuarioCommand : Request<ResponseVal>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }
    }
}
