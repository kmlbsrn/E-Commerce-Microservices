namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductCommand: ICommand<CreateProductResult>
{
    public string Name { get; init; } = default!;
    public List<string> Category { get; init; } = new();
    public string Description { get; init; } = default!;
    public string ImageFile { get; init; } = default!;
    public decimal Price { get; init; }
}

public sealed record CreateProductResult(Guid Id);

internal class CreateProductHandler(IDocumentSession session): ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}