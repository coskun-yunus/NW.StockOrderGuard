using MediatR;
using System.Collections.Generic;
using NW.StockOrderGuard.Stock.Application.Dto;

namespace NW.StockOrderGuard.Stock.Application.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>
    {
    }
} 