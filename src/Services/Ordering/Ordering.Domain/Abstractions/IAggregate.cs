namespace Ordering.Domain.Abstractions;


public interface IAggregate<TKey>:IAggregate,IEntity<TKey>
{
    
}

public interface IAggregate: IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    
    IDomainEvent[] ClearDomainEvents();
}