using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.DTO.Users;
using CoreBussiness.ResultPattern;
using MediatR;

namespace CommadQuerySegerationResponsibility.Users.Commads;

public class LoginUserCommand:IRequest<Result<User>>, IRequest<Result<string>>
{
    public LoginModel? LoginModel { get; set; }
}