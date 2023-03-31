
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.Exceptions;
using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.UnitTests.Domain.Entities;

public class TransactionTests
{

    [Fact]
    public void Most_Be_Success_Transaction()
    {
        //arrange
        var transaction = Setup("21980467099", "92182247009",100);
    
        //action
        transaction.CreateTransaction();
        
        //assert balance
        Assert.True(transaction.Payee.AccountBank.Balance.Amount == 600);
        Assert.True(transaction.Payer.AccountBank.Balance.Amount == 150);
        
        //assert length transaction register
        Assert.True(transaction.Payer.Transactions.Count == 1);
        Assert.True(transaction.Payee.Transactions.Count == 1);
    }
    
    [Fact]
    public void Most_Be_Fail_Transaction_By_Balance_Insuficient()
    {
        //arrange
        var transaction = Setup("21980467099", "92182247009",350);
    
        //assert exception
       Assert.Throws<TransactionBalanceInsufficientException>(() =>
       {
           transaction.CreateTransaction();
       });
       
       //assert balance
       Assert.True(transaction.Payee.AccountBank.Balance.Amount == 500);
       Assert.True(transaction.Payer.AccountBank.Balance.Amount == 250);
       
       //assert length transaction register
       Assert.True(transaction.Payer.Transactions.Count == 0);
       Assert.True(transaction.Payee.Transactions.Count == 0);
    }
    
    [Fact]
    public void Most_Be_Fail_Transaction_By_Payer_Invalid()
    {
        //arrange
        var transaction = Setup("21980467099", "46050440000154",100);
    
        //assert exception
        Assert.Throws<TransactionPayerInvalidTypeException>(() =>
        {
            transaction.CreateTransaction();
        });
        
        //assert balance
        Assert.True(transaction.Payee.AccountBank.Balance.Amount == 500);
        Assert.True(transaction.Payer.AccountBank.Balance.Amount == 250);
        
        //assert length transaction register
        Assert.True(transaction.Payer.Transactions.Count == 0);
        Assert.True(transaction.Payee.Transactions.Count == 0);
    }
    
    private Transaction Setup(string documentPayee,string documentPayeer,decimal amountTransfer)
    {
        var payee = new Customer("Payee", "last", Document.Create(documentPayee).Value);
        var payer = new Customer("Payer", "last", Document.Create(documentPayeer).Value);

        payee.AccountBank = new AccountBank(new Money("C", 500));
        payer.AccountBank = new AccountBank( new Money("C", 250));

        return new Transaction(new Money("C", amountTransfer), payer, payee);
    }
}