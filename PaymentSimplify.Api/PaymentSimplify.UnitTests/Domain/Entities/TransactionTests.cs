
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
        transaction.CreateTransactionTransfer(_payer);
        
        //assert balance
        Assert.Equal(600,transaction.AccountBankPayee.Balance.Amount);
        Assert.Equal(150,transaction.AccountBankPayer.Balance.Amount);
        
        //assert length transaction register
        Assert.Equal(1,transaction.AccountBankPayer.TransactionsPayer.Count);
        Assert.Equal(1,transaction.AccountBankPayee.TransactionsPayee.Count);
    }
    
    [Fact]
    public void Most_Be_Fail_Transaction_By_Balance_Insuficient()
    {
        //arrange
        var transaction = Setup("21980467099", "92182247009",350);
    
        //assert exception
       Assert.Throws<TransactionBalanceInsufficientException>(() =>
       {
           transaction.CreateTransactionTransfer(_payer);
       });
       
       //assert balance
       Assert.Equal(500,transaction.AccountBankPayee.Balance.Amount);
       Assert.Equal(250,transaction.AccountBankPayer.Balance.Amount);
       
       //assert length transaction register
       Assert.Equal(0,transaction.AccountBankPayee.TransactionsPayee.Count);
       Assert.Equal(0,transaction.AccountBankPayer.TransactionsPayer.Count);
    }
    
    [Fact]
    public void Most_Be_Fail_Transaction_By_Payer_Invalid()
    {
        //arrange
        var transaction = Setup("21980467099", "46050440000154",100);
    
        //assert exception
        Assert.Throws<TransactionPayerInvalidTypeException>(() =>
        {
            transaction.CreateTransactionTransfer(_payer);
        });
        
        //assert balance
        Assert.Equal(500,transaction.AccountBankPayee.Balance.Amount);
        Assert.Equal(250,transaction.AccountBankPayer.Balance.Amount);
        
        //assert length transaction register
        Assert.Equal(0,transaction.AccountBankPayee.TransactionsPayee.Count);
        Assert.Equal(0,transaction.AccountBankPayer.TransactionsPayer.Count);
    }
    
    private Transaction Setup(string documentPayee,string documentPayeer,decimal amountTransfer)
    {
        var payee = new Customer("Payee", "last", Document.Create(documentPayee).Value,new AccountBank(new Money("C",50)));
        var payer = new Customer("Payer", "last", Document.Create(documentPayeer).Value,new AccountBank(new Money("C",50)));

        payee.AccountBank = new AccountBank(new Money("C", 500));
        payer.AccountBank = new AccountBank( new Money("C", 250));

        _payer = payer;
        
        return new Transaction(new Money("C", amountTransfer), payee.AccountBank,payer.AccountBank);
    }

    private Customer _payer;
}