using NW.StockOrderGuard.SharedKernel;
using System;

namespace NW.StockOrderGuard.Product.Domain.ValueObjects
{
    public class Rating : IValueObject, IEquatable<Rating>
    {
        public decimal Rate { get; private set; }
        public int Count { get; private set; }

        public Rating(decimal rate, int count)
        {
            Rate = rate;
            Count = count;
        }

        public override bool Equals(object obj) => Equals(obj as Rating);

        public bool Equals(Rating other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Rate == other.Rate && Count == other.Count;
        }

        public override int GetHashCode() => HashCode.Combine(Rate, Count);
    }
} 