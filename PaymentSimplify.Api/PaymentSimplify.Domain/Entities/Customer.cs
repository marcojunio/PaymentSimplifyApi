namespace PaymentSimplify.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public Customer(
        string firsname,
        string lastName,
        Document document)
    {
        FistName = firsname;
        LastName = lastName;
        Document = document;
        _transactions = new List<Transaction>();
    }

    public virtual AccountBank AccountBank { get; set; }
    public Document Document { get; private set; }
    public string FistName { get; private set;}
    public string LastName { get; private set; }

    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public void ChangeFirstName(string newFirstName)
    {
        FistName = newFirstName;
    }

    public void ChangeLastName(string newLastName)
    {
        LastName = newLastName;
    }

    public void ChangeDocument(Document document)
    {
        Document = document;
    }

    public void AddTransaction(Transaction transaction) => _transactions.Add(transaction);

    private List<Transaction> _transactions;
}