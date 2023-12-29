using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.Sql;

namespace InstaManager.ModuleApplication.ElmahService;

public static class Elmah
{
    public static void StartElmahService(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddElmah<SqlErrorLog>(options =>
        {
            options.Path = "/Elmah";
            options.ConnectionString = configuration.GetConnectionString("Elmah");
            options.Filters.Add(new NotFoundFilter());
        });
    }
    public class NotFoundFilter:IErrorFilter
    {
        public void OnErrorModuleFiltering(object sender, ExceptionFilterEventArgs args)
        {
            if (args.Exception.GetBaseException() is FileNotFoundException)
            {
                args.Dismiss();
            }
            
            if(args.Context is HttpContext httpContext)
                if (httpContext.Response.StatusCode == 404)
                    args.Dismiss();
        }
    }
}