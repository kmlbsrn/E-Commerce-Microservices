
namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductRequest
{
    public string Name { get; init; } = default!;
    public List<string> Category { get; init; } = new();
    public string Description { get; init; } = default!;
    public string ImageFile { get; init; } = default!;
    public decimal Price { get; init; }
}

public sealed record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            
            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();
            
            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create a new product");
        
    }
}