namespace Basket.API.Basket.DeleteBasket;


public sealed record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>; 

public sealed record DeleteBasketResult(bool IsSuccess);


public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName cannot be empty");
    }
}


public class DeleteBasketCommandHandler(IBasketRepository repository):ICommandHandler<DeleteBasketCommand,DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}