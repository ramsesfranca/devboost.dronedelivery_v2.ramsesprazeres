using DroneDelivery.Data.Data;
using DroneDelivery.Domain.Entidades;
using DroneDelivery.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Data.Repositorios
{
    public class UserRepository : IUserRepository
    {
        private readonly DroneDbContext _context;

        public UserRepository(DroneDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<User>> ObterAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> ObterAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task AdicionarAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> ObterPorEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
