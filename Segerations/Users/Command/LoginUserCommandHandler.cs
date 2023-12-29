using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.DTO.Users;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Segerations.Users.Command;

public class LoginUserCommand:IRequest<Result<string>>
{
    public LoginModel? LoginModel { get; set; }
}
public class LoginUserCommandHandler:IRequestHandler<LoginUserCommand,Result<string>>
{
    private IUnitOfWork _work;
    private readonly JwtOptions _jwtOptions;
     
    public LoginUserCommandHandler(IUnitOfWork work,IOptionsSnapshot<JwtOptions>options)
    {
        _work = work;
        _jwtOptions = options.Value;
    }

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.LoginModel!.UserName) || string.IsNullOrEmpty(request.LoginModel.Password))
        {
            return Result<string>.Fail("نام کاربری یا رمز عبور خالی است.");
        }
         
        var user = await _work.Set<User>().AsTracking().FirstOrDefaultAsync(u => u.UserName == request.LoginModel.UserName && u.Password == request.LoginModel.Password);
        if (user == null)
        {
            return Result<string>.Fail("کاربری با این نام کاربری و یا رمز عبور یافت نشد.");
        }
        var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(_jwtOptions.ValidTokenTime);
        var tokenKey = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey!);

        ClaimsIdentity claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim("PhoneNumber", user.PhoneNumber!));
        claimsIdentity.AddClaim(new Claim("UserName", user.UserName));
        claimsIdentity.AddClaim(new Claim("Identifier", user.Id.ToString()));
        var role = await _work.Set<Role>().AsTracking().FirstOrDefaultAsync(x => x.Id == user.RoleId);

        if (role == null)
        {
            return Result<string>.Fail("نقش کاربری یافت نشد.");
        }
         
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName!));
        var signingCredential = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claimsIdentity,
            Expires = tokenExpiryTimeStamp,
            SigningCredentials = signingCredential,
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        user.Token = token;
        var saveChange=await _work.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<string>.IsSuccess(token) : Result<string>.Fail("خطا در پاسخگویی",500);
    }
}