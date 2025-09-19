using FlowerShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Domain.Interfaces
{
    public interface IFlowerResponsitory
    {
        Task<Flower?> GetByIdAsync(int id);
        Task<IEnumerable<Flower>> GetAllAsync();
        Task<Flower> AddAsync(Flower item);
        Task UpdateAsync(Flower item);
        Task DeleteAsync(int id);
    }
}
