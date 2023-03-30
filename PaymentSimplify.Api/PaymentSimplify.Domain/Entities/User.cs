namespace PaymentSimplify.Domain.Entities;

public class User : BaseEntity
{
    public Document Document { get; set; }
    public string FistName { get; set; }
    public string LastName { get; set; }
    public  Email Email { get; set; }
}