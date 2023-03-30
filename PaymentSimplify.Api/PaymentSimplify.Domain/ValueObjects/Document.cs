namespace PaymentSimplify.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(string value, TypeDocumentEnum typeDocument)
    {
        Value = value;
        TypeDocument = typeDocument;
    }
    public string Value { get; }
    public TypeDocumentEnum TypeDocument { get; }


    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Value))
            return false;

        return TypeDocument switch
        {
            TypeDocumentEnum.Cpf => Value.Length == 11,
            TypeDocumentEnum.Cnpj => Value.Length == 14,
            _ => throw new ArgumentOutOfRangeException($"TypeDocumet in {nameof(Document)}")
        };
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        return new IComparable[]
        {
            Value,
            TypeDocument
        };
    }
}