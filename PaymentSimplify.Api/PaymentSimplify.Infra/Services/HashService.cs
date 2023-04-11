using System.Security.Cryptography;
using System.Text;
using PaymentSimplify.Application.Common.Interfaces;

namespace PaymentSimplify.Infra.Services;

public class HashService : IHashService
{
    public string Create(string value, string salt)
    {
        using var key = new Rfc2898DeriveBytes(value, Encoding.Default.GetBytes(salt), 10000, HashAlgorithmName.SHA512);

        return Convert.ToBase64String(key.GetBytes(512));
    }
}