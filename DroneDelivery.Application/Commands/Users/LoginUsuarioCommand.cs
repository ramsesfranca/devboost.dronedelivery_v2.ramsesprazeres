using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;

namespace DroneDelivery.Application.Commands.Users
{
    public class LoginUsuarioCommand : Request<ResponseVal>
    {
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
