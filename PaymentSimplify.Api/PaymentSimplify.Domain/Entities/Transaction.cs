using PaymentSimplify.Domain.Exceptions;

namespace PaymentSimplify.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    public Transaction(Money amount, Customer payer, Customer payee, AccountBank accountBank)
    {
        Amount = amount;
        Payer = payer;
        Payee = payee;
        AccountBank = accountBank;
    }
    
    public Transaction(Money amount, Customer payer, Customer payee)
    {
        Amount = amount;
        Payer = payer;
        Payee = payee;
    }

    private Transaction()
    {
        
    }

    private Money Amount { get; } = null!;
    public Customer Payer { get; } = null!;
    public virtual Customer Payee { get; } = null!;
    public virtual AccountBank AccountBank { get; } = null!;

    public void CreateTransactionTransfer()
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
        Payer.AccountBank.AddTransaction(this);
        Payee.AccountBank.AddTransaction(this);
    }
}