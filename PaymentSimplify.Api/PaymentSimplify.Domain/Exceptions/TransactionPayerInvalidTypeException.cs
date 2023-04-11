using PaymentSimplify.Common.Exceptions;

namespace PaymentSimplify.Domain.Exceptions;

public class TransactionPayerInvalidTypeException : CoreException
{
    public TransactionPayerInvalidTypeException(string message) : base(message)
    {
        
    }
}