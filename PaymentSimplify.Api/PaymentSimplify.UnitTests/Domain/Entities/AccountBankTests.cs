using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.UnitTests.Domain.Entities;

public class AccountBankTests
{
    [Fact]
    public void Most_Be_Add_Credit_Success()
    {
        //arrange
        var accountBank = new AccountBank(new Money("C", 50));

        //action
        accountBank.AddCredit(new Money("C",50));

        //assert
        Assert.Equal(100,accountBank.Balance.Amount);
    }
    
    [Fact]
    public void Most_Be_Withdraw_Money_Success()
    {
        //arrange
        var accountBank = new AccountBank(new Money("C", 50));

        //action
        accountBank.WithdrawMoney(new Money("C",50));

        //assert
        Assert.Equal(0,accountBank.Balance.Amount);
    }
}