using System.Security.Claims;

namespace PaymentSimplify.Application.Common.Interfaces;

public interface IJwtService
{
    Dictionary<string, object> Decode(string token);
    string Encode(IList<Claim>? claims);
}