using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.UnitTests.Domain.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Most_Be_Increment_Money()
    {
        //arrange
        var money = new Money("C",50m);
        
        //action
        money.IncrementAmount(50);

        //assert
        Assert.True(money.Amount == 100);
    }
    
    [Fact]
    public void Most_Be_Decrement_Money()
    {
        //arrange
        var money = new Money("C",50m);
        
        //action
        money.DecrementAmount(50);

        //assert
        Assert.True(money.Amount == 0);
    }
    
    
    [Fact]
    public void Most_Be_Decrement_Money_Negative()
    {
        //arrange
        var money = new Money("C",50m);
        
        //action
        money.DecrementAmount(51);

        //assert
        Assert.True(money.Amount == -1);
    }
}