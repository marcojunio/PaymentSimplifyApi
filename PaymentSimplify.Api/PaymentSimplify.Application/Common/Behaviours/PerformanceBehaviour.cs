using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentSimplify.Application.Common.Interfaces;

namespace PaymentSimplify.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public PerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _timer = new Stopwatch();
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        
        _timer.Start();

        var response = await next();
        
        _timer.Stop();

        var elapsedMiliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMiliseconds <= 500) return response;
        
        var requestName = typeof(TRequest);
        var userId = _currentUserService.GetIdUser() ?? string.Empty;
        _logger.LogWarning("PaymentSimplify Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
            requestName, elapsedMiliseconds, userId, request);

        return response;
    }
}