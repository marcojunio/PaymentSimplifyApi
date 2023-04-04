using Microsoft.AspNetCore.Http;
using PaymentSimplify.Application.Common.Interfaces;

namespace PaymentSimplify.Infra.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetIdUser()
    {
        var claims = _httpContextAccessor.HttpContext?.User?.Claims;
        return claims?.FirstOrDefault(f => f.Type == "user_id")?.Value;
    }
}