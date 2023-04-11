using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.UnitTests.Domain.Entities;

public class AuthTests
{
    [Fact]
    public void Most_Be_Change_Password_Success()
    {
        //arrange
        var auth = Setup();
        
        //action
        auth.ChangePassword("321");
        
        //assert
        Assert.Equal("321",auth.Password);
    }
    
    [Fact]
    public void Most_Be_Change_Password_Fail()
    {
        //arrange
        var auth = Setup();
        
        //action
        auth.ChangePassword("");
        
        //assert
        Assert.Equal("123",auth.Password);
    }
    
    [Fact]
    public void Most_Be_Change_Email_Success()
    {
        //arrange
        var auth = Setup();
        
        //action
        auth.ChangeEmail(Email.Create("teste1@gmail.com").Value);
        
        //assert
        Assert.Equal("teste1@gmail.com",auth.Email.Addreess);
    }
    
    [Fact]
    public void Most_Be_Change_Email_Fail()
    {
        //arrange
        var auth = Setup();
        
        //action
        var email = Email.Create("").Value;
        auth.ChangeEmail(email);

        //assert
        Assert.Equal("teste@gmail.com",auth.Email.Addreess);
    }


    private Auth Setup()
    {
        return new Auth(Email.Create("teste@gmail.com").Value, "123",Guid.NewGuid().ToString(),
            new Customer("first name", "last name", Document.Create("21980467099").Value,new AccountBank(new Money("C",50))));
    }
}