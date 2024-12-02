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


public class CreateProductCommandValidatior:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidatior()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

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