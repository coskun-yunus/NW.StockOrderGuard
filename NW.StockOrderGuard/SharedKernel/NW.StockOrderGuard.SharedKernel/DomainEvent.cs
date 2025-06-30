using System;

namespace NW.StockOrderGuard.SharedKernel
{
    public abstract class DomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }

    public interface IDomainEventHandler<TEvent> where TEvent : DomainEvent
    {
        void Handle(TEvent domainEvent);
    }
} 