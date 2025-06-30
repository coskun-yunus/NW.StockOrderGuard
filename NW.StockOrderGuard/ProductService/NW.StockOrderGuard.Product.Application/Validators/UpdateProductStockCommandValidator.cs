using FluentValidation;
using NW.StockOrderGuard.Product.Application.Commands.UpdateProductStock;

namespace NW.StockOrderGuard.Product.Application.Validators
{
    public class UpdateProductStockCommandValidator : AbstractValidator<UpdateProductStockCommand>
    {
        public UpdateProductStockCommandValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Ürün adı zorunludur.");

            RuleFor(x => x.ThresholdStock)
                .GreaterThanOrEqualTo(0).WithMessage("Kritik stok değeri sıfır veya daha büyük olmalıdır.");

            RuleFor(x => x.CurrentStock)
                .GreaterThanOrEqualTo(0).WithMessage("Mevcut stok değeri sıfır veya daha büyük olmalıdır.");
        }
    }
} 