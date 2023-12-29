using CoreApplication.PermissionApplication;
using CoreApplication.RoleApplication;
using CoreApplication.UserApplication;
using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.DTO.Users;
using CoreBussiness.ResultPattern;
using CoreBussiness.ServiceManagement;
using CoreBussiness.UnitsOfWork;
using CoreStorage.AppContext;
using FluentValidation;
using InstaManager.ModuleApplication.AuthApplication;
using InstaManager.ModuleApplication.SeedApplication;
using MapsterMapper;
using MediatR;
using Segerations.Users;
using Segerations.Users.Command;

namespace InstaManager.ModuleApplication.ServiceApps;

/// <summary>
/// 
/// </summary>
public static class ServiceApp
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
    public static void StartApplicationService(this IServiceCollection service)
    {
        service.AddScoped<IServiceManager, ServiceManager>();
        service.AddScoped<IUnitOfWork, ApplicationContext>();
        service.AddScoped<IUserService,UserService>();
        service.AddValidatorsFromAssemblyContaining(typeof(UserModelValidation));
        service.AddScoped<IMapper,Mapper>();
        service.AddScoped<IDatabaseInitializer, DatabaseInitialService>();
        service.AddScoped<IRoleService, RoleService>();
        service.AddScoped<IPermissionService, PermissionService>();
        service.AddScoped(typeof(IRequestHandler<LoginUserCommand, Result<string>>), typeof(LoginUserCommandHandler));
    }
}