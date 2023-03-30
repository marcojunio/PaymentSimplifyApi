using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.UnitTests.Domain.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Most_Be_A_Valid_Email()
    {
        //arrange
        var email = Email.Create("test@gmail.com");
        
        //asset
        Assert.True(email.IsSuccess);        
        Assert.True(string.IsNullOrEmpty(email.Message));        
    }

    [Fact]
    public void Most_Be_A_Invalid_Email_By_Regex()
    {
        //arrange
        var email = Email.Create("test@.com");
        
        //asset
        Assert.True(email.IsError);        
        Assert.True(!string.IsNullOrEmpty(email.Message));      
    }
    
    [Fact]
    public void Most_Be_A_Invalid_Email_By_Empty()
    {
        //arrange
        var email = Email.Create("");
        
        //asset
        Assert.True(email.IsError);        
        Assert.True(!string.IsNullOrEmpty(email.Message));      
    }
    
    
    [Fact]
    public void Most_Be_A_Invalid_Email_By_Length()
    {
        //arrange
        var email = Email.Create("test@gmailikgjdfiogdsdfpsdofposdifgipofjgiopdfijogdfjiogdfjiogdfjiogojidfgjiodfpoifgoidfgoijdfiogjdfiojgdiodfgdfgdfsdlfksdpofsdopf.com");
        
        //asset
        Assert.True(email.IsError);        
        Assert.True(!string.IsNullOrEmpty(email.Message));        
    }
}