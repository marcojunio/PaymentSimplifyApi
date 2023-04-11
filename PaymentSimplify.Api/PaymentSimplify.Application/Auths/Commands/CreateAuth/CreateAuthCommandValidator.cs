using FluentValidation;

namespace PaymentSimplify.Application.Auths.Commands.CreateAuth;

public class CreateAuthCommandValidator : AbstractValidator<CreateAuthCommand>
{
    public CreateAuthCommandValidator()
    {
        RuleFor(v => v.Document)
            .MaximumLength(14)
            .NotEmpty();
        
        RuleFor(v => v.Password)
            .MaximumLength(40)
            .NotEmpty();
        
        RuleFor(v => v.FirstName)
            .MaximumLength(60)
            .NotEmpty();
        
        RuleFor(v => v.LastName)
            .MaximumLength(60)
            .NotEmpty();
        
        RuleFor(v => v.Email)
            .MaximumLength(80)
            .NotEmpty();
    }
}