using DroneDelivery.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ObterAsync();

        Task<User> ObterPorEmailAsync(string email);

        Task<User> ObterAsync(Guid id);

        Task AdicionarAsync(User user);

    }
}
