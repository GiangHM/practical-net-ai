using FlowerShop.Domain.Entities;
using FlowerShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Infrastructure.Persistence
{
    public class FlowerRespository : IFlowerResponsitory
    {
        private readonly FlowerShopDbContext _context;
        public FlowerRespository(FlowerShopDbContext context)
        {
            _context = context;
        }
        public async Task<Flower> AddAsync(Flower item)
        {
            var entry = await _context.Flowers.AddAsync(item);
            return entry.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Flowers.Where(f => f.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Flower>> GetAllAsync()
        {
            return await _context.Flowers
                .Include(o => o.UnitPrice)
                .Include(o => o.CategoryId)
                .ToListAsync();
        }

        public Task<Flower?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Flower item)
        {
            ArgumentNullException.ThrowIfNull(item);
            _context.Flowers.Update(item);
            await Task.CompletedTask;
        }
    }
}
