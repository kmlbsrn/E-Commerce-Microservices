using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);


public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x=>x.Order.OrderName).NotEmpty().WithMessage("OrderName is required");
        RuleFor(x=>x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x=>x.Order.OrderItems).NotNull().WithMessage("OrderItems is required");
    }
}