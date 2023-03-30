using PaymentSimplify.Common.Results;
using PaymentSimplify.Common.Strings;

namespace PaymentSimplify.Domain.ValueObjects;

public class Document : ValueObject
{
    public static Result<Document> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result<Document>.Error("Document is empty.");

        TypeDocumentEnum? typeDocument = null;
        
        if (IsCnpj(value))
            typeDocument = TypeDocumentEnum.Cnpj;

        if (IsCpf(value))
            typeDocument = TypeDocumentEnum.Cpf;
        
        return typeDocument is null ? Result<Document>.Error("Document is invalid.") : Result<Document>.Success(new Document(value,(TypeDocumentEnum)typeDocument));
    }

    public string Value { get; }
    public TypeDocumentEnum TypeDocument { get; }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return TypeDocument;
    }

    private Document(string value,TypeDocumentEnum typeDocument)
    {
        Value = value;
        TypeDocument = typeDocument;
    }

    private static bool IsCpf(string cpf)
    {
        try
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            if (cpf.ValueOnlyRepeat())
                return false;

            var multiply1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiply2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf
                .Replace(".", "")
                .Replace("-", "");

            if (cpf.Length != 11)
                return false;

            var tempCpf = cpf[..9];
            var sum = 0;

            for (var i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiply1[i];
            var rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            var digit = rest.ToString();
            tempCpf += digit;
            sum = 0;
            for (var i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiply2[i];
            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit += rest;

            return cpf.EndsWith(digit);
        }
        catch
        {
            return false;
        }
    }

    private static bool IsCnpj(string cnpj)
    {
        try
        {
            if (string.IsNullOrEmpty(cnpj))
                return false;

            if (cnpj.ValueOnlyRepeat())
                return false;

            var multiply1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiply2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj
                .Replace(".", "")
                .Replace("-", "")
                .Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            var tempCnpj = cnpj[..12];

            var sum = 0;

            for (var i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiply1[i];
            var rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            var digit = rest.ToString();
            tempCnpj += digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiply2[i];
            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit += rest;

            return cnpj.EndsWith(digit);
        }
        catch
        {
            return false;
        }
    }
}