using NW.StockOrderGuard.Product.Application.Commands.UpdateProductStock;
using NW.StockOrderGuard.Product.Application.Contracts;
using NW.StockOrderGuard.Product.Application.IntegrationEvents;
using NW.StockOrderGuard.SharedKernel;

namespace NW.StockOrderGuard.Product.Application.UseCases
{
    public class UpdateProductStockUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventPublisher _eventPublisher;

        public UpdateProductStockUseCase(IProductRepository productRepository, IEventPublisher eventPublisher)
        {
            _productRepository = productRepository;
            _eventPublisher = eventPublisher;

        }

        public async Task<Result> HandleAsync(UpdateProductStockCommand dto)
        {
            var allProducts = await _productRepository.GetAllAsync();
            var product = allProducts.FirstOrDefault(p => p.Title == dto.ProductName);
            if (product == null)
                return Result.Failure("Product not found.");
            product.UpdateStock(dto.ThresholdStock, dto.CurrentStock);
            await _productRepository.SaveAsync(product);

            ProductSyncedDto _productSyncedDto = new ProductSyncedDto
            {
                ProductCode = product.ProductCode,
                Title = product.Title,
                Price = product.Price,
                CurrentStock = product.CurrentStock,
                ThresholdStock = product.ThresholdStock,
                BestPrice = product.GetBestOffer()
            };
            var eventDto = new ProductSyncedEvent();
            eventDto.Products.Add(_productSyncedDto);
            await _eventPublisher.PublishProductSyncedAsync(eventDto);

            return Result.Success();
        }
    }
} 