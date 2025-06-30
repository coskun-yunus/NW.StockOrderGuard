using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NW.StockOrderGuard.Stock.Domain.Entities;
using NW.StockOrderGuard.Stock.Application.Contracts;
using System.Linq;

namespace NW.StockOrderGuard.Stock.Application.Queries.GetLowStockProducts
{
    public class GetLowStockProductsQueryHandler : IRequestHandler<GetLowStockProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetLowStockProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetLowStockAsync();
        }
    }
} 