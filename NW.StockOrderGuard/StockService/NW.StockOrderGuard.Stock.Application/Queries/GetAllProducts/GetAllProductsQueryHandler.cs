using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NW.StockOrderGuard.Stock.Domain.Entities;
using NW.StockOrderGuard.Stock.Application.Contracts;

namespace NW.StockOrderGuard.Stock.Application.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
} 