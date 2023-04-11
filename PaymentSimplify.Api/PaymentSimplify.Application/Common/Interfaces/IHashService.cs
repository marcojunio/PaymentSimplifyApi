namespace PaymentSimplify.Application.Common.Interfaces;

public interface IHashService
{
    public string Create(string value, string salt);
}