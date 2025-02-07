namespace Ordering.Domain.Abstractions;


public interface IEntity<TKey> : IEntity
{
    public TKey Id { get; set; }
}

public interface IEntity
{
    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }
    
    public DateTime? LastModifiedAt { get; set; }
    
    public string? LastModifiedBy { get; set; }
}