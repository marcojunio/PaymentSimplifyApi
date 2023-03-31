namespace PaymentSimplify.Domain.Exceptions;

public class TransactionPayerInvalidTypeException : Exception
{
    public TransactionPayerInvalidTypeException()
    {
        
    }

    public TransactionPayerInvalidTypeException(string message) : base(message)
    {
        
    }
}