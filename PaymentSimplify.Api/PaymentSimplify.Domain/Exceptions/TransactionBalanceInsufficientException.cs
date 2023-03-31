namespace PaymentSimplify.Domain.Exceptions;

public class TransactionBalanceInsufficientException : Exception
{
    public TransactionBalanceInsufficientException(string message) : base(message)
    {
        
    }

    public TransactionBalanceInsufficientException()
    {
        
    }
}