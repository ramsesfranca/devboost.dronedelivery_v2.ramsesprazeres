using DroneDelivery.Data.Data;
using DroneDelivery.Domain.Entidades;
using DroneDelivery.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Data.Repositorios
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DroneDbContext _context;

        public PedidoRepository(DroneDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pedido>> ObterAsync()
        {
            return await _context.Pedidos.Include(x => x.Drone).ToListAsync();
        }

        public async Task<Pedido> ObterAsync(Guid id)
        {
            return await _context.Pedidos.Include(x => x.Drone).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoverAsync(Guid id)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
            _context.Remove(pedido);
        }

        public void Remover(Pedido pedido)
        {
            _context.Remove(pedido);
        }


    }
}
