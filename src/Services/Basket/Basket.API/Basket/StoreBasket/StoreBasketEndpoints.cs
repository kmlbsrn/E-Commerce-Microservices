

namespace Basket.API.Basket.StoreBasket;

public sealed record StoreBasketRequest(ShoppingCart Cart);

public sealed record StoreBasketResponse(string UserName);


public class StoreBasketEndpoints:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                
                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();
                
                return Results.Created($"/basket/{response.UserName}",response);
            }
        ).WithName("CreateBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Creates a new shopping cart.")
        .WithDescription("Creates a new shopping cart.");
    }
}