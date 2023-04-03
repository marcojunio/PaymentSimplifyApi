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

    private Money Amount { get; }
    public virtual Customer Payer { get; }
    public virtual Customer Payee { get; }
    public virtual AccountBank AccountBank { get; }

    public void CreateTransactionTransfer()
    {
        if (!AccountBank.BalanceSuficient(Amount))
            throw new TransactionBalanceInsufficientException("Insufficient balance to complete a transaction.");

        if (Payer.Document.TypeDocument == TypeDocumentEnum.Cnpj)
            throw new TransactionPayerInvalidTypeException("It is not possible for a shopkeeper to be a payer.");

        //remove credit for Payer
        AccountBank.WithdrawMoney(Amount);

        //Increment credit for Payee
        Payee.AccountBank.AddCredit(Amount);

        //register transaction;
        AccountBank.AddTransaction(this);
        Payee.AccountBank.AddTransaction(this);
    }
}