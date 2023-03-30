
using PaymentSimplify.Domain.Enums;
using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.UnitTests.Domain.ValueObjects;

public class DocumentTests
{
    [Fact]
    public void Most_Be_A_Valid_Document_Cpf()
    {
        //arrange
        var document = new Document("07389806095",TypeDocumentEnum.Cpf);

        //asserts
        Assert.True(document.IsValid());
    }
    
    [Fact]
    public void Most_Be_A_Valid_Document_Cnpj()
    {
        //arrange
        var document = new Document("09627300000170",TypeDocumentEnum.Cnpj);

        //asserts
        Assert.True(document.IsValid());
    }
    
    [Fact]
    public void Most_Be_A_Invalid_Document_Cpf()
    {
        //arrange
        var document = new Document("0738980605",TypeDocumentEnum.Cpf);

        //asserts
        Assert.False(document.IsValid());
    }
    
    [Fact]
    public void Most_Be_A_Invalid_Document_Cnpj()
    {
        //arrange
        var document = new Document("096273000001",TypeDocumentEnum.Cnpj);

        //asserts
        Assert.False(document.IsValid());
    }
}