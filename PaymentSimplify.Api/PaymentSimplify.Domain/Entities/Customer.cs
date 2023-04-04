namespace PaymentSimplify.Domain.Entities;

public sealed class Customer : BaseAuditableEntity
{
    public Customer(
        string firsname,
        string lastName,
        Document document, 
        AccountBank accountBank)
    {
        FistName = firsname;
        LastName = lastName;
        Document = document;
        AccountBank = accountBank;
    }

    private Customer()
    {
    }

    public AccountBank AccountBank { get; set; } = null!;
    public Document Document { get; } = null!;
    public string FistName { get; } = null!;
    public string LastName { get; } = null!;
}