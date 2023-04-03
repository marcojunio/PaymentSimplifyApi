
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
        transaction.CreateTransactionTransfer();
        
        //assert balance
        Assert.Equal(600,transaction.Payee.AccountBank.Balance.Amount);
        Assert.Equal(150,transaction.Payer.AccountBank.Balance.Amount);
        
        //assert length transaction register
        Assert.Equal(1,transaction.Payer.AccountBank.Transactions.Count);
        Assert.Equal(1,transaction.Payee.AccountBank.Transactions.Count);
    }
    
    [Fact]
    public void Most_Be_Fail_Transaction_By_Balance_Insuficient()
    {
        //arrange
        var transaction = Setup("21980467099", "92182247009",350);
    
        //assert exception
       Assert.Throws<TransactionBalanceInsufficientException>(() =>
       {
           transaction.CreateTransactionTransfer();
       });
       
       //assert balance
       Assert.Equal(500,transaction.Payee.AccountBank.Balance.Amount);
       Assert.Equal(250,transaction.Payer.AccountBank.Balance.Amount);
       
       //assert length transaction register
       Assert.Equal(0,transaction.Payer.AccountBank.Transactions.Count);
       Assert.Equal(0,transaction.Payee.AccountBank.Transactions.Count);
    }
    
    [Fact]
    public void Most_Be_Fail_Transaction_By_Payer_Invalid()
    {
        //arrange
        var transaction = Setup("21980467099", "46050440000154",100);
    
        //assert exception
        Assert.Throws<TransactionPayerInvalidTypeException>(() =>
        {
            transaction.CreateTransactionTransfer();
        });
        
        //assert balance
        Assert.Equal(500,transaction.Payee.AccountBank.Balance.Amount);
        Assert.Equal(250,transaction.Payer.AccountBank.Balance.Amount);
        
        //assert length transaction register
        Assert.Equal(0,transaction.Payer.AccountBank.Transactions.Count);
        Assert.Equal(0,transaction.Payee.AccountBank.Transactions.Count);
    }
    
    private Transaction Setup(string documentPayee,string documentPayeer,decimal amountTransfer)
    {
        var payee = new Customer("Payee", "last", Document.Create(documentPayee).Value,new AccountBank(new Money("C",50)));
        var payer = new Customer("Payer", "last", Document.Create(documentPayeer).Value,new AccountBank(new Money("C",50)));

        payee.AccountBank = new AccountBank(new Money("C", 500));
        payer.AccountBank = new AccountBank( new Money("C", 250));

        return new Transaction(new Money("C", amountTransfer), payer, payee,payer.AccountBank);
    }
}