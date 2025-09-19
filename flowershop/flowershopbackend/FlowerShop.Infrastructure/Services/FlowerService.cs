using FlowerShop.Application.Dtos;
using FlowerShop.Application.Features.Flowers.Queries;
using FlowerShop.Application.Interfaces;

namespace FlowerShop.Infrastructure.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerGetAllHandler<IEnumerable<FlowerResponseItem>> _getAllHandler;
        public FlowerService(IFlowerGetAllHandler<IEnumerable<FlowerResponseItem>> getAllHandler)
        {
            _getAllHandler = getAllHandler;
        }
        public async Task<IEnumerable<FlowerResponseItem>> GetAllFlowersAsync(CancellationToken cancellationToken = default)
        {
            return await _getAllHandler.Handle(cancellationToken);
        }
    }
}
