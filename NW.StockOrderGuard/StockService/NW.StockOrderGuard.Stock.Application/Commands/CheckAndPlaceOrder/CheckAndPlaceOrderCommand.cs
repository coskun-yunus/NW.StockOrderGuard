using MediatR;
using System.Collections.Generic;
using NW.StockOrderGuard.Stock.Application.Dto;

namespace NW.StockOrderGuard.Stock.Application.Commands.CheckAndPlaceOrder
{
    public class CheckAndPlaceOrderCommand : IRequest<List<OrderDto>>
    {
    }
} 