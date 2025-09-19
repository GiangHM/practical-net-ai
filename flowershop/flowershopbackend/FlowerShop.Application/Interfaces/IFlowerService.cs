using FlowerShop.Application.Dtos;

namespace FlowerShop.Application.Interfaces
{
    public interface IFlowerService
    {
        Task<IEnumerable<FlowerResponseItem>> GetAllFlowersAsync(CancellationToken cancellationToken = default);
    }
}
