using System.Security.Claims;
using MediatR;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Common.Results;
using PaymentSimplify.Domain.Repositories;

namespace PaymentSimplify.Application.Auths.Commands.AuthenticatedAuth;

public class AuthenticatedAuthCommand : IRequest<Result>
{
    public string Login { get; set; }
    public string Password { get; set; }
}

public class AuthenticatedAuthCommandHandler : IRequestHandler<AuthenticatedAuthCommand, Result>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;
    private readonly IHashService _hashService;
    private readonly ICurrentUserService _currentUserService;

    public AuthenticatedAuthCommandHandler(
        IAuthRepository authRepository,
        IJwtService jwtService,
        IHashService hashService, ICurrentUserService currentUserService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
        _hashService = hashService;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(AuthenticatedAuthCommand request, CancellationToken cancellationToken)
    {
        var user = _currentUserService.GetIdUser();

        var resultError = Result.Error("E-mail or password incorrect.");

        if (!await _authRepository.EmailAlreadyExists(request.Login))
            return resultError;

        var auth = await _authRepository.GetUserByEmail(request.Login);

        if (auth is null)
            return resultError;

        var hashedPasswordRequest = _hashService.Create(request.Password, auth.Salt);

        if (!string.Equals(auth.Password, hashedPasswordRequest))
            return resultError;

        return Result<AuthenticatedAuthResponse>.Success(new AuthenticatedAuthResponse()
        {
            Token = _jwtService.Encode(new List<Claim>()
            {
                new("user_id", auth.Id)
            })
        });
    }
}