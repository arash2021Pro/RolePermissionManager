using Microsoft.AspNetCore.Mvc;

namespace InstaManager.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("/[controller]")]
public class UserController:ControllerBase
{
    /// <summary>
    /// adding user .
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HttpGet]
    public async Task<IActionResult> RegisterUser()
    {
        return Ok();
    }
    
}