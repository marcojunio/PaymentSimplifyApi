namespace PaymentSimplify.Domain.Entities;

public class AccountBank : BaseAuditableEntity
{
    public AccountBank(Money balance)
    {
        Balance = balance;
        _transactionsPayee = new List<Transaction>();
        _transactionsPayer = new List<Transaction>();
    }


    
    public Money Balance { get; } = null!;
    public IReadOnlyCollection<Transaction> TransactionsPayer => _transactionsPayer;
    public IReadOnlyCollection<Transaction> TransactionsPayee => _transactionsPayee;
    

    public bool BalanceSuficient(Money money) => Balance.Amount >= money.Amount;

    public void AddCredit(Money money) => Balance.IncrementAmount(money.Amount);

    public void WithdrawMoney(Money money) => Balance.DecrementAmount(money.Amount);
    public void AddTransactionPayer(Transaction transaction) => _transactionsPayer.Add(transaction);
    public void AddTransactionPayee(Transaction transaction) => _transactionsPayee.Add(transaction);
    
    private AccountBank()
    {
        _transactionsPayee = new List<Transaction>();
        _transactionsPayer = new List<Transaction>();
    }
    
    private List<Transaction> _transactionsPayee;
    private List<Transaction> _transactionsPayer;

}