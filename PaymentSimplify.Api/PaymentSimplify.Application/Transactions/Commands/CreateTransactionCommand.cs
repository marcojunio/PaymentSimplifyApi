using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Common.Results;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.Application.Transactions.Commands;

public class CreateTransactionCommand : IRequest<Result>
{
    public string IdPayee { get; set; }
    public decimal Amount { get; set; }
}

public class CreateAuthCommandHandler : IRequestHandler<CreateTransactionCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentSimplifyContext _paymentSimplifyContext;

    public CreateAuthCommandHandler(
        IUnitOfWork unitOfWork,
        IPaymentSimplifyContext paymentSimplifyContext)
    {
        _unitOfWork = unitOfWork;
        _paymentSimplifyContext = paymentSimplifyContext;
    }

    public async Task<Result> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var idPayer = new Guid("37b5ad11-236c-499c-5d95-08db3641a6fa");
        var idPayee = new Guid(request.IdPayee);

        var payer = await _paymentSimplifyContext
            .Customers
            .Include(f => f.AccountBank)
            .Where(f => f.Id == idPayer).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        var payee = await _paymentSimplifyContext
            .Customers
            .Include(f => f.AccountBank)
            .Where(f => f.Id == idPayee).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        var transaction =  new Transaction(new Money("C",request.Amount),payer,payee);

        transaction.CreateTransactionTransfer();

        _paymentSimplifyContext.AccountBanks.Update(payee.AccountBank);
        _paymentSimplifyContext.AccountBanks.Update(payer.AccountBank);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}