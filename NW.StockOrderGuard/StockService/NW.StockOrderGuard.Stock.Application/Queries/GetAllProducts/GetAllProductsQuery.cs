using MediatR;
using NW.StockOrderGuard.Stock.Domain.Entities;
using System.Collections.Generic;

namespace NW.StockOrderGuard.Stock.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}