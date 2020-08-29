using DroneDelivery.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Cliente>> ObterAsync();

        Task<Cliente> ObterPorEmailAsync(string email);

        Task<Cliente> ObterAsync(Guid id);

        Task AdicionarAsync(Cliente usuario);

    }
}
