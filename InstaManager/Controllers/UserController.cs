using CoreBussiness.DTO.Users;
using InstaManager.ModuleApplication.AuthApplication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Segerations.Users.Command;

namespace InstaManager.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("/[controller]")]
public class UserController:ControllerBase
{
    private IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// adding user .
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [TokenValidator]
     public async Task<IActionResult> RegisterUser([FromBody]UserModel userModel)
    {
        var result = await _mediator.Send(new RegisterUserCommand(){UserModel = userModel,CreatorId = HttpContext.GetCurrentUserId()});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }
}