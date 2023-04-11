namespace PaymentSimplify.Domain.Entities;

public class AccountBank : BaseAuditableEntity
{
    public AccountBank(Money balance)
    {
        Balance = balance;
        _transactions = new List<Transaction>();
    }

    private AccountBank()
    {
        _transactions = new List<Transaction>();
    }
    
    public Money Balance { get; } = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;
    
    private List<Transaction> _transactions;
    public bool BalanceSuficient(Money money) => Balance.Amount >= money.Amount;

    public void AddCredit(Money money) => Balance.IncrementAmount(money.Amount);

    public void WithdrawMoney(Money money) => Balance.DecrementAmount(money.Amount);
    public void AddTransaction(Transaction transaction) => _transactions.Add(transaction);

}