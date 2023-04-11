using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Common.Settings;

namespace PaymentSimplify.Infra.Services;

public class JwtService : IJwtService
{
    private readonly AppSettings _appSettings;

    public JwtService(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public Dictionary<string, object> Decode(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;

    public string Encode(IList<Claim>? claims)
    {
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_appSettings.AuthenticatedSettings.Key));

        var tokenHandler = new JwtSecurityTokenHandler();

        //claims roles
        var subject = new ClaimsIdentity();
        claims ??= new List<Claim>();
        
        foreach (var claim in claims)
            subject.AddClaim(new Claim(claim.Type, claim.Value));
        
        var createdDate = DateTime.UtcNow;
        
        //token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
            Audience = _appSettings.AuthenticatedSettings.ValidAudience,
            Issuer = _appSettings.AuthenticatedSettings.ValidIssuer,
            IssuedAt = createdDate,
            NotBefore = createdDate
        };
        
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}