namespace Basket.API.Basket.GetBasket;

public sealed record GetBasketRequest(string UserName);

public sealed record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                var response = result.Adapt<GetBasketResponse>();
                
                return Results.Ok(response);
            }
        )
        .WithName("GetBasketByUserName")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Retrieves the shopping cart of the user.")
        .WithDescription("Retrieves the shopping cart of the user.");
    }
}