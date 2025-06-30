using MediatR;
using System.Collections.Generic;
using NW.StockOrderGuard.Stock.Domain.Entities;

namespace NW.StockOrderGuard.Stock.Application.Queries.GetLowStockProducts
{
    public class GetLowStockProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
} 