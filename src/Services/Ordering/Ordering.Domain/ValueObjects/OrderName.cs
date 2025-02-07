namespace Ordering.Domain.ValueObjects;

public  record OrderName
{
    private const int MaxLength = 5;

    public string Value { get; }
    
    protected OrderName() { }

    public OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        // ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, MaxLength);

        return new OrderName(value);
    }
}