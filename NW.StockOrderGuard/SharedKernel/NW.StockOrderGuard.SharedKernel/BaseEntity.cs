using System;

namespace NW.StockOrderGuard.SharedKernel
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
    }
} 