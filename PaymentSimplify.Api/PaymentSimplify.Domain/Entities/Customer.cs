namespace PaymentSimplify.Domain.Entities;

public sealed class Customer : BaseAuditableEntity
{
    public Customer(
        string firsname,
        string lastName,
        Document document, AccountBank accountBank)
    {
        FistName = firsname;
        LastName = lastName;
        Document = document;
        AccountBank = accountBank;
    }

    public AccountBank AccountBank { get; set; }
    public Document Document { get; }
    public string FistName { get; }
    public string LastName { get; }

}