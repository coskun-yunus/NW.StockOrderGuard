using MediatR;
using NW.StockOrderGuard.SharedKernel;

namespace NW.StockOrderGuard.Product.Application.Commands.UpdateProductStock
{
    public class UpdateProductStockCommand : IRequest<Result>
    {
        public string ProductName { get; set; }
        public int ThresholdStock { get; set; }
        public int CurrentStock { get; set; }
    }
} 