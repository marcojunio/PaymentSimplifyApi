using FluentValidation;

namespace PaymentSimplify.Application.Auths.Commands.AuthenticatedAuth;

public class AuthenticatedAuthCommandValidator : AbstractValidator<AuthenticatedAuthCommand>
{
    public AuthenticatedAuthCommandValidator()
    {
        RuleFor(f => f.Login)
            .NotEmpty();
        
        RuleFor(f => f.Password)
            .NotEmpty();
    }
}