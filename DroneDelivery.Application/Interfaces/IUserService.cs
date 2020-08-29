using DroneDelivery.Application.Models;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Response
{
    public interface IUserService
    {
        Task<ResponseVal> CriarUsuario(CreateUserModel user);
        Task<ResponseVal> Authentication(LoginModel user);
    }
}
