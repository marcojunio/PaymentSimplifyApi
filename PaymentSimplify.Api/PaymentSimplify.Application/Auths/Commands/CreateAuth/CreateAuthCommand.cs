using MediatR;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Common.Results;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.Repositories;
using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.Application.Auths.Commands.CreateAuth;

public sealed record CreateAuthCommand : IRequest<Result>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
}

public class CreateAuthCommandHandler : IRequestHandler<CreateAuthCommand, Result>
{
    private readonly IAuthRepository _authRepository;
    private readonly ICustumerRepository _custumerRepository;
    private readonly IHashService _hashService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthCommandHandler(
        IUnitOfWork unitOfWork,
        IHashService hashService,
        IAuthRepository authRepository, 
        ICustumerRepository custumerRepository)
    {
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _authRepository = authRepository;
        _custumerRepository = custumerRepository;
    }

    public async Task<Result> Handle(CreateAuthCommand request, CancellationToken cancellationToken)
    {
        var resultDocument = Document.Create(request.Document);

        if (resultDocument.IsError)
            return resultDocument;

        var resultEmail = Email.Create(request.Email);

        if (resultEmail.IsError)
            return resultEmail;

        if (await _authRepository.EmailAlreadyExists(request.Email))
            return Result.Error("E-mail already exists");
        
        if (await _custumerRepository.DocumentAlreadyExists(request.Document))
            return Result.Error("Document already exists");

        var salt = Guid.NewGuid().ToString();

        var hashPassword = _hashService.Create(request.Password, salt);

        var auth = new Auth(
            resultEmail.Value,
            hashPassword,
            salt,
            new Customer(
                request.FirstName,
                request.LastName,
                resultDocument.Value,
                new AccountBank(new Money("C", 0))
            )
        );

        _authRepository.Add(auth);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(auth.Id);
    }
}