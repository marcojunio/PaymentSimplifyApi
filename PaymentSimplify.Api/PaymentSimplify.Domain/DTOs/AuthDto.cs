namespace PaymentSimplify.Domain.DTOs;

public class AuthDto
{
    public string Id { get; set; }
    public string IdCustumer { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}