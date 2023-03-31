using System.Text.RegularExpressions;
using PaymentSimplify.Common.Results;

namespace PaymentSimplify.Domain.ValueObjects;

public class Email : ValueObject
{
    public static Result<Email> Create(string value) => IsEmail(value) ? Result<Email>.Success() : Result<Email>.Error("E-mail invalid.");

    public string Addreess { get;}
    
    private Email(string addreess)
    {
        Addreess = addreess;
    }
    
    public static bool IsEmail(string s)
    {
        if (string.IsNullOrEmpty(s))
            return false;
            
        var regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,15}|[0-9]{1,3})(\]?)$");

        if (!regex.IsMatch(s))
            return false;
        
        return s.Length <= 80;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Addreess;
    }
}