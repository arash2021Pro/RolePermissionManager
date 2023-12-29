using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InstaManager.ModuleApplication.AuthApplication;

public static class AuthenticatedUser
{
    public static int GetCurrentUserId(this HttpContext httpContext)
    {
        var userId = 0;
        try
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var handler = new JwtSecurityTokenHandler();
            var claim = handler.ReadJwtToken(token);
            userId = int.Parse(claim.Claims.First(x => x.Type == "Identifier").Value);
        }
        catch (Exception e)
        {
            return e.HResult;
        }
        return userId;
    }
}