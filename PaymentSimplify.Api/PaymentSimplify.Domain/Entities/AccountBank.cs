namespace PaymentSimplify.Domain.Entities;

public class AccountBank : BaseAuditableEntity
{
    public AccountBank(Money balance)
    {
        Balance = balance;
    }
    
    public Money Balance { get; }
    public virtual Customer Customer { get; }

    public bool BalanceSuficient(Money money) => Balance.Amount >= money.Amount;

    public void AddCredit(Money money) => Balance.IncrementAmount(money.Amount);

    public void WithdrawMoney(Money money) => Balance.DecrementAmount(money.Amount);

}