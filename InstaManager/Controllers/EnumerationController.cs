using MediatR;
using Microsoft.AspNetCore.Mvc;
using Segerations.Roles.Query;

namespace InstaManager.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("/[controller]")]
public class EnumerationController:ControllerBase
{
    private IMediator _mediator;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public EnumerationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/[controller]/Selection")]
    public async Task<IActionResult> GetRolesSelection()
    {
        var result = await _mediator.Send(new ListRoleSelectionQuery());
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }
}