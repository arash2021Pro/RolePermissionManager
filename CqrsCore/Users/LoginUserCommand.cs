using CoreBussiness.DTO.Users;
using CoreBussiness.ResultPattern;
using MediatR;

namespace CqrsCore.Users;

public class LoginUserCommand:IRequest<Result<string>>
{
    public LoginModel? LoginModel { get; set; }
}