using CoreBussiness.DTO.Users;
using InstaManager.ModuleApplication.AuthApplication;

namespace InstaManager.ModuleApplication.Binders;

public static class BinderService
{
    public static void BindApplication(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<JwtOptions>(x => configuration.GetSection("JwtConfiguration").Bind(x));
    }
}