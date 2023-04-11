namespace PaymentSimplify.Domain.Entities;

public class Auth : BaseAuditableEntity
{
    public Auth(Email email, string password,string salt, Customer customer)
    {
        Email = email;
        Password = password;
        Customer = customer;
        Salt = salt;
    }

    private Auth()
    {
        
    }
    
    public virtual Customer Customer { get; } = null!;
    public Email Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public string Salt { get; private set; } = null!;

    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrEmpty(newPassword))
            return;

        Password = newPassword;
    }

    public void ChangeEmail(Email? email)
    {
        if(email is null)
            return;
        
        if (string.IsNullOrEmpty(email.Addreess))
            return;

        Email = email;
    }
}