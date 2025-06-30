using System.Threading.Tasks;
using NW.StockOrderGuard.Product.Application.Contracts;
using NW.StockOrderGuard.Product.Domain.Entities;
using System.Collections.Generic;
using NW.StockOrderGuard.Product.Application.IntegrationEvents;

namespace NW.StockOrderGuard.Product.Application.UseCases
{
    public class TrackProductCatalogUseCase
    {
        private readonly IExternalProductCatalog _externalProductCatalog;
        private readonly IProductRepository _productRepository;
        private readonly IEventPublisher _eventPublisher;

        public TrackProductCatalogUseCase(IExternalProductCatalog externalProductCatalog, IProductRepository productRepository, IEventPublisher eventPublisher)
        {
            _externalProductCatalog = externalProductCatalog;
            _productRepository = productRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task SyncProductsAsync()
        {
            IEnumerable<Domain.Entities.Product> products = await _externalProductCatalog.FetchCurrentProductsAsync();
            foreach (var product in products)
            {
                await _productRepository.SaveAsync(product);
            }
           
            var eventDto = new ProductSyncedEvent
            {
                Products = products.Select(p => new ProductSyncedDto
                {
                    ProductCode = p.ProductCode,
                    Title = p.Title,
                    Price = p.Price,
                    CurrentStock = p.CurrentStock,
                    ThresholdStock = p.ThresholdStock,
                    BestPrice = p.GetBestOffer()
                }).ToList()
            };
            await _eventPublisher.PublishProductSyncedAsync(eventDto);
        }
    }
} 