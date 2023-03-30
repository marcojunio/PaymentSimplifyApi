
using PaymentSimplify.Domain.Enums;
using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.UnitTests.Domain.ValueObjects;

public class DocumentTests
{
    [Fact]
    public void Most_Be_A_Invalid_Document_By_Empty()
    {
        //arrange
        var document = Document.Create("");
        
        //asserts
        Assert.True(!document.IsSuccess);
        Assert.True(!string.IsNullOrEmpty(document.Message));
    }
    
    
    [Fact]
    public void Most_Be_A_Valid_Document_Cpf()
    {
        //arrange
        var document = Document.Create("07389806095");
        
        //asserts
        Assert.True(document.IsSuccess);
        Assert.True(document.Value.TypeDocument == TypeDocumentEnum.Cpf);
        Assert.True(string.IsNullOrEmpty(document.Message));
    }
    
    [Fact]
    public void Most_Be_A_Valid_Document_Cnpj()
    {
        //arrange
        var document =  Document.Create("09627300000170");

        //asserts
        Assert.True(document.IsSuccess);
        Assert.True(document.Value.TypeDocument == TypeDocumentEnum.Cnpj);
        Assert.True(string.IsNullOrEmpty(document.Message));
    }
    
    [Fact]
    public void Most_Be_A_Invalid_Document_Cpf()
    {
        //arrange
        var document = Document.Create("0738980605");

        //asserts
        Assert.True(document.IsError);
        Assert.True(!string.IsNullOrEmpty(document.Message));
    }
    
    [Fact]
    public void Most_Be_A_Invalid_Document_Cnpj()
    {
        //arrange
        var document = Document.Create("096273000001");

        //asserts
        Assert.True(document.IsError);
        Assert.True(!string.IsNullOrEmpty(document.Message));
    }
}