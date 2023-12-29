using CoreBussiness.ResultPattern;
using CoreBussiness.ServiceManagement;
using MediatR;
namespace CqrsCore.Users;

public class LoginUserCommandHandler:IRequestHandler<LoginUserCommand,Result<string>>
{

    private IServiceManager _serviceManager;

    public LoginUserCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}