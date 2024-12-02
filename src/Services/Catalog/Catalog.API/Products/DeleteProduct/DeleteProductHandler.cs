namespace Catalog.API.Products.DeleteProduct;


public sealed record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;

public sealed record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandHandler(IDocumentSession session): ICommandHandler<DeleteProductCommand,DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        
        return new DeleteProductResult(true);
    }
}