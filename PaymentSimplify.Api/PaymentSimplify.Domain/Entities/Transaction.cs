using PaymentSimplify.Domain.Exceptions;

namespace PaymentSimplify.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    public Transaction(Money amount,Customer payer,Customer payee)
    {
        Amount = amount;
        Payer = payer;
        Payee = payee;
    }
    
    public Money Amount { get;  }
    public virtual Customer Payer { get; }
    public virtual Customer Payee { get; }
    
    public void CreateTransaction()
    {
        if (!Payer.AccountBank.BalanceSuficient(Amount))
            throw new TransactionBalanceInsufficientException("Insufficient balance to complete a transaction.");

        if (Payer.Document.TypeDocument == TypeDocumentEnum.Cnpj)
            throw new TransactionPayerInvalidTypeException("It is not possible for a shopkeeper to be a payer.");
        
        //remove credit for Payer
        Payer.AccountBank.WithdrawMoney(Amount);
        
        //Increment credit for Payee
        Payee.AccountBank.AddCredit(Amount);
        
        //register transaction;
        Payer.AddTransaction(this);
        Payee.AddTransaction(this);
    }
}
