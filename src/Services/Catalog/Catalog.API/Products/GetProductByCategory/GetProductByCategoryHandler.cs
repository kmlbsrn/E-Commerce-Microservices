namespace Catalog.API.Products.GetProductByCategoryHandler;

public sealed record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public sealed record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryQueryHandler(IDocumentSession session): IQueryHandler<GetProductByCategoryQuery,GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        
        return new GetProductByCategoryResult(products);
    }
}