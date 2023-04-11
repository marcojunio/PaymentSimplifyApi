using PaymentSimplify.Common.Exceptions;

namespace PaymentSimplify.Domain.Exceptions;

public class TransactionBalanceInsufficientException : CoreException
{
    public TransactionBalanceInsufficientException(string message) : base(message)
    {
        
    }
}