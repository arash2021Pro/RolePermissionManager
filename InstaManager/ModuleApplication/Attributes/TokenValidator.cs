using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CoreBussiness.ResultPattern;
using CoreBussiness.ServiceManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace InstaManager.ModuleApplication.AuthApplication;

public class TokenValidator:AuthorizeAttribute,IAsyncAuthorizationFilter
{
  
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        Result<bool> ValidateToken(string? token)
         {
             var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            if (String.IsNullOrEmpty(token))
            {
                return Result<bool>.Fail("توکن وجود ندارد");
            }
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtConfiguration")["SecretKey"]!));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration.GetSection("JwtConfiguration")["Issuer"]!,
                    ValidAudience =configuration.GetSection("JwtConfiguration")["Audience"]!,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return Result<bool>.Fail("توکن معتبر نیست");
            }
            return Result<bool>.IsSuccess(true);
        }
        string? controllerName = context.HttpContext.Request.RouteValues["controller"]?.ToString();
        controllerName += "Controller";
        string? actionName = context.HttpContext.Request.RouteValues["action"]?.ToString();

        var serviceManager = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>();
        var permissionResult = await serviceManager.UserService.GetUserPermissionAsync(context.HttpContext.GetCurrentUserId(), controllerName, actionName);
        
        var tokenResult = await serviceManager.UserService.GetUserTokenAsync(context.HttpContext.GetCurrentUserId());
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var result = ValidateToken(token!);
        if ( token == tokenResult.Data && result.Success && context.HttpContext.User.Identity!.IsAuthenticated)
        {
            if (permissionResult.Success)
            {
                
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedResult();
        }
    }

    
}