namespace PaymentSimplify.Application.Common.Interfaces;

public interface ICentralBankService
{
    Task<bool> TransactionAuthorized();
}