using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NW.StockOrderGuard.Stock.Application.IntegrationEvents;
using NW.StockOrderGuard.Stock.Application.Contracts;

namespace NW.StockOrderGuard.Stock.Application.Commands.SyncProducts
{
    public class SyncProductsCommandHandler : IRequestHandler<SyncProductsCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        public SyncProductsCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(SyncProductsCommand request, CancellationToken cancellationToken)
        {
            foreach (var dto in request.Event.Products)
            {
                await _productRepository.SaveAsync(new Domain.Entities.Product(
                    dto.ProductCode,
                    dto.Title,
                    dto.Price,
                    dto.BestPrice,
                    dto.CurrentStock,
                    dto.ThresholdStock
                ));
            }
            return Unit.Value;
        }
    }
} 