using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Common.Results;
using PaymentSimplify.Common.Strings;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Domain.Repositories;
using PaymentSimplify.Domain.ValueObjects;

namespace PaymentSimplify.Application.Transactions.Commands;

public class CreateTransactionCommand : IRequest<Result>
{
    public string IdPayee { get; set; }
    public decimal Amount { get; set; }
}

public class CreateAuthCommandHandler : IRequestHandler<CreateTransactionCommand, Result>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICustumerRepository _custumerRepository;
    private readonly IAccountBankRepository _accountBankRepository;
    private readonly ICentralBankService _centralBankService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthCommandHandler(
        IUnitOfWork unitOfWork,
        ICustumerRepository custumerRepository,
        ICurrentUserService currentUserService, 
        IAccountBankRepository accountBankRepository, ICentralBankService centralBankService)
    {
        _unitOfWork = unitOfWork;
        _custumerRepository = custumerRepository;
        _currentUserService = currentUserService;
        _accountBankRepository = accountBankRepository;
        _centralBankService = centralBankService;
    }

    public async Task<Result> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var idPayer = _currentUserService.GetIdUser()?.ToGuid();
        var idPayee = request.IdPayee.ToGuid();

        var payee = await _custumerRepository
            .GetQueryble()
            .Include(f => f.AccountBank)
                .ThenInclude(f => f.TransactionsPayee)
            .Where(f => f.Id == idPayee)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        var payer = await _custumerRepository
            .GetQueryble()
            .Include(f => f.AccountBank)
                .ThenInclude(f => f.TransactionsPayer)
            .Where(f => f.Id == idPayer)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if(payer is null)
            return Result.Error("Payer has not been identified.");
        
        if(payee is null)
            return Result.Error("Payee has not been identified");

        var transaction = new Transaction(new Money("C", request.Amount), payee.AccountBank, payer.AccountBank);

        var dbTransaction = _unitOfWork.BeginTransaction();

        try
        {
            transaction.CreateTransactionTransfer(payer);

            _accountBankRepository.Update(payer.AccountBank);
            
            _accountBankRepository.Update(payee.AccountBank);

            var authorized = await _centralBankService.TransactionAuthorized();

            if (!authorized)
            {
                await dbTransaction.RollbackAsync(cancellationToken);

                return Result.Error("Unauthorized transaction.");
            }
            
            await dbTransaction.CommitAsync(cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("The money has been transferred successfully.");
        }
        catch
        {
            await dbTransaction.RollbackAsync(cancellationToken);
            
            return Result.Error("Fail create bank transfer, try again later.");
        }
    }
}