using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentSimplify.Application.Auths.Commands.AuthenticatedAuth;
using PaymentSimplify.Application.Auths.Commands.CreateAuth;
using PaymentSimplify.Common.Results;

namespace PaymentSimplify.Api.Controllers;

[AllowAnonymous]
public class AuthController : ApiControllerBase
{
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Result>> CreateAuthCommand(CreateAuthCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPost]
    [Route("authenticated")]
    [Authorize]
    public async Task<ActionResult<Result>> AuthenticatedCommand(AuthenticatedAuthCommand command)
    {
        return await Mediator.Send(command);
    }
}