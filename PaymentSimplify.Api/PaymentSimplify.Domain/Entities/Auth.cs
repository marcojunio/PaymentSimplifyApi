namespace PaymentSimplify.Domain.Entities;

public class Auth : BaseAuditableEntity
{
    public Auth(Email email, string password, Customer customer)
    {
        Email = email;
        Password = password;
        Customer = customer;
    }

    public virtual Customer Customer { get; }
    public Email Email { get; private set; }
    public string Password { get; private set; }

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