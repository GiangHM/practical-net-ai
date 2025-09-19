using FlowerShop.Application.Dtos;
using FlowerShop.Domain.Interfaces;

namespace FlowerShop.Application.Features.Flowers.Queries
{
    // Declare this interface instead of using MediatR for simplicity
    public interface IFlowerGetAllHandler<R>
    {
        Task<R> Handle(CancellationToken cancellationToken);
    }
    public class FlowerGetAll : IFlowerGetAllHandler<IEnumerable<FlowerResponseItem>>
    {
        private readonly IFlowerResponsitory _responitory;
        public FlowerGetAll(IFlowerResponsitory responitory)
        {
            _responitory = responitory;
        }
        public async Task<IEnumerable<FlowerResponseItem>> Handle(CancellationToken cancellationToken)
        {
            var flowers = await _responitory.GetAllAsync();
            var result = flowers.Select(f => new FlowerResponseItem
            {
                Id = f.Id,
                Name = f.FlowerName,
                CategoryName = "",//TODO: get category name
                UnitPrice = f.UnitPrice.Price.Amount,
                UnitCurrency = f.UnitPrice.Price.Currency,
            });
            return result;
        }
    }
}
