using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentSimplify.Application.Transactions.Commands;
using PaymentSimplify.Common.Results;

namespace PaymentSimplify.Api.Controllers;

[Authorize]
public class TransactionController : ApiControllerBase
{
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Result>> CreateAuthCommand(CreateTransactionCommand command)
    {
        return await Mediator.Send(command);
    }
}