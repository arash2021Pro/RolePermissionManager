using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.ResultPattern;
using CoreBussiness.ServiceManagement;
using InstaManager.ModuleApplication.AuthApplication;
using MediatR;

namespace CommadQuerySegerationResponsibility.Users.Commads;

public class LoginUserCommandHandler:IRequestHandler<LoginUserCommand,Result<string>>
{
    private IServiceManager _serviceManager;
    private IJwtProvider _jwtProvider;
    public LoginUserCommandHandler(IServiceManager serviceManager, IJwtProvider jwtProvider)
    {
        _serviceManager = serviceManager;
        _jwtProvider = jwtProvider;
    }
    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var token = await _jwtProvider.GenerateJwtAsync(request.LoginModel!.UserName,request.LoginModel.Password!);
        return Result<string>.IsSuccess(token!);
    }
}