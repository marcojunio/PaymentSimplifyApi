namespace PaymentSimplify.Domain.ValueObjects;

public class Money : ValueObject
{
    public Money(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }
    
    public string Currency { get; }    
    public decimal Amount { get; private set; }

    public void IncrementAmount(decimal value)
    {
        Amount += value;
    }
    
    public void DecrementAmount(decimal value)
    {
        Amount -= value;
    } 
        
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency;
        yield return Amount;
    }
}