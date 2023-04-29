using FluentValidation;

namespace PaymentSimplify.Application.Transactions.Commands;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(f => f.IdPayee)
            .NotEmpty()
            .NotNull();

        RuleFor(f => f.Amount)
            .NotNull()
            .GreaterThan(0);
    }
    
}