using System.ComponentModel.DataAnnotations;
using CoreBussiness.DTO.Roles;
using CoreBussiness.DTO.Users;
using CoreBussiness.ServiceManagement;
using CoreBussiness.UnitsOfWork;
using CoreStorage.AppContext;
using InstaManager.ModuleApplication.AuthApplication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Segerations.Roles.Command;
using Segerations.Users.Command;

namespace InstaManager.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("/[controller]")]
public class AutheticationController:ControllerBase
{
    private IMediator _mediator;
    public AutheticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// making user login and authenticate and then get a jwt token
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] LoginModel model)
    {
        var result = await _mediator.Send(new LoginUserCommand() { LoginModel = model });
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }

    /// <summary>
    /// logging user out
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        var userId = HttpContext.GetCurrentUserId();
        var result = await _mediator.Send(new LogoutCommand() {UserId = userId});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }

    /// <summary>
    /// the authenticated user would be able to change his own password
    /// </summary>
    [HttpPut]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model,CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetCurrentUserId();
        var result = await _mediator.Send(new ChangePasswordCommand() { PasswordModel = model,UserId = userId}, cancellationToken);
        return result.Success ? Ok(result.Data) : Problem(result.Error);
    }
    
    // forget Password.
    //change profile.
    
}