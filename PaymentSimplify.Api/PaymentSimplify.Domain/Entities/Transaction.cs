using PaymentSimplify.Domain.Exceptions;

namespace PaymentSimplify.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    public Transaction(Money amount, AccountBank accountBankPayee,AccountBank accountBankPayer)
    {
        Amount = amount;
        AccountBankPayee = accountBankPayee;
        AccountBankPayer = accountBankPayer;
    }
    
    public Transaction(Money amount)
    {
        Amount = amount;
    }

    private Transaction()
    {
        
    }

    public Money Amount { get; } = null!;
    public virtual AccountBank AccountBankPayee { get; } = null!;
    public virtual AccountBank AccountBankPayer { get; } = null!;

    public void CreateTransactionTransfer(Customer payer)
    {
        if (!AccountBankPayer.BalanceSuficient(Amount))
            throw new TransactionBalanceInsufficientException("Insufficient balance to complete a transaction.");

        if (payer.Document.TypeDocument == TypeDocumentEnum.Cnpj)
            throw new TransactionPayerInvalidTypeException("It is not possible for a shopkeeper to be a payer.");

        //remove credit for Payer
        AccountBankPayer.WithdrawMoney(Amount);

        //Increment credit for Payee
        AccountBankPayee.AddCredit(Amount);

        //register transaction;
        AccountBankPayer.AddTransactionPayer(this);
        AccountBankPayee.AddTransactionPayee(this);
    }
}